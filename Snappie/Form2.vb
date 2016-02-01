Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Threading
Public Class Form2
    Dim thread As System.Threading.Thread
    Dim thread2 As System.Threading.Thread
    Dim Final As String = String.Empty
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Control.CheckForIllegalCrossThreadCalls = False
        Hidesplash()
        Final = CaptureS.Label2.Text
        Me.TopMost = True
        Timer1.Start()
    End Sub
    Private Sub hidesplash()
        If NewForm.SplashHide.Checked = True Then
            Me.Opacity = 0
            Me.WindowState = FormWindowState.Minimized
            Me.ShowInTaskbar = False
            Me.Hide()
        Else
        End If
    End Sub
    Protected Overrides Sub OnPaintBackground(ByVal e As PaintEventArgs)
        MyBase.OnPaintBackground(e)

        Dim rect As New Rectangle(0, 0, Me.ClientSize.Width - 1, Me.ClientSize.Height - 1)

        e.Graphics.DrawRectangle(Pens.Blue, rect)
    End Sub
    Dim ClientId As String = "18931b2a8a62667"
    Public Function UploadImage(ByVal image As String)
        Dim w As New WebClient()
        w.Headers.Add("Authorization", "Client-ID " & ClientId)
        Dim Keys As New System.Collections.Specialized.NameValueCollection
        Try
            Keys.Add("image", Convert.ToBase64String(File.ReadAllBytes(image)))
            Dim responseArray As Byte() = w.UploadValues("https://api.imgur.com/3/image", Keys)
            Dim result = Encoding.ASCII.GetString(responseArray)
            Dim reg As New System.Text.RegularExpressions.Regex("link"":""(.*?)""")
            Dim match As RegularExpressions.Match = reg.Match(result)
            Dim url As String = match.ToString.Replace("link"":""", "").Replace("""", "").Replace("\/", "/")
            Return url
        Catch s As Exception
            MessageBox.Show("Something went wrong. " & s.Message)
            Return "Failed!"
        End Try
    End Function
    Private Sub UploadImagexD()
        Try
            Dim url As String = UploadImage(Final)
            txtImgurLink.Text = (url)
            Process.Start(txtImgurLink.Text)
            Me.Hide()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        thread = New System.Threading.Thread(AddressOf UploadImagexD)
        thread.Start()
        Timer1.Stop()
    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Try
            If Me.Visible = True Then

            ElseIf Me.Visible = False Then
                NewForm.Show()
                NewForm.ShowInTaskbar = True
                Timer2.Stop()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub MaterialFlatButton1_Click(sender As Object, e As EventArgs) Handles MaterialFlatButton1.Click
        Me.WindowState = FormWindowState.Minimized
        Me.ShowInTaskbar = False
    End Sub
End Class