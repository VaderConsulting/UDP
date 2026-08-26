Public Class TraceListener
    Inherits System.Diagnostics.TraceListener

    Private m_Sender As New Server

    Public Sub New()
        m_Sender.Protocol = Net.Sockets.ProtocolType.Udp
        m_Sender.Encode = System.Text.Encoding.ASCII
        m_Sender.ServerPort = 8080
        Me.Name = "UDP Listener"
    End Sub

    Public Sub New(ByVal Port As Int32)
        m_Sender.Protocol = Net.Sockets.ProtocolType.Udp
        m_Sender.Encode = System.Text.Encoding.ASCII
        m_Sender.ServerPort = Port
        Me.Name = "UDP Listener"
    End Sub

    Public Overloads Overrides Sub Write(ByVal Message As String)
        Send(Message)
    End Sub

    Public Overloads Sub Write(ByVal ex As Exception)
        Send(ex.InnerException.ToString)
    End Sub

    Public Overloads Overrides Sub WriteLine(ByVal Message As String)
        Send(Message & vbCrLf)
    End Sub

    Private Sub Send(ByVal Message As String)
        If Message.Trim.Length > 0 Then
            m_Sender.SendMessage(My.Computer.Name & ": " & Message)
        End If
    End Sub

End Class
