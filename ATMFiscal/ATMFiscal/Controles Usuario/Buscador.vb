Public Class Buscador
    Public Event Buscar()
    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdBuscar.Click
        RaiseEvent Buscar()
    End Sub
End Class
