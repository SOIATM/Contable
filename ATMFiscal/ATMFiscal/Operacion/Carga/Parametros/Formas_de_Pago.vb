Imports Telerik.WinControls
Public Class Formas_de_Pago
    Private Sub Formas_de_Pago_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Tabla_detalleFormasPago.SqlSelect = "SELECT 	Id_Forma_Pago,	Clave,	Descripcion FROM dbo.Forma_de_Pago"
        Me.Tabla_detalleFormasPago.Cargar()
        Me.Tabla_detalleFormasPago.Tabla.Columns(0).Visible = False
    End Sub

    Private Sub Tabla_detalleFormasPago_cmd_Editar(clave As String) Handles Tabla_detalleFormasPago.Cmd_Editar
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim claves As String = InputBox("Teclea la Clave:", Eventos.titulo_app, Me.Tabla_detalleFormasPago.Registro_columna(1))
        If claves = "" Then
            RadMessageBox.Show("Debe colocar una clave valida", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If
        Dim descripcion As String = InputBox("Teclea el  Nombre de La Forma de Pago:", Eventos.titulo_app, Me.Tabla_detalleFormasPago.Registro_columna(2))
        If descripcion = "" Then
            RadMessageBox.Show("Debe colocar un Nombre valido", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If
        Dim sql As String = "UPDATE dbo.Forma_de_Pago"
        sql &= " SET   "
        sql &= "	Clave  = '" & claves & "'," '@Clave_tipo
        sql &= "	Descripcion = '" & descripcion & "' " '@tipo
        sql &= " where Id_Forma_Pago= " & clave
        If Eventos.Comando_sql(sql) = 1 Then
            RadMessageBox.Show("Datos Guardados correctamente", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

        Else
            RadMessageBox.Show("Error al actualizar los datos, revise la información proporcionada...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub

    Private Sub Tabla_detalleFormasPago_cmd_eliminar(clave As String) Handles Tabla_detalleFormasPago.Cmd_eliminar
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If RadMessageBox.Show("Realmente deseas eliminar la Forma de Pago: " & Me.Tabla_detalleFormasPago.Registro_columna(2) & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If Eventos.Comando_sql("Delete from dbo.Forma_de_Pago where Id_Forma_Pago=" & clave) > 0 Then
                RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Eventos.Insertar_usuariol("Forma_de_Pago_D", "Delete from dbo.Forma_de_Pago where Id_Forma_Pago= " & clave & "")
            Else
                RadMessageBox.Show("No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            End If
        End If
    End Sub

    Private Sub Tabla_detalleFormasPago_cmd_Nuevo() Handles Tabla_detalleFormasPago.Cmd_Nuevo
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim claves As String = InputBox("Teclea la Clave:", Eventos.titulo_app, "")
        If claves = "" Then
            RadMessageBox.Show("Debe colocar una clave valida", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If
        Dim descripcion As String = InputBox("Teclea el Nombre de La Forma de Pago:", Eventos.titulo_app, "")
        If descripcion = "" Then
            RadMessageBox.Show("Debe colocar un Nombre valido", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If
        Dim sql As String = "  INSERT INTO dbo.Forma_de_Pago"
        sql &= "("
        sql &= " 	Clave ,"
        sql &= " 	Descripcion"
        sql &= " 	)"
        sql &= " VALUES "
        sql &= "("
        sql &= " 	'" & claves & "'," '
        sql &= " 	'" & descripcion & "'" '
        sql &= " 	)"
        If Eventos.Comando_sql(sql) = 1 Then
            RadMessageBox.Show("Datos Guardados correctamente", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Eventos.Insertar_usuariol("Forma_de_Pago_I", sql)
        Else
            RadMessageBox.Show("Error al actualizar los datos, revise la información proporcionada...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub

    Private Sub Tabla_detalleFormasPago_Cerrar() Handles Tabla_detalleFormasPago.Cerrar
        Me.Close()
    End Sub

    'Private Sub CmdManual_Click(sender As Object, e As EventArgs) Handles CmdManual.Click
    '    Eventos.Abrir_Manual("Formas de Pago")
    'End Sub
End Class
