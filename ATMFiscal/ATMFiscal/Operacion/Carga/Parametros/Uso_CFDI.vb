Imports Telerik.WinControls
Public Class Uso_CFDI
    Private Sub Uso_CFDI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sql As String = "SELECT  Id_Uso_CFDI,	Clave,	Descripcion FROM dbo.Uso_CFDI"
        Me.TablaUsoCFDI.SqlSelect = sql
        Me.TablaUsoCFDI.Cargar()
        Me.TablaUsoCFDI.Tabla.Columns(0).Visible = False
    End Sub

    Private Sub TablaUsoCFDI_cmd_Editar(clave As String) Handles TablaUsoCFDI.Cmd_Editar
        Dim claves As String = InputBox("Teclea la Clave:", Eventos.titulo_app, Me.TablaUsoCFDI.Registro_columna(1))
        If claves = "" Then
            MessageBox.Show("Debe colocar una clave valida", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim descripcion As String = InputBox("Teclea la Descripcion:", Eventos.titulo_app, Me.TablaUsoCFDI.Registro_columna(2))
        If descripcion = "" Then
            MessageBox.Show("Debe colocar un Nombre valido", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim sql As String = " UPDATE dbo.Uso_CFDI SET  	Clave ='" & claves & "', 	Descripcion = '" & descripcion & "'     where Id_Uso_CFDI= " & clave & " "

        If Eventos.Comando_sql(sql) = 1 Then
            MessageBox.Show("Datos Guardados correctamente", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Eventos.Insertar_usuariol("Uso_CFDI_E", sql)
        Else
            MessageBox.Show("Error al actualizar los datos, revise la información proporcionada...", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub TablaUsoCFDI_cmd_eliminar(clave As String) Handles TablaUsoCFDI.Cmd_eliminar
        If MessageBox.Show("Realmente deseas eliminar el Uso: " & Me.TablaUsoCFDI.Registro_columna(2) & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If Eventos.Comando_sql("Delete from dbo.Uso_CFDI where Id_Uso_CFDI=" & clave) > 0 Then
                MessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Eventos.Insertar_usuariol("Uso_CFDI_D", "Delete from dbo.Uso_CFDI where Id_Uso_CFDI= " & clave & "")
            Else
                MessageBox.Show("No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub TablaUsoCFDI_cmd_Nuevo() Handles TablaUsoCFDI.Cmd_Nuevo
        Dim claves As String = InputBox("Teclea la Clave:", Eventos.titulo_app, "")
        If claves = "" Then
            MessageBox.Show("Debe colocar una clave valida", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim descripcion As String = InputBox("Teclea la Descripcion:", Eventos.titulo_app, "")
        If descripcion = "" Then
            MessageBox.Show("Debe colocar un Nombre valido", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim SQL As String = "INSERT INTO dbo.Uso_CFDI 	( 	Clave,	Descripcion	) VALUES 	(	 '" & claves & "',   '" & descripcion & "'	)"
        If Eventos.Comando_sql(SQL) = 1 Then
            MessageBox.Show("Datos Guardados correctamente", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Eventos.Insertar_usuariol("Uso_CFDI_I", SQL)
        Else
            MessageBox.Show("Error al actualizar los datos, revise la información proporcionada...", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub TablaUsoCFDI_Cerrar() Handles TablaUsoCFDI.Cerrar
        Me.Close()
    End Sub

    'Private Sub CmdManual_Click(sender As Object, e As EventArgs) Handles CmdManual.Click
    '    Eventos.Abrir_Manual("Uso de CFDI")
    'End Sub


End Class