Option Strict On
Imports Microsoft.WindowsAPICodePack.Taskbar

Public Class TrayController

    Public Sub New()
        _progress = 0
        _progressMax = 100
        _style = Styles.Green
    End Sub

    Public Enum Styles As Integer
        None = 0
        Marquee = 1
        Green = 2
        Red = 4
        Amber = 8
    End Enum

    Private _progress As Integer
    Public Property Progress As Integer
        Get
            Return _progress
        End Get
        Set(value As Integer)
            _progress = value
            TaskbarManager.Instance.SetProgressValue(_progress, _progressMax)
        End Set
    End Property

    Private _progressMax As Integer
    Public Property ProgressMax As Integer
        Get
            Return _progressMax
        End Get
        Set(value As Integer)
            _progressMax = value
            TaskbarManager.Instance.SetProgressValue(_progress, _progressMax)
        End Set
    End Property

    Private _style As Styles
    Public Property Style As Styles
        Get
            Return _style
        End Get
        Set(value As Styles)
            _style = value
            TaskbarManager.Instance.SetProgressState(DirectCast(_style, TaskbarProgressBarState))
        End Set
    End Property

    Private _overlayIcon As Icon
    Public Property OverlayIcon As Icon
        Get
            Return _overlayIcon
        End Get
        Set(value As Icon)
            _overlayIcon = value
            TaskbarManager.Instance.SetOverlayIcon(value, String.Empty)
        End Set
    End Property

    Public Function IsSupported() As Boolean
        Return TaskbarManager.IsPlatformSupported
    End Function
End Class