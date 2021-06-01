Imports Telerik.WinControls
Public Class Control_Tasas
    Private Sub Control_Tasas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Tabla_Tasas.SqlSelect = "SELECT 	Id_Tasa,Descripcion,Valor FROM dbo.Tasas"
        Me.Tabla_Tasas.Cargar()
        Me.Tabla_Tasas.Tabla.Columns(0).Visible = False
    End Sub

    Private Sub Tabla_Tasas_cmd_Editar(clave As String) Handles Tabla_Tasas.Cmd_Editar
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim claves As String = InputBox("Teclea la Descripcion:", Eventos.titulo_app, Me.Tabla_Tasas.Registro_columna(1))
        If claves = "" Then
            RadMessageBox.Show("Debe colocar una Descripcion valida", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If
        Dim descripcion As Decimal = InputBox("Teclea el  Valor de la Tasa : " & claves & "", Eventos.titulo_app, Me.Tabla_Tasas.Registro_columna(2))
        If descripcion = Nothing Then
            RadMessageBox.Show("Debe colocar un Valor valido", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If
        Dim sql As String = "UPDATE dbo.Tasas"
        sql &= " SET   "
        sql &= "	Descripcion  = '" & claves & "'," '@Descripcion
        sql &= "	Valor = " & descripcion & " " '@Valor
        sql &= " where Id_Tasa= " & clave
        If Eventos.Comando_sql(sql) = 1 Then
            RadMessageBox.Show("Datos Guardados correctamente", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Eventos.Insertar_usuariol("Tasas_E", sql)
        Else
            RadMessageBox.Show("Error al actualizar los datos, revise la información proporcionada...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub

    Private Sub Tabla_Tasas_cmd_eliminar(clave As String) Handles Tabla_Tasas.Cmd_eliminar
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If RadMessageBox.Show("Realmente deseas eliminar la Tasa: " & Me.Tabla_Tasas.Registro_columna(2) & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If Eventos.Comando_sql("Delete from dbo.Tasas where Id_Tasa=" & clave) > 0 Then
                RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Eventos.Insertar_usuariol("Tasas_D", "Delete from dbo.Tasas where Id_Tasa= " & clave & "")
            Else
                RadMessageBox.Show("No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            End If
        End If
    End Sub

    Private Sub Tabla_Tasas_cmd_Nuevo() Handles Tabla_Tasas.Cmd_Nuevo
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim claves As String = InputBox("Teclea la Descripcion:", Eventos.titulo_app, "")
        If claves = "" Then
            RadMessageBox.Show("Debe colocar una Descripcion valida", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If
        Dim descripcion As Decimal = InputBox("Teclea el  Valor de la Tasa : " & claves & "", Eventos.titulo_app, 0)
        If descripcion = Nothing Then
            RadMessageBox.Show("Debe colocar un Valor valido", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If
        Dim sql As String = "  INSERT INTO dbo.Tasas"
        sql &= "("
        sql &= " 	Descripcion ,"
        sql &= " 	Valor"
        sql &= " 	)"
        sql &= " VALUES "
        sql &= "("
        sql &= " 	'" & claves & "'," '
        sql &= " 	" & descripcion & "" '
        sql &= " 	)"
        If Eventos.Comando_sql(sql) = 1 Then
            RadMessageBox.Show("Datos Guardados correctamente", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Eventos.Insertar_usuariol("Forma_de_Pago_I", sql)
        Else
            MessageBox.Show("Error al actualizar los datos, revise la información proporcionada...", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub Tabla_Tasas_Cerrar() Handles Tabla_Tasas.Cerrar
        Me.Close()
    End Sub

    'Private Sub CmdManual_Click(sender As Object, e As EventArgs) Handles CmdManual.Click
    '    ' Eventos.Abrir_Manual("Tasas")
    'End Sub


End Class