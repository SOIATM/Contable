Imports Telerik.WinControls
Public Class LiberadorPolizasEgresos
	Public Event Registro(ByVal clave As String)
	Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles CmdCerrar.Click
		Me.Close()
	End Sub

	Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
		If Me.TablaImportar.Rows.Count > 0 Then
			Limpia()
			Me.lstCliente.SelectText = ""
			Me.TxtFiltro.Text = ""
			Me.TxtFiltro.Text = ""
		End If
	End Sub
	Private Sub Limpia()
		Me.TablaImportar.Rows.Clear()
	End Sub
	Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdImportar.Click
		Limpia()
		Dim sql As String = ""
		If Me.lstCliente.SelectText <> "" Then

			sql = "SELECT XmlAuditados.UUID,XmlAuditados.Orden,XmlAuditados.Id_Poliza_Tusa, Polizas.Id_Poliza, Polizas.ID_anio , Polizas.ID_mes ,Polizas.Num_Pol
                        FROM XmlAuditados left JOIN Polizas ON Polizas.Id_Poliza = XmlAuditados.Id_PolizaS 
                        WHERE XmlAuditados.Fecha >= " & Eventos.Sql_hoy(Me.DtInicio.Value) & " AND  XmlAuditados.Fecha <=" & Eventos.Sql_hoy(Dtfin.Value) & " AND XmlAuditados.Id_Empresa = " & Me.lstCliente.SelectItem & "    "


			Dim ds As DataSet = Eventos.Obtener_DS(sql)
			If ds.Tables(0).Rows.Count > 0 Then
				Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count
				For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
					Me.TablaImportar.Item(UUID.Index, i).Value = Trim(ds.Tables(0).Rows(i)("UUID"))
                    Me.TablaImportar.Item(anio.Index, i).Value = Trim(IIf(IsDBNull(ds.Tables(0).Rows(i)("ID_anio")), "", ds.Tables(0).Rows(i)("ID_anio")))
                    Me.TablaImportar.Item(Mes.Index, i).Value = Trim(IIf(IsDBNull(ds.Tables(0).Rows(i)("ID_mes")), "", ds.Tables(0).Rows(i)("ID_mes")))
                    Me.TablaImportar.Item(Num.Index, i).Value = Trim(IIf(IsDBNull(ds.Tables(0).Rows(i)("Num_Pol")), "", ds.Tables(0).Rows(i)("Num_Pol")))
                    Me.TablaImportar.Item(Pol.Index, i).Value = Trim(IIf(IsDBNull(ds.Tables(0).Rows(i)("Id_Poliza")), "", ds.Tables(0).Rows(i)("Id_Poliza")))
					Me.TablaImportar.Item(PolT.Index, i).Value = Trim(IIf(IsDBNull(ds.Tables(0).Rows(i)("Id_Poliza_Tusa")), "", ds.Tables(0).Rows(i)("Id_Poliza_Tusa")))
					Me.TablaImportar.Item(OT.Index, i).Value = Trim(IIf(IsDBNull(ds.Tables(0).Rows(i)("Orden")), "", ds.Tables(0).Rows(i)("Orden")))
				Next
			End If
		End If
	End Sub


	Private Sub CmdEliminar_Click(sender As Object, e As EventArgs) Handles CmdEliminar.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
			If Me.lstCliente.SelectText <> "" Then
				If Me.TablaImportar.Item(Verif.Index, i).Value = True Then
					Eventos.LiberarCarga_UUIDComp(Me.TablaImportar.Item(UUID.Index, i).Value, Me.lstCliente.SelectItem, Me.TablaImportar.Item(Pol.Index, i).Value)
				End If
			Else
				RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
			End If
		Next
		RadMessageBox.Show("Proceso terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
		Me.CmdImportar.PerformClick()
	End Sub

    Private Sub Liberar_UUID_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Eventos.DiseñoTabla(Me.TablaImportar)
        Me.lstCliente.Cargar(" SELECT DISTINCT Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = 1
    End Sub

    Private Sub TxtFiltro_TextChanged(sender As Object, e As EventArgs) Handles TxtFiltro.TextChanged
        Dim sql As String = ""

        If Me.TablaImportar.Rows.Count > 0 Then

            Me.TablaImportar.Rows.Clear()

            If Me.TxtFiltro.Text <> "" Then
                If Me.LblFiltro.Text = "Poliza ATM" Then
                    Me.TxtFiltro.Text = "Polizas.Id_poliza"
                ElseIf Me.LblFiltro.Text = "Polzia en Tusa" Then
                    Me.TxtFiltro.Text = "XmlAuditados.Id_Poliza_Tusa"
                ElseIf Me.LblFiltro.Text = "Seleccion" Then
                    Exit Sub
                End If

                sql = "SELECT XmlAuditados.UUID,XmlAuditados.Orden,XmlAuditados.Id_Poliza_Tusa, Polizas.Id_Poliza, Polizas.ID_anio , Polizas.ID_mes ,Polizas.Num_Pol 
                        FROM XmlAuditados left JOIN Polizas ON Polizas.Id_Poliza = XmlAuditados.Id_Polizas 
                        WHERE " & Me.LblFiltro.Text & " like '%" & Me.TxtFiltro.Text & "%' and XmlAuditados.Fecha >= " & Eventos.Sql_hoy(Me.DtInicio.Value) & " AND  XmlAuditados.Fecha <=" & Eventos.Sql_hoy(Dtfin.Value) & " AND XmlAuditados.Id_Empresa = " & Me.lstCliente.SelectItem & "    "
                Dim ds As DataSet = Eventos.Obtener_DS(sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.TablaImportar.Item(UUID.Index, i).Value = Trim(ds.Tables(0).Rows(i)("UUID"))
                        Me.TablaImportar.Item(anio.Index, i).Value = Trim(IIf(IsDBNull(ds.Tables(0).Rows(i)("ID_anio")), "", ds.Tables(0).Rows(i)("ID_anio")))
                        Me.TablaImportar.Item(Mes.Index, i).Value = Trim(IIf(IsDBNull(ds.Tables(0).Rows(i)("ID_mes")), "", ds.Tables(0).Rows(i)("ID_mes")))
                        Me.TablaImportar.Item(Num.Index, i).Value = Trim(IIf(IsDBNull(ds.Tables(0).Rows(i)("Num_Pol")), "", ds.Tables(0).Rows(i)("Num_Pol")))
                        Me.TablaImportar.Item(Pol.Index, i).Value = Trim(IIf(IsDBNull(ds.Tables(0).Rows(i)("Id_Poliza")), "", ds.Tables(0).Rows(i)("Id_Poliza")))
                        Me.TablaImportar.Item(PolT.Index, i).Value = Trim(IIf(IsDBNull(ds.Tables(0).Rows(i)("Id_Poliza_Tusa")), "", ds.Tables(0).Rows(i)("Id_Poliza_Tusa")))
                        Me.TablaImportar.Item(OT.Index, i).Value = Trim(IIf(IsDBNull(ds.Tables(0).Rows(i)("Orden")), "", ds.Tables(0).Rows(i)("Orden")))
                    Next
                End If

            End If
        End If
    End Sub

	Private Sub TablaImportar_Click(sender As Object, e As EventArgs) Handles TablaImportar.Click
		If Me.TablaImportar.RowCount > 0 Then
			RaiseEvent Registro(Me.TablaImportar.Item(0, Me.TablaImportar.CurrentCell.RowIndex).Value.ToString)
            Me.LblFiltro.Text = Me.TablaImportar.Columns(Me.TablaImportar.CurrentCell.ColumnIndex).HeaderText
        End If
	End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
            If Me.lstCliente.SelectText <> "" Then
                If Me.TablaImportar.Item(Verif.Index, i).Value = True Then
                    Eventos.LiberarComprobacion(Me.TablaImportar.Item(UUID.Index, i).Value, Me.lstCliente.SelectItem)
                End If
            Else
                RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            End If
        Next
        RadMessageBox.Show("Proceso terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Me.CmdImportar.PerformClick()
    End Sub
End Class
