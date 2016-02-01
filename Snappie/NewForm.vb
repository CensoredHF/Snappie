Imports System.Drawing.Drawing2D
Imports System.Threading
Imports Microsoft.WindowsAPICodePack.Taskbar
Imports System.Text
Imports System.IO
Imports System.Net

Public Class NewForm
    Private _tc As New TrayController
    Dim thread As System.Threading.Thread
    Dim Final As String = String.Empty
    Dim thread2 As System.Threading.Thread
    Public Declare Function GetAsyncKeyState Lib "user32.dll" (ByVal key As Integer) As Integer
    Private Sub Panel1_DragDrop(sender As Object, e As DragEventArgs) Handles Panel1.DragDrop
        Dim theFiles() As String = CType(e.Data.GetData("FileDrop", True), String())
        For Each theFile As String In theFiles
            TextBox1.Text = (theFile)
            PictureBox1.Image = My.Resources._new
            Timer2.Start()
            ThenUpload()
        Next
    End Sub

    Private Sub Panel1_DragEnter(sender As Object, e As DragEventArgs) Handles Panel1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        PictureBox1.Image = My.Resources.waitgif
        Timer2.Stop()
    End Sub
    Sub PlaySystemSound()
        Try

            Dim tempDir As String = "C:\Users\" & Environment.UserName.ToString & "\AppData\Local\Temp\capture.mp3"
            Dim capturesound As String = tempDir

            Me.AxWindowsMediaPlayer1.URL = capturesound
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CreateCaptureSound()
        Dim tempDir As String = "C:\Users\" & Environment.UserName.ToString & "\AppData\Local\Temp\capture.mp3"

        If System.IO.File.Exists(tempDir) = True Then
            ' Do nothing
        ElseIf System.IO.File.Exists(tempDir) = True Then
            My.Computer.FileSystem.WriteAllBytes(tempDir, My.Resources.ResourceManager.GetObject("Capture"), False)
        End If
    End Sub
    Private Sub CheckUpdate()
        Try
            Dim address As String = "https://www.dropbox.com/s/z5w3r4k2w3s4dvl/UpdateCheck.txt?dl=1"
            Dim client As WebClient = New WebClient()
            Dim reply As String = client.DownloadString(address)
            UpdateShow.Text = reply
            If UpdateShow.Text = "NewUpdate" Then
                ' New update Is avaible
                XylosButton4.Visible = True
                UpdateShow.ForeColor = Color.Lime
            Else
                UpdateShow.Text = "No new updates"
            End If
        Catch ex As Exception
 
        End Try
    End Sub
    Private Sub CheckForUpdates()
        If My.Computer.Network.IsAvailable = True Then
            ' Check for updates If enabled
            If CheckBox8.Checked = False Then
                thread = New System.Threading.Thread(AddressOf CheckUpdate)
                thread.Start()
            Else
                UpdateShow.Text = "Disabled In settings"
            End If
        ElseIf My.Computer.Network.IsAvailable = False Then

            UpdateShow.Text = "No Internet Connection :-("

        End If
    End Sub
    Private Sub CreateRecent()
        Dim AppData As String = "C:\Users\" & Environment.UserName.ToString & "\AppData\Roaming\Snappie"
        If System.IO.Directory.Exists(AppData) = False Then
            Directory.CreateDirectory(AppData)
        End If
    End Sub
    Private Sub Tray()
        If StartTray.Checked = True Then
            Me.WindowState = FormWindowState.Minimized
            Me.Hide()
            Me.Opacity = 0
            Me.ShowInTaskbar = False
            NotifyIcon1.Visible = True
        ElseIf StartTray.Checked = False Then

        End If
    End Sub
    Private Sub MainScan()
        Dim AppData As String = "C:\Users\" & Environment.UserName.ToString & "\AppData\Roaming\Snappie"
        Dim RecentLocation As New IO.DirectoryInfo(AppData)
        Dim RecentPictures As IO.FileInfo() = RecentLocation.GetFiles()
        Dim Eeee As IO.FileInfo

        For Each Eeee In RecentPictures
            ListBox1.Items.Add(Eeee.FullName)
        Next
        LoadRecent()
    End Sub
    Private Sub LoadRecent()
        Try
            Dim AppData As String = "C:\Users\" & Environment.UserName.ToString & "\AppData\Roaming\Snappie"
            If PictureBox5.Image Is Nothing = True Then
                ListBox1.SelectedIndex = 0
                Dim xD As String = String.Empty
                xD = ListBox1.SelectedItem
                PictureBox5.Image = Image.FromFile(xD)
                ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
            End If
            If PictureBox6.Image Is Nothing = True Then
                ListBox1.SelectedIndex = 0
                Dim xD As String = String.Empty
                xD = ListBox1.SelectedItem
                PictureBox6.Image = Image.FromFile(xD)
                ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
            End If
            If PictureBox7.Image Is Nothing = True Then
                ListBox1.SelectedIndex = 0
                Dim xD As String = String.Empty
                xD = ListBox1.SelectedItem
                PictureBox7.Image = Image.FromFile(xD)
                ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
                LinkLabel1.Visible = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Checkrecent()
        If CheckBox6.Checked = True Then
            MainScan()
        ElseIf CheckBox6.Checked = False Then
        End If
    End Sub
    Private Sub CheckSettings()
        If My.Settings.SaveFile = "1" Then
            CheckBox2.Checked = True
        ElseIf "0" = My.Settings.SaveFile Then
            CheckBox2.Checked = False
        End If
        If "1" = My.Settings.HighQuality Then
            CheckBox3.Checked = True
        ElseIf "0" = My.Settings.HighQuality Then
            CheckBox3.Checked = False
        End If
        If "1" = My.Settings.CopyImageToClip Then
            CheckBox4.Checked = True
        ElseIf "0" = My.Settings.CopyImageToClip Then
            CheckBox4.Checked = False
        End If
        If "1" = My.Settings.Tags Then
            RadioButton1.Checked = True
        ElseIf "0" = My.Settings.Tags Then
            RadioButton1.Checked = False
        End If
        If "1" = My.Settings.CaptureSound Then
            SoundEffect.Checked = True
        ElseIf "0" = My.Settings.CaptureSound Then
            SoundEffect.Checked = False
        End If
        If "1" = My.Settings.RecentPreview Then
            CheckBox7.Checked = True
        ElseIf "0" = My.Settings.RecentPreview Then
            CheckBox7.Checked = False
        End If
        If "1" = My.Settings.RecentPicture Then
            CheckBox6.Checked = True
        ElseIf "0" = My.Settings.RecentPicture Then
            CheckBox6.Checked = False
        End If
        If "1" = My.Settings.QuickSnap Then
            CheckBox5.Checked = True
        ElseIf "0" = My.Settings.QuickSnap Then
            CheckBox5.Checked = False
        End If
        If "1" = My.Settings.TrayIcon Then
            StartTray.Checked = True
        ElseIf "0" = My.Settings.TrayIcon Then
            StartTray.Checked = False
        End If
        If "1" = My.Settings.Update Then
            CheckBox8.Checked = True
        ElseIf "0" = My.Settings.Update Then
            CheckBox8.Checked = False
        End If
    End Sub
    Private Sub NewForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Control.CheckForIllegalCrossThreadCalls = False
        CreateCaptureSound()
        CheckSettings()
        CheckForUpdates()

        CreateRecent()
        Checkrecent()
        Tray()
        CheckPhoto()
    End Sub
    Private Sub CheckPhoto()
        Try
            If CheckBox6.Checked = True Then
                Dim AppData As String = "C:\Users\" & Environment.UserName.ToString & "\AppData\Roaming\Snappie"
                Dim myDir As DirectoryInfo = New DirectoryInfo(AppData)
                If (myDir.EnumerateFiles().Any()) Then

                    LinkLabel3.Visible = True

                Else

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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
    Private Sub checkclip()
        If CheckBox4.Checked = True Then
            If RadioButton1.Checked = True Then
                Clipboard.SetText("[Img]" + OdusBae.Text + "[/Img]")
            Else
                Clipboard.SetText(OdusBae.Text)
            End If
        End If
    End Sub
    Private Sub UploadImagexD()
        Try
            Dim url As String = UploadImage(TextBox1.Text)
            OdusBae.Text = (url)
            Process.Start(OdusBae.Text)
            checkclip()
            TextBox1.Text = ""
            XylosButton1.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub ThenUpload()
        ProgressBar1.Value = 0
        _tc.Progress = 0
        XylosButton1.Enabled = False
        StartThread.Start()
        Timer5.Start()
    End Sub
    Private Sub XylosButton1_Click(sender As Object, e As EventArgs) Handles XylosButton1.Click

        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                TextBox1.Text = OpenFileDialog1.FileName
                ThenUpload()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        PictureBox3.Image = My.Resources.Capture1
        Timer3.Stop()
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then
            My.Settings.CopyImageToClip = "1"
            My.Settings.Save()
            RadioButton1.Visible = True
            RadioButton2.Visible = True
            RadioButton1.Checked = False
            RadioButton2.Checked = False
        ElseIf CheckBox4.Checked = False Then
            My.Settings.CopyImageToClip = "0"
            RadioButton1.Checked = False
            RadioButton2.Checked = False
            RadioButton1.Visible = False
            RadioButton2.Visible = False
        End If
    End Sub
    Private Sub Selectregion()
        Try

            Me.ShowInTaskbar = False
            Me.Hide()
            System.Threading.Thread.Sleep(79)
            CaptureS.Show() ' Start the capturing.

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub XylosButton2_Click(sender As Object, e As EventArgs) Handles XylosButton2.Click
        Selectregion()
    End Sub
    Private Sub Main()
        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim r As New Random
        Dim sb As New StringBuilder
        For i As Integer = 1 To 8
            Dim idx As Integer = r.Next(0, 35)
            sb.Append(s.Substring(idx, 1))
        Next
        RandomString.Text = sb.ToString
    End Sub
    Private Sub SavePicture()
        Dim AppData As String = "C:\Users\" & Environment.UserName.ToString & "\AppData\Roaming\Snappie\"
        SaveFileDialog1.Filter = "Bmp Files (*.Bmp*)|*.Bmp"
        Main()
        If CheckBox6.Checked = True Then
            Me.PictureBox4.Image.Save(AppData + RandomString.Text + ".PNG")
        End If
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

            If (Me.PictureBox4.Image IsNot Nothing) Then
                Me.PictureBox4.Image.Save(SaveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp)
            End If
        End If
    End Sub
    Private Sub CaptureFull()
        Dim bounds As Rectangle
        Dim screenshot As System.Drawing.Bitmap
        Dim graph As Graphics
        bounds = Screen.PrimaryScreen.Bounds
        screenshot = New System.Drawing.Bitmap(bounds.Width, bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        graph = Graphics.FromImage(screenshot)
        graph.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy)
        PictureBox4.Image = screenshot
        SavePicture()
    End Sub
    Private Sub XylosButton3_Click(sender As Object, e As EventArgs) Handles XylosButton3.Click
        TakeDesktop()
    End Sub
    Private Sub TakeDesktop()
        If SoundEffect.Checked = False Then
            PlaySystemSound()
            Me.Hide()
            Me.ShowInTaskbar = False
            System.Threading.Thread.Sleep(79)

            CaptureFull()


            Me.WindowState = FormWindowState.Normal
            Me.Show()
            Me.ShowInTaskbar = True
        ElseIf SoundEffect.Checked = True Then
            Me.Hide()
            Me.ShowInTaskbar = False
            System.Threading.Thread.Sleep(79)

            CaptureFull()


            Me.WindowState = FormWindowState.Normal
            Me.Show()
            Me.ShowInTaskbar = True
        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If CheckBox5.Checked = True Then

        ElseIf CheckBox5.Checked = False Then
            If GetAsyncKeyState(Keys.PrintScreen) <> 0 Then
                CaptureFull()
            End If
        End If
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        If CheckBox5.Checked = True Then

        ElseIf CheckBox5.Checked = False Then
            If GetAsyncKeyState(Keys.F11) <> 0 Then
                CaptureS.Close()
                Selectregion()
            End If
        End If
    End Sub
    Private Sub PictureBox5_MouseHover(sender As Object, e As EventArgs) Handles PictureBox5.MouseHover
        If CheckBox7.Checked = True Then
            PreviewRecent.Show()
            PreviewRecent.PictureBox1.Image = PictureBox5.Image
        ElseIf CheckBox7.Checked = False Then

        End If
    End Sub
    Private Sub PictureBox6_MouseHover(sender As Object, e As EventArgs) Handles PictureBox6.MouseHover
        If CheckBox7.Checked = True Then
            PreviewRecent.Show()
            PreviewRecent.PictureBox1.Image = PictureBox6.Image
        ElseIf CheckBox7.Checked = False Then

        End If
    End Sub
    Private Sub PictureBox7_MouseHover(sender As Object, e As EventArgs) Handles PictureBox7.MouseHover
        If CheckBox7.Checked = True Then
            PreviewRecent.Show()
            PreviewRecent.PictureBox1.Image = PictureBox7.Image
        ElseIf CheckBox7.Checked = False Then

        End If
    End Sub
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        ShowMoreRecent.Show()
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        PlaySystemSound()
    End Sub

    Private Sub Timer5_Tick(sender As Object, e As EventArgs) Handles Timer5.Tick
        ProgressBar1.Increment(3)
        _tc.Progress = ProgressBar1.Value

        If ProgressBar1.Value = 100 Then
            Timer5.Stop()
            _tc.Progress = 0
        End If

    End Sub

    Private Sub StartThread_Tick(sender As Object, e As EventArgs) Handles StartThread.Tick
        thread = New System.Threading.Thread(AddressOf UploadImagexD)
        thread.IsBackground = True
        thread.TrySetApartmentState(ApartmentState.STA)
        thread.Start()
        StartThread.Stop()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            My.Settings.SaveFile = "1"
            My.Settings.Save()
        ElseIf CheckBox2.Checked = False Then
            My.Settings.SaveFile = "0"
            My.Settings.Save()
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            My.Settings.HighQuality = "1"
            My.Settings.Save()
        ElseIf CheckBox3.Checked = False Then
            My.Settings.HighQuality = "0"
            My.Settings.Save()
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            My.Settings.Tags = "1"
            My.Settings.Save()
        ElseIf RadioButton2.Checked = True Then
            My.Settings.Tags = "0"
            My.Settings.Save()
        End If
    End Sub

    Private Sub SoundEffect_CheckedChanged(sender As Object, e As EventArgs) Handles SoundEffect.CheckedChanged
        If SoundEffect.Checked = True Then
            My.Settings.CaptureSound = "1"
            My.Settings.Save()
        ElseIf SoundEffect.Checked = False Then
            My.Settings.CaptureSound = "0"
            My.Settings.Save()
        End If
    End Sub

    Private Sub CheckBox7_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox7.CheckedChanged
        If CheckBox7.Checked = True Then
            My.Settings.RecentPreview = "1"
            My.Settings.Save()
        ElseIf CheckBox7.Checked = False Then
            My.Settings.RecentPreview = "0"
            My.Settings.Save()
        End If
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked = True Then
            My.Settings.RecentPicture = "1"
            My.Settings.Save()
        ElseIf CheckBox6.Checked = False Then
            My.Settings.RecentPicture = "0"
            My.Settings.Save()
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked = True Then
            My.Settings.QuickSnap = "1"
            My.Settings.Save()
        ElseIf CheckBox5.Checked = False Then
            My.Settings.QuickSnap = "0"
            My.Settings.Save()
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton1.Checked = True Then
            My.Settings.Tags = "0"
            My.Settings.Save()
        End If
    End Sub

    Private Sub XylosButton4_Click(sender As Object, e As EventArgs) Handles XylosButton4.Click
        Dim NewUpdate As String = "https://www.dropbox.com/s/giy8eh00zlbc7ga/PutExeDlLinkHere.txt?dl=1"
        WebBrowser1.Navigate(NewUpdate)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub RegionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegionToolStripMenuItem.Click
        Selectregion()
    End Sub

    Private Sub DesktopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DesktopToolStripMenuItem.Click
        TakeDesktop()
    End Sub

    Private Sub OpenFormToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenFormToolStripMenuItem.Click
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        Me.ShowInTaskbar = True
        Me.Opacity = 100
    End Sub

    Private Sub StartTray_CheckedChanged(sender As Object, e As EventArgs) Handles StartTray.CheckedChanged
        If StartTray.Checked = True Then
            My.Settings.TrayIcon = "1"
            My.Settings.Save()
        ElseIf StartTray.Checked = False Then
            My.Settings.TrayIcon = "0"
            My.Settings.Save()
        End If
    End Sub

    Private Sub UploadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UploadToolStripMenuItem.Click
        ImgurUploader.Show()
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Try
            Dim AppData As String = "C:\Users\" & Environment.UserName.ToString & "\AppData\Roaming\Snappie"
            System.IO.Directory.Delete(AppData, True)
            LinkLabel3.Text = "Cleared."
            System.Threading.Thread.Sleep(69)
            LinkLabel3.Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message + "Turn Recent Photo's Off Then Restart Snappie Then click this to delete the recent pictures")
        End Try
    End Sub

    Private Sub CheckBox8_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox8.CheckedChanged
        If CheckBox8.Checked = True Then
            My.Settings.Update = "1"
            My.Settings.Save()
        ElseIf CheckBox8.Checked = False Then
            My.Settings.Update = "0"
            My.Settings.Save()
        End If
    End Sub
End Class