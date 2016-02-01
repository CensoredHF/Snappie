Imports System.IO

Public Class Form1

    Public Declare Function GetAsyncKeyState Lib "user32.dll" (ByVal key As Integer) As Integer

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                TextBox1.Text = OpenFileDialog1.FileName
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub checktart()
        If CheckBox2.Checked = True Then
            Me.WindowState = FormWindowState.Minimized
            Me.Visible = False
            Me.Hide()
            ShowInTaskbar = False
            NotifyIcon1.Visible = True
        ElseIf CheckBox2.Checked = False Then
            ' dont go 
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        checktart()

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.ShowInTaskbar = False
        Me.Hide()
        System.Threading.Thread.Sleep(79)
        CaptureS.Show() ' Start the capturing.
    End Sub
    Sub PlaySystemSound()
        Try
            Dim tempDir As String = "C:\Users\" & Environment.UserName.ToString & "\AppData\Local\Temp\capture.mp3"
            My.Computer.FileSystem.WriteAllBytes(tempDir, My.Resources.ResourceManager.GetObject("Capture"), False)

            Dim capturesound As String = tempDir
            Me.AxWindowsMediaPlayer1.URL = capturesound
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If GetAsyncKeyState(Keys.PrintScreen) <> 0 Then
            MessageBox.Show("PrintScreen Pressed")
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        CaptureS.Close()
        Form2.Close()
        Me.Close()
    End Sub

    Private Sub startsave()
        Me.WindowState = FormWindowState.Normal
        Me.ShowInTaskbar = True
        SaveFileDialog1.Filter = "Bmp Files (*.Bmp*)|*.Bmp"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK _
        Then
            Desktop.Image.Save(SaveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp)
        End If
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        PlaySystemSound()
        Me.WindowState = FormWindowState.Minimized
        Me.ShowInTaskbar = False
        System.Threading.Thread.Sleep(79)


        ' startsave()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        AdvancedSettings.Show()
    End Sub
End Class
