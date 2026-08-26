Imports System.Net.Sockets
Imports System.Net
Imports System.Text

Public Class Server
    'Inherits System.ComponentModel.Component

    '#Region " Component Designer generated code "

    '        Public Sub New(ByVal Container As System.ComponentModel.IContainer)
    '            MyClass.New()

    '            'Required for Windows.Forms Class Composition Designer support
    '            Container.Add(Me)
    '        End Sub

    '        Public Sub New()
    '            MyBase.New()

    '            'This call is required by the Component Designer.
    '            InitializeComponent()

    '            'Add any initialization after the InitializeComponent() call

    '        End Sub

    '        'Component overrides dispose to clean up the component list.
    '        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    '            If disposing Then
    '                If Not (components Is Nothing) Then
    '                    components.Dispose()
    '                End If
    '            End If
    '            MyBase.Dispose(disposing)
    '        End Sub

    '        'Required by the Component Designer
    '        Private components As System.ComponentModel.IContainer

    '        'NOTE: The following procedure is required by the Component Designer
    '        'It can be modified using the Component Designer.
    '        'Do not modify it using the code editor.
    '        Private Sub InitializeComponent()
    '            components = New System.ComponentModel.Container
    '        End Sub

    '#End Region

    ' Default property values
    Private m_Protocol As ProtocolType = ProtocolType.Udp
    Private m_ServerAddress As IPAddress = IPAddress.Broadcast
    Private m_ServerPort As Integer = 0
    Private m_data As Byte() = New Byte() {}
    Private m_Encode As Encoding
    Private m_LocalPort As Integer = 0

    ''' <summary>
    ''' The server protocol to use. Currently only UDP is supported.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Protocol() As ProtocolType
        Get
            Return (m_Protocol)
        End Get
        Set(ByVal Value As ProtocolType)
            If (Value = ProtocolType.Udp) Then
                m_Protocol = Value
            Else
                'Throw New ProtocolNotSupportedException
            End If
        End Set
    End Property

    ''' <summary>
    ''' The server IPEndPoint.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Server() As IPEndPoint
        Get
            Return New IPEndPoint(m_ServerAddress, m_ServerPort)
        End Get
        Set(ByVal Value As IPEndPoint)
            m_ServerAddress = Value.Address
            m_ServerPort = Value.Port
        End Set
    End Property

    ''' <summary>
    ''' The server IP Address.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ServerAddress() As IPAddress
        Get
            Return m_ServerAddress
        End Get
        Set(ByVal Value As IPAddress)
            m_ServerAddress = Value
        End Set
    End Property

    ''' <summary>
    ''' The server port number.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ServerPort() As Integer
        Get
            Return m_ServerPort
        End Get
        Set(ByVal Value As Integer)
            m_ServerPort = Value
        End Set
    End Property

    ''' <summary>
    ''' Text encoding to use for sent messages.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Encode() As Encoding
        Get
            Return (m_Encode)
        End Get
        Set(ByVal Value As Encoding)
            m_Encode = Value
        End Set
    End Property

    ''' <summary>
    ''' The local port number.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LocalPort() As Integer
        Get
            Return m_LocalPort
        End Get
        Set(ByVal value As Integer)
            m_LocalPort = value
        End Set
    End Property

    Public Sub SendMessage(ByVal message As String)
        ' Encode message per settings
        Select Case m_Encode.EncodingName
            Case "Default"
                m_data = Encoding.Default.GetBytes(message)
            Case "US-ASCII"
                m_data = Encoding.ASCII.GetBytes(message)
            Case "Unicode"
                m_data = Encoding.Unicode.GetBytes(message)
            Case "UTF7"
                m_data = Encoding.UTF7.GetBytes(message)
            Case "UTF8"
                m_data = Encoding.UTF8.GetBytes(message)
            Case Else
                m_data = Encoding.ASCII.GetBytes(message)
        End Select
        ' Send the message
        Try
            Select Case Protocol
                Case ProtocolType.Udp
                    SendUDPMessage(m_data)
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function SendUDPMessage(ByVal _data As Byte()) As Integer
        ' Create a UDP Server and send the message, then clean up
        Dim _UDPClient As UdpClient = Nothing
        Dim ReturnCode As Integer

        Try
            _UDPClient = New UdpClient(m_LocalPort)
            ReturnCode = 0
            _UDPClient.Connect(Server)
            ReturnCode = _UDPClient.Send(_data, _data.Length)
        Catch ex As Exception
            Throw ex
        Finally
            If Not (_UDPClient Is Nothing) Then
                _UDPClient.Close()
            End If
        End Try
        Return ReturnCode
    End Function

End Class


