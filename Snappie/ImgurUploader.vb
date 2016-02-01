Imports System.Threading
Imports Microsoft.WindowsAPICodePack.Taskbar
Imports System.Net
Imports System.Text
Imports System.IO

Public Class ImgurUploader
    Private _tc As New TrayController
    Dim thread As System.Threading.Thread
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                Button1.Enabled = False
                ProgressBar1.Visible = True
                TextBox1.Text = OpenFileDialog1.FileName
                ThenUpload()
            Catch ex As Exception
            End Try
        End If
    End Sub
    Private Sub UploadImagexD()
        Try
            Dim url As String = UploadImage(TextBox1.Text)
            OdusBae.Text = (url)
            Process.Start(OdusBae.Text)
            TextBox1.Text = ""
            Button1.Enabled = True
            PictureBox1.Image = My.Resources.logo_1200_630
            Label2.Text = "..."
            ProgressBar1.Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub ThenUpload()
        Timer1.Start()
        Label2.Text = "Uploading"
        PictureBox1.Image = My.Resources.post_62718_Imgur_loading_gif_candidate_vo_OiXh
        _tc.Progress = 0
        thread = New System.Threading.Thread(AddressOf UploadImagexD)
        thread.Start()
    End Sub
    Dim ClientId As String = "18931b2a8a62667"
    Public Function UploadImage(ByVal image As String)
        Dim w As New WebClient()
        w.Headers.Add("Authorization", "Client-ID " & ClientId)
        Dim Keys As New System.Collections.Specialized.NameValueCollection
        Try
            Keys.Add("image", Convert.ToBase64String(File.ReadAllBytes(image)))
            Dim WhoSoup As Byte() = w.UploadValues("https://api.imgur.com/3/image", Keys)
            Dim QQ = Encoding.ASCII.GetString(WhoSoup)
            Dim reg As New System.Text.RegularExpressions.Regex("link"":""(.*?)""")
            Dim match As RegularExpressions.Match = reg.Match(QQ)
            Dim url As String = match.ToString.Replace("link"":""", "").Replace("""", "").Replace("\/", "/")
            Return url
        Catch Erro As Exception
            MessageBox.Show("Something went wrong. " & Erro.Message)
            Return "Failed!"
        End Try
    End Function

    Private Sub ImgurUploader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Control.CheckForIllegalCrossThreadCalls = False
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar2.Increment(1)
        _tc.Progress = ProgressBar2.Value
        If ProgressBar2.Value = 100 Then
            ProgressBar2.Value = 0
            _tc.Progress = 0
            Timer1.Stop()
        End If
    End Sub
End Class