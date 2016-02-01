Public Class ShowMoreRecent

    Private Sub ShowMoreRecent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox1.Image = NewForm.PictureBox5.Image
        PictureBox2.Image = NewForm.PictureBox6.Image
        PictureBox3.Image = NewForm.PictureBox7.Image
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If PictureBox2.BackColor = Color.SteelBlue Then
            PictureBox2.BackColor = Color.Transparent
            PictureBox1.BackColor = Color.SteelBlue
        Else
            PictureBox1.BackColor = Color.SteelBlue
        End If
    End Sub
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If PictureBox1.BackColor = Color.SteelBlue Then
            PictureBox1.BackColor = Color.Transparent
            PictureBox2.BackColor = Color.SteelBlue
        Else
            PictureBox2.BackColor = Color.SteelBlue
        End If
    End Sub

    Private Sub ViewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewToolStripMenuItem.Click
        If PictureBox1.BackColor = Color.SteelBlue Then
            PreviewRecent.Show()
            PreviewRecent.PictureBox1.Image = PictureBox1.Image
        ElseIf PictureBox2.BackColor = Color.SteelBlue Then
            PreviewRecent.Show()
            PreviewRecent.PictureBox1.Image = PictureBox2.Image
        End If
    End Sub
End Class