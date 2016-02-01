Public Class AnimTab
    Inherits TabControl
    Dim OldIndex As Integer

    Private _Speed As Integer = 9
    Property Speed As Integer
        Get
            Return _Speed
        End Get
        Set(value As Integer)
            If value > 20 Or value < -20 Then
                MsgBox("Speed needs to be in between -20 and 20.")
            Else
                _Speed = value
            End If
        End Set
    End Property

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
    End Sub

    Sub DoAnimationScrollLeft(ByVal Control1 As Control, Control2 As Control)
        Dim G As Graphics = Control1.CreateGraphics()
        Dim P1 As New Bitmap(Control1.Width, Control1.Height)
        Dim P2 As New Bitmap(Control2.Width, Control2.Height)
        Control1.DrawToBitmap(P1, New Rectangle(0, 0, Control1.Width, Control1.Height))
        Control2.DrawToBitmap(P2, New Rectangle(0, 0, Control2.Width, Control2.Height))

        For Each c As Control In Control1.Controls
            c.Hide()
        Next

        Dim Slide As Integer = Control1.Width - (Control1.Width Mod _Speed)

        Dim a As Integer
        For a = 0 To Slide Step _Speed
            G.DrawImage(P1, New Rectangle(a, 0, Control1.Width, Control1.Height))
            G.DrawImage(P2, New Rectangle(a - Control2.Width, 0, Control2.Width, Control2.Height))
        Next
        a = Control1.Width
        G.DrawImage(P1, New Rectangle(a, 0, Control1.Width, Control1.Height))
        G.DrawImage(P2, New Rectangle(a - Control2.Width, 0, Control2.Width, Control2.Height))

        SelectedTab = Control2

        For Each c As Control In Control2.Controls
            c.Show()
        Next

        For Each c As Control In Control1.Controls
            c.Show()
        Next
    End Sub

    Protected Overrides Sub OnSelecting(e As System.Windows.Forms.TabControlCancelEventArgs)
        If OldIndex < e.TabPageIndex Then
            DoAnimationScrollRight(TabPages(OldIndex), TabPages(e.TabPageIndex))
        Else
            DoAnimationScrollLeft(TabPages(OldIndex), TabPages(e.TabPageIndex))
        End If
    End Sub

    Protected Overrides Sub OnDeselecting(e As System.Windows.Forms.TabControlCancelEventArgs)
        OldIndex = e.TabPageIndex
    End Sub

    Sub DoAnimationScrollRight(ByVal Control1 As Control, Control2 As Control)
        Dim G As Graphics = Control1.CreateGraphics()
        Dim P1 As New Bitmap(Control1.Width, Control1.Height)
        Dim P2 As New Bitmap(Control2.Width, Control2.Height)
        Control1.DrawToBitmap(P1, New Rectangle(0, 0, Control1.Width, Control1.Height))
        Control2.DrawToBitmap(P2, New Rectangle(0, 0, Control2.Width, Control2.Height))

        For Each c As Control In Control1.Controls
            c.Hide()
        Next

        Dim Slide As Integer = Control1.Width - (Control1.Width Mod _Speed)

        Dim a As Integer
        For a = 0 To -Slide Step -_Speed
            G.DrawImage(P1, New Rectangle(a, 0, Control1.Width, Control1.Height))
            G.DrawImage(P2, New Rectangle(a + Control2.Width, 0, Control2.Width, Control2.Height))
        Next
        a = Control1.Width
        G.DrawImage(P1, New Rectangle(a, 0, Control1.Width, Control1.Height))
        G.DrawImage(P2, New Rectangle(a + Control2.Width, 0, Control2.Width, Control2.Height))

        SelectedTab = Control2

        For Each c As Control In Control2.Controls
            c.Show()
        Next

        For Each c As Control In Control1.Controls
            c.Show()
        Next
    End Sub

End Class