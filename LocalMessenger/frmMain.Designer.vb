<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.rtbReceive = New System.Windows.Forms.RichTextBox
        Me.txtMessage = New System.Windows.Forms.TextBox
        Me.btnSend = New System.Windows.Forms.Button
        Me.lblName = New System.Windows.Forms.Label
        Me.lblNameValue = New System.Windows.Forms.Label
        Me.chkIgnoreSelf = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'rtbReceive
        '
        Me.rtbReceive.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbReceive.Location = New System.Drawing.Point(12, 61)
        Me.rtbReceive.Name = "rtbReceive"
        Me.rtbReceive.ReadOnly = True
        Me.rtbReceive.Size = New System.Drawing.Size(320, 189)
        Me.rtbReceive.TabIndex = 5
        Me.rtbReceive.Text = ""
        '
        'txtMessage
        '
        Me.txtMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMessage.Location = New System.Drawing.Point(12, 35)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(240, 20)
        Me.txtMessage.TabIndex = 3
        '
        'btnSend
        '
        Me.btnSend.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSend.Location = New System.Drawing.Point(258, 33)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(74, 22)
        Me.btnSend.TabIndex = 4
        Me.btnSend.Text = "Send"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(12, 9)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(43, 13)
        Me.lblName.TabIndex = 0
        Me.lblName.Text = "Name:"
        '
        'lblNameValue
        '
        Me.lblNameValue.AutoSize = True
        Me.lblNameValue.Location = New System.Drawing.Point(53, 9)
        Me.lblNameValue.Name = "lblNameValue"
        Me.lblNameValue.Size = New System.Drawing.Size(62, 13)
        Me.lblNameValue.TabIndex = 1
        Me.lblNameValue.Text = "NameValue"
        '
        'chkIgnoreSelf
        '
        Me.chkIgnoreSelf.AutoSize = True
        Me.chkIgnoreSelf.Checked = True
        Me.chkIgnoreSelf.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIgnoreSelf.Location = New System.Drawing.Point(251, 8)
        Me.chkIgnoreSelf.Name = "chkIgnoreSelf"
        Me.chkIgnoreSelf.Size = New System.Drawing.Size(75, 17)
        Me.chkIgnoreSelf.TabIndex = 2
        Me.chkIgnoreSelf.Text = "Ignore self"
        Me.chkIgnoreSelf.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(344, 262)
        Me.Controls.Add(Me.chkIgnoreSelf)
        Me.Controls.Add(Me.lblNameValue)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.txtMessage)
        Me.Controls.Add(Me.rtbReceive)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Local Messenger"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rtbReceive As System.Windows.Forms.RichTextBox
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblNameValue As System.Windows.Forms.Label
    Friend WithEvents chkIgnoreSelf As System.Windows.Forms.CheckBox

End Class
