Imports Telerik.WinControls
Public Class LiberarCarga
	
	Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		If Me.lstCliente.SelectText <> "" Then
			If radMessageBox.Show("La Empresa  " & Me.lstCliente.SelectText & " es correcta?",Eventos.versionDB, MessageBoxButtons.YesNo, radMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
				Eventos.LiberarCarga(Eventos.Sql_hoy(Me.DtInicio.Value), Eventos.Sql_hoy(Dtfin.Value), Me.ChkComplementos.Checked, Me.ChkFacturas.Checked, 1, Eventos.Bool2(Me.RadEmitidas.Checked))
				radMessageBox.Show("Proceso terminado", Eventos.titulo_app, MessageBoxButtons.OK, radMessageIcon.Info)
			End If
		Else
			radMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, radMessageIcon.Info)
		End If

	End Sub

	Private Sub LiberarCarga_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Me.lstCliente.Cargar(" SELECT DISTINCT Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.lblusuario.Text & "%')")
		Me.lstCliente.SelectItem = 1
	End Sub

	Private Sub CmdPorUUID_Click(sender As Object, e As EventArgs) Handles CmdPorUUID.Click
        Eventos.Abrir_form(Liberar_UUID)
    End Sub

    Private Sub CmdCerrar_Click(sender As Object, e As EventArgs) Handles CmdCerrar.Click
        Me.Close()
    End Sub
End Class