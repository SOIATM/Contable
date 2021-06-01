Imports Telerik.WinControls
Public Class Metodos_de_Pago
    Private Sub Metodos_de_Pago_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Tabla_detalleMetodos_Pago.SqlSelect = "sELECT 	Id_Metodo_Pago,	Clave,	Descripcion FROM dbo.Metodos_de_Pago"
        Me.Tabla_detalleMetodos_Pago.Cargar()
        Me.Tabla_detalleMetodos_Pago.Tabla.Columns(0).Visible = False
    End Sub

    Private Sub Tabla_detalleMetodos_Pago_cmd_Editar(clave As String) Handles Tabla_detalleMetodos_Pago.Cmd_Editar
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim claves As String = InputBox("Teclea la Clave: ", Eventos.titulo_app, Me.Tabla_detalleMetodos_Pago.Registro_columna(1))
        If claves = "" Then
            RadMessageBox.Show("Debe colocar una Clave valida", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If
        Dim Metodo As String = InputBox("Teclea el Metodo de Pago: ", Eventos.titulo_app, Me.Tabla_detalleMetodos_Pago.Registro_columna(2))
        If Metodo = "" Then
            RadMessageBox.Show("Debe colocar el Metodo de Pago valido", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If


        Dim sql As String = "UPDATE dbo.Metodos_de_Pago"
        sql &= " SET  "
        sql &= " 	Clave = '" & claves & "',"
        sql &= " 	Descripcion = '" & Metodo & "'"
        sql &= " where Id_Metodo_Pago= " & clave
        If Eventos.Comando_sql(sql) = 1 Then
            RadMessageBox.Show("Datos Guardados correctamente", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Eventos.Insertar_usuariol("Metodos_de_Pago_E", sql)
        Else
            RadMessageBox.Show("Error al actualizar los datos, revise la información proporcionada...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub

    Private Sub Tabla_detalleMetodos_Pago_cmd_eliminar(clave As String) Handles Tabla_detalleMetodos_Pago.Cmd_eliminar
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If RadMessageBox.Show("Realmente deseas eliminar el Metodos de Pago: " & Me.Tabla_detalleMetodos_Pago.Registro_columna(2) & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If Eventos.Comando_sql("Delete from dbo.Metodos_de_Pago where Id_Metodo_Pago=" & clave) > 0 Then
                RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Eventos.Insertar_usuariol("Metodos_de_Pago_D", "Delete from dbo.Metodos_de_Pago where Id_Metodo_Pago= " & clave & "")
            Else
                RadMessageBox.Show("No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            End If
        End If
    End Sub

    Private Sub Tabla_detalleMetodos_Pago_cmd_Nuevo() Handles Tabla_detalleMetodos_Pago.Cmd_Nuevo
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim claves As String = InputBox("Teclea la Clave: ", Eventos.titulo_app, "")
        If claves = "" Then
            RadMessageBox.Show("Debe colocar una Clave valida", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If
        Dim Metodo As String = InputBox("Teclea el Metodo de Pago: ", Eventos.titulo_app, "")
        If Metodo = "" Then
            RadMessageBox.Show("Debe colocar el Metodo de Pago valido", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If

        Dim sql As String = "INSERT INTO dbo.Metodos_de_Pago"
        sql &= "("
        sql &= " 	Clave,"
        sql &= " 	Descripcion"
        sql &= " 	)"
        sql &= " VALUES "
        sql &= " 	("
        sql &= " 	'" & claves & "',"
        sql &= " 	'" & Metodo & "'"
        sql &= " 	)"
        If Eventos.Comando_sql(sql) = 1 Then
            RadMessageBox.Show("Datos Guardados correctamente", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Eventos.Insertar_usuariol("Metodos_de_Pago_I", sql)
        Else
            RadMessageBox.Show("Error al actualizar los datos, revise la información proporcionada...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub

    Private Sub Tabla_detalleMetodos_Pago_Cerrar() Handles Tabla_detalleMetodos_Pago.Cerrar
        Me.Close()
    End Sub

    'Private Sub CmdManual_Click(sender As Object, e As EventArgs) Handles CmdManual.Click
    '    Eventos.Abrir_Manual("Métodos de Pago")
    'End Sub
End Class