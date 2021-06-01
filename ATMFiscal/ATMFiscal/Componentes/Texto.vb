Public Class Texto
    Inherits TextBox
    Sub New()
        Me.BackColor = Color.AliceBlue
        Me.BorderStyle = FlatStyle.Flat
        Me.Font = New Font("Franklin Gothic Medium", 10, FontStyle.Bold)
    End Sub
    Private Sub Seleccionado(sender As Object, e As EventArgs) Handles Me.GotFocus
        Me.BackColor = Color.Tomato
    End Sub
    Private Sub NoSeleccionado(sender As Object, e As EventArgs) Handles Me.LostFocus
        Me.BackColor = Color.AliceBlue
    End Sub
End Class
