Public Class frmMain

    Private Delegate Sub m_ReceiveDelegate(ByVal Message As String)
    Private WithEvents m_Receiver As New UDP.Client
    Private m_Sender As New UDP.Server
    Private m_Name As String = My.Computer.Name

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        m_Receiver.Stop()
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        m_Receiver.Protocol = Net.Sockets.ProtocolType.Udp
        m_Receiver.Encode = System.Text.Encoding.ASCII
        m_Receiver.ClientPort = 8080
        m_Receiver.Start()

        m_Sender.Protocol = Net.Sockets.ProtocolType.Udp
        m_Sender.Encode = System.Text.Encoding.ASCII
        m_Sender.ServerPort = 8080

        lblNameValue.Text = m_Name
    End Sub

    Private Sub Receiver_AfterReceive(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_Receiver.AfterReceive
        Dim Message As String = m_Receiver.Message

        DoReceive(Message)
    End Sub

    Private Sub DoReceive(ByVal Message As String)

        If rtbReceive.InvokeRequired Then
            If chkIgnoreSelf.Checked And Message.StartsWith(m_Name) Then
            Else
                Dim MyDelegate As New m_ReceiveDelegate(AddressOf DoReceive)
                rtbReceive.Invoke(MyDelegate, Message)
            End If
        Else
            SyncLock rtbReceive
                rtbReceive.Text &= m_Receiver.Message & vbCrLf
                rtbReceive.SelectionStart = rtbReceive.TextLength
                rtbReceive.ScrollToCaret()
            End SyncLock
        End If

    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Send(txtMessage.Text.Trim)
    End Sub

    Private Sub Send(ByVal Message As String)
        If Message.Trim.Length > 0 Then
            m_Sender.SendMessage(m_Name & ": " & Message)

            txtMessage.Text = ""
            txtMessage.Focus()

            SyncLock rtbReceive
                rtbReceive.AppendText(m_Name & ": " & Message & vbCrLf)
                rtbReceive.SelectionStart = rtbReceive.TextLength
                rtbReceive.ScrollToCaret()
            End SyncLock
        End If
    End Sub

    Private Sub txtMessage_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMessage.KeyDown
        If e.KeyData = Keys.Return Or e.KeyData = Keys.Enter Then
            Send(txtMessage.Text)
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub

End Class
