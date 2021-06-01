Imports Telerik.WinControls
Public Class Tipo_Polizas
	Private Sub Tipo_Polizas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Me.Tabla_detallePolizas.SqlSelect = "SELECT  	Id_Tipo_poliza, 	Clave_Tipo, 	Tipo FROM dbo.Tipo_Poliza "
		Me.Tabla_detallePolizas.Cargar()
		Me.Tabla_detallePolizas.Tabla.Columns(0).Visible = False
	End Sub

    Private Sub Tabla_detallePolizas_cmd_Editar(clave As String) Handles Tabla_detallePolizas.Cmd_Editar

        Dim claves As String = InputBox("Teclea la Clave:", Eventos.titulo_app, Me.Tabla_detallePolizas.Registro_columna(1))
        If claves = "" Then
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "Debe colocar una clave valida", MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()
            Exit Sub
        End If
        Dim descripcion As String = InputBox("Teclea el Nombre Tipo:", Eventos.titulo_app, Me.Tabla_detallePolizas.Registro_columna(2))
        If descripcion = "" Then
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "Debe colocar un Nombre valido", MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()
            Exit Sub

        End If
        Dim sql As String = "UPDATE dbo.Tipo_Poliza"
        sql &= " SET   "
        sql &= "	Clave_Tipo = '" & Trim(claves) & "',"
        sql &= "	Tipo = '" & descripcion & "' "
        sql &= " where Id_Tipo_poliza= " & clave

    End Sub

    Private Sub Tabla_detallePolizas_cmd_eliminar(clave As String) Handles Tabla_detallePolizas.Cmd_eliminar

	End Sub

	Private Sub Tabla_detallePolizas_cmd_Nuevo() Handles Tabla_detallePolizas.Cmd_Nuevo
		Dim claves As String = InputBox("Teclea la Clave:", Eventos.titulo_app, "")
		If claves = "" Then
			RadMessageBox.SetThemeName("MaterialBlueGrey")
			Dim Ms As DialogResult = RadMessageBox.Show(Me, "Debe colocar una clave valida", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
			Me.Text = Ms.ToString()
			Exit Sub
		End If
		Dim descripcion As String = InputBox("Teclea el Nombre Tipo:", Eventos.titulo_app, "")
		If descripcion = "" Then
			MessageBox.Show("Debe colocar un Nombre valido", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Error)
			Exit Sub

			RadMessageBox.SetThemeName("MaterialBlueGrey")
			Dim Ms As DialogResult = RadMessageBox.Show(Me, "Debe colocar un Nombre valido", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
			Me.Text = Ms.ToString()
		End If
		Dim sql As String = "  INSERT INTO dbo.Tipo_Poliza"
		sql &= "("
		sql &= " 	Clave_Tipo,"
		sql &= " 	Tipo"
		sql &= " 	)"
		sql &= " VALUES "
		sql &= "("
		sql &= " 	'" & claves & "'," '
		sql &= " 	'" & descripcion & "'" '
		sql &= " 	)"

    End Sub

	Private Sub Tabla_detallePolizas_Cerrar() Handles Tabla_detallePolizas.Cerrar
		Me.Close()

	End Sub

End Class