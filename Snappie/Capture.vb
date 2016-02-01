Imports System.Net
Imports System.Text
Imports System.IO

Public Class CaptureS
    Public Declare Function GetAsyncKeyState Lib "user32.dll" (ByVal key As Integer) As Integer
    Dim key As String = String.Empty
    Dim key1 As String = String.Empty
    Dim Final As String = String.Empty
    Private Sub Capture_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.Size = SystemInformation.PrimaryMonitorSize
        Panel1.BackColor = Color.FromArgb(50, Color.Blue)
    End Sub
    Private Sub Capture_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        check()
    End Sub
    Private Sub check()
        If Panel1.Visible = True Then
            '
        Else
            Cursor.Hide()
            Dim Value As Point

            Value = Me.Location
            Panel1.Location = Value
            Panel1.Location = Windows.Forms.Cursor.Position
            Panel1.Size = New Point(20, 22)
            Panel1.Location = MousePosition
            Panel1.Visible = True

            Timer2.Start()

        End If
    End Sub
    Private Sub Effect()
        Dim tempDir As String = "C:\Users\" & Environment.UserName.ToString & "\AppData\Local\Temp\capture.mp3"
        Dim capturesound As String = tempDir
        Form1.AxWindowsMediaPlayer1.URL = capturesound
    End Sub
    Private Sub Panel1_MouseClick(sender As Object, e As MouseEventArgs) Handles Panel1.MouseClick
        Effect()
        Me.Opacity = 0
        Me.ShowInTaskbar = False
        System.Threading.Thread.Sleep(79) 'for the icon to disapear in taskbar lol
        Panel1.BackColor = Color.FromArgb(25, Color.Transparent)
        PictureBox1.Size = Panel1.Location

        Dim img2 As Bitmap
        If Screen.PrimaryScreen.Bounds.Contains(Cursor.Position) Then
            Me.Bounds = Screen.PrimaryScreen.Bounds
        Else
            Dim screens() As Screen = Screen.AllScreens
            Me.Bounds = screens(1).Bounds
        End If
        img2 = New Bitmap(Panel1.Bounds.Width, Panel1.Bounds.Height)
        Dim gr As Graphics = Graphics.FromImage(img2)
        gr.CopyFromScreen(New Point(Panel1.Bounds.Left, Panel1.Bounds.Top), New Point(0, 0), Me.Size)
        Me.BackgroundImage = img2
        PictureBox1.Image = img2

        ' Now save...
        Cursor.Show()
        If NewForm.CheckBox2.Checked = True Then
            SaveFileDialog1.Filter = "Bmp Image (*.Bmp*)|*.Bmp"
            If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                img2.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Bmp)
                Label2.Text = SaveFileDialog1.FileName
                NewForm.Show()
                MsgBox("Saved")
            End If
        ElseIf NewForm.CheckBox2.Checked = False Then
            Dim Odus As String = "temp.bmp"
            Dim HaZe As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, Odus)
            img2 = PictureBox1.Image
            Dim FileToSaveAs As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, Odus)
            If NewForm.CheckBox3.Checked = True Then ' Save as bmp
                PictureBox1.Image.Save(FileToSaveAs, System.Drawing.Imaging.ImageFormat.Bmp)
                Label2.Text = HaZe
                Form2.Show()
                Form2.ShowInTaskbar = True
            ElseIf NewForm.CheckBox3.Checked = False Then ' Save as Jpeg..
                PictureBox1.Image.Save(FileToSaveAs, System.Drawing.Imaging.ImageFormat.Jpeg)
                Label2.Text = HaZe
                Form2.Show()
                Form2.ShowInTaskbar = True
            End If
        End If
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
    Private Sub Panel1_MouseWheel(sender As Object, e As MouseEventArgs) Handles Panel1.MouseWheel
        ' If e.Delta > 0 Then
        'Up
        'Panel1.Height += 10
        '  Panel1.Width += 10
        ' Else
        'Down
        '  Panel1.Height -= 10
        '  Panel1.Width -= 10
        '  End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Panel1.Size = MousePosition
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        If GetAsyncKeyState(Keys.RButton) <> 0 Then
            Timer2.Stop()
            Cursor.Show()
            Me.Close()
            NewForm.ShowInTaskbar = True
            NewForm.Show()
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
End Class