Imports System.Net.Sockets
Imports System.Net
Imports System.Text
Imports System.Threading

Public Class Client
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

    ' Default Property values
    Private m_Protocol As ProtocolType = ProtocolType.Udp
    Private m_ThreadReceive As Thread
    Private m_ClientPort As Integer = 0
    Private m_Message As String = ""
    Private m_Encode As Encoding = Encoding.Default
    Private m_UDPClient As UdpClient
    Private m_Client As New IPEndPoint(IPAddress.Any, 0)
    Private m_BytesReceived As Integer = 0
    Private m_Closing As Boolean = False

    ''' <summary>
    ''' This event is fired right before an inbound message is received.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event BeforeReceive As EventHandler

    ''' <summary>
    ''' This event is fired immediately after an inbound message is received.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event AfterReceive As EventHandler

    ''' <summary>
    ''' The encoding to use with received messages.
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
    ''' The number of bytes received by the server in the most recent message.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BytesReceived() As Integer
        Get
            Return (m_BytesReceived)
        End Get
    End Property

    ''' <summary>
    ''' Read Only. The message received by the server.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Message() As String
        Get
            Return (m_Message)
        End Get
    End Property

    ''' <summary>
    ''' The client IPEndPoint.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Client() As IPEndPoint
        Get
            Return m_Client
        End Get
        Set(ByVal Value As IPEndPoint)
            m_Client = Value
        End Set
    End Property

    ''' <summary>
    ''' The server port to check for inbound data.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ClientPort() As Integer
        Get
            Return (m_ClientPort)
        End Get
        Set(ByVal Value As Integer)
            m_ClientPort = Value
        End Set
    End Property

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
            m_Protocol = Value
        End Set
    End Property

    Public Sub Receive()
        Dim AbortedByStop As Boolean

        ' Clear the Message property
        'm_Message = ""
        ' Raise the BeforeReceive event
        RaiseEvent BeforeReceive(Me, New EventArgs)
        ' Receive our UDP data
        Dim Data As Byte() = {}
        Try
            Select Case Protocol
                Case ProtocolType.Udp
                    Data = m_UDPClient.Receive(m_Client)
                    m_BytesReceived = Data.Length
                Case Else
                    'Throw New ProtocolNotSupportedException
            End Select
        Catch ex As ThreadAbortException
            AbortedByStop = True
            m_Message = ""
            Exit Sub
        Catch ex As Exception
            Throw ex
        Finally
            ' The thread finished blocking, and ended, so we start again
            If Not AbortedByStop Then '<--- ADDED
                InitializeThread()
            End If
        End Try
        ' Encode the data per the Encode property
        Dim Strdata As String = ""
        Select Case m_Encode.EncodingName
            Case "Default"
                Strdata = System.Text.Encoding.Default.GetString(Data)
            Case "US-ASCII"
                Strdata = System.Text.Encoding.ASCII.GetString(Data)
            Case "Unicode"
                Strdata = System.Text.Encoding.Unicode.GetString(Data)
            Case "UTF7"
                Strdata = System.Text.Encoding.UTF7.GetString(Data)
            Case "UTF8"
                Strdata = System.Text.Encoding.UTF8.GetString(Data)
            Case Else
                Strdata = System.Text.Encoding.ASCII.GetString(Data)
        End Select
        ' Set the message
        m_Message = Strdata
        ' Raise the AfterReceive event
        RaiseEvent AfterReceive(Me, New EventArgs)
    End Sub

    Private Sub InitializeClient()
        ' Configure a UDPClient
        Select Case Protocol
            Case ProtocolType.Udp
                If (m_UDPClient Is Nothing) Then
                    m_UDPClient = New UdpClient(m_ClientPort)
                End If
            Case Else
                'Throw New ProtocolNotSupportedException
        End Select
    End Sub

    Private Sub InitializeThread()
        ' Start a worker thread
        Try
            m_ThreadReceive = New Thread(AddressOf Receive)
            m_ThreadReceive.Start()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Start()
        ' Initialize the Client and the Thread
        InitializeClient()
        InitializeThread()
    End Sub

    Public Sub [Stop]()
        ' Close the UDPClient and stop the worker thread
        Try
            ' Suspend the thread and then abort it.   Keeps it from continuing to try to process anything further while
            ' it winds down.
            If Not m_ThreadReceive Is Nothing Then
                m_ThreadReceive.Abort()
            End If
            If Not (m_UDPClient Is Nothing) Then
                ' Close the UDPClient and then force it to Nothing
                m_UDPClient.Close()
                m_UDPClient = Nothing
            End If
            If Not m_ThreadReceive Is Nothing Then
                m_ThreadReceive.Join()
                m_ThreadReceive = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
