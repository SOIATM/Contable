Imports Telerik.WinControls
Imports Microsoft.Office.Interop
Public Class Carga_Masiva_Personal
	Dim tiene As Boolean = False
	Dim clave As String = ""
	Dim activo As Boolean
	Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
		Me.Close()
	End Sub

	Private Sub CmdLimpiarN_Click(sender As Object, e As EventArgs) Handles CmdLimpiarN.Click
		If Me.TablaImportar.Rows.Count > 0 Then
			Limpia()
			Me.lstCliente.SelectItem = Inicio.Clt
			Me.lblRegistros.Text = "Total de Registros: 0"
		End If
	End Sub
	Private Sub Limpia()
		Me.TablaImportar.Columns.Clear()
	End Sub

	Private Sub CmdBuscarN_Click(sender As Object, e As EventArgs) Handles CmdBuscarN.Click
		If Me.lstCliente.SelectText <> "" Then

			Eventos.LlenarDataGrid_DS(Eventos.CargarExcelXMLDatosPersonalClientes("Datos"), Me.TablaImportar)
			Try
				Me.TablaImportar.Rows.RemoveAt(0)
				If Me.TablaImportar.Rows.Count > 0 Then
					Me.lblRegistros.Text = Me.lblRegistros.Text & " " & Me.TablaImportar.Rows.Count
				End If
				For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
					If Me.TablaImportar.Item(13, i).Value.ToString Is Nothing Or Me.TablaImportar.Item(13, i).Value.ToString = "" Then
						Me.TablaImportar.Item(13, i).Value = Calcula_Guia()
					End If
				Next
			Catch ex As Exception

			End Try

		Else

		End If
	End Sub


	Private Sub Cargar_clientes()
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		Me.lstCliente.Cargar("SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                                FROM     Empresa INNER JOIN
                                Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                                Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                                Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                                Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                                WHERE  (Usuarios.Usuario LIKE '%" & Inicio.LblUsuario.Text & "%')")
		Me.lstCliente.SelectItem = Inicio.Clt

		Me.LstDelegacion.Cargar("SELECT Delegaciones_IMSS.Id_Delegacion_IMSS, Delegaciones_IMSS.Delegacion FROM     Delegaciones_IMSS")

		Dim sql As String = "SELECT Delegaciones_IMSS.Id_Delegacion_IMSS  , Sub_Delegaciones_IMSS.Id_Sub_Delegacion_IMSS,Sub_Delegaciones_IMSS.Clave  FROM Delegaciones_IMSS 
                                INNER JOIN Sub_Delegaciones_IMSS ON Sub_Delegaciones_IMSS.Id_Delegacion_IMSS =Delegaciones_IMSS.Id_Delegacion_IMSS 
                                INNER JOIN Clientes_Delegaciones ON Clientes_Delegaciones.Id_Sub_Delegacion_IMSS =Sub_Delegaciones_IMSS.Id_Sub_Delegacion_IMSS 
                                INNER JOIN Empresa ON Empresa.Id_Empresa = Clientes_Delegaciones.Id_Empresa 
                                WHERE  Clientes_Delegaciones.Id_Empresa = " & Inicio.Clt & ""
		Dim ds As DataSet = Eventos.Obtener_DS(sql)
		If ds.Tables(0).Rows.Count > 0 Then
			Me.LstDelegacion.SelectItem = ds.Tables(0).Rows(0)(0)
			Me.LstSubDelegacion.Cargar("SELECT Sub_Delegaciones_IMSS.Id_Sub_Delegacion_IMSS, Sub_Delegaciones_IMSS.Sub_Delegacion   
                                FROM Sub_Delegaciones_IMSS 
                                INNER JOIN Delegaciones_IMSS ON Delegaciones_IMSS.Id_Delegacion_IMSS =Sub_Delegaciones_IMSS.Id_Delegacion_IMSS 
                                WHERE Delegaciones_IMSS.Id_Delegacion_IMSS =" & ds.Tables(0).Rows(0)(0) & "")
			Me.LstSubDelegacion.SelectItem = ds.Tables(0).Rows(0)(1)
			clave = ds.Tables(0).Rows(0)(2)
			tiene = True
		Else
			tiene = False
			Me.LstDelegacion.SelectText = ""
            RadMessageBox.Show("Selecciona parametros de Delegacion y Sub-Delegacion a la Empresa " & Me.lstCliente.SelectText & "...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If

		Me.LstGuia.Cargar("SELECT 400,400 UNION SELECT 371,371 UNION SELECT 373,373 UNION SELECT 374,374 UNION SELECT  375, 374 UNION SELECT  376,376 UNION SELECT  397,397 UNION SELECT 405 , 405 UNION SELECT 406 , 406")
		Me.LstGuia.SelectText = "400"
	End Sub

	Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
        If RadMessageBox.Show("La empresa " & Me.lstCliente.SelectText & " es correcta?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If Me.TablaImportar.Rows.Count > 0 Then
                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = Me.TablaImportar.RowCount - 1
                Me.Cursor = Cursors.AppStarting
                For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                    If Me.TablaImportar.Item(1, i).Value.ToString() <> "" Then
                        Guardar_Masivo(i)
                    End If
                    frm.Barra.Value = i
                Next
                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Me.Cursor = Cursors.Arrow
                Me.Close()
            End If
        End If
    End Sub

	Private Sub Guardar_Masivo(ByVal i As Integer)
		RadMessageBox.SetThemeName("MaterialBlueGrey")

		If NO_IMSS(Trim(Me.TablaImportar.Item(6, i).Value)) = True Then

			Dim sql As String = " INSERT INTO dbo.Personal_Clientes
	    (
	     ID_matricula, Ap_paterno, Ap_materno, Nombres, Registro_Patronal, Dig_Verificador_RP, No_IMSS, Dig_Verif_IMSS, Salario_Base,
	    Tipo_Trabajador, Tipo_Salario, Sem_Jornada_Reducida, Unidad_Medicina_Familiar, Guia, CURP, Fecha_alta,  Id_Empresa 	    )
             VALUES 
	    (
     "
			sql &= "  " & IIf(IsDBNull(Me.TablaImportar.Item(0, i).Value), "NULL", Me.TablaImportar.Item(0, i).Value) & " , " '@id_matricula
			sql &= "  '" & IIf(IsDBNull(Me.TablaImportar.Item(1, i).Value), "", Me.TablaImportar.Item(1, i).Value) & "', " '@ap_paterno
			sql &= "  '" & IIf(IsDBNull(Me.TablaImportar.Item(2, i).Value), "", Me.TablaImportar.Item(2, i).Value) & "', " '@ap_materno
			sql &= " '" & IIf(IsDBNull(Me.TablaImportar.Item(3, i).Value), "", Me.TablaImportar.Item(3, i).Value) & "', " '@nombres
			sql &= " '" & IIf(IsDBNull(Me.TablaImportar.Item(4, i).Value), "", Me.TablaImportar.Item(4, i).Value) & "', " '@sexo
			sql &= " " & IIf(IsDBNull(Me.TablaImportar.Item(5, i).Value), "NULL", Me.TablaImportar.Item(5, i).Value) & ", " '@fecha_nacimto
			sql &= " '" & IIf(IsDBNull(Me.TablaImportar.Item(6, i).Value), "", Me.TablaImportar.Item(6, i).Value) & "', " '@no_curp
			sql &= " '" & IIf(IsDBNull(Me.TablaImportar.Item(7, i).Value), "", Me.TablaImportar.Item(7, i).Value) & "', " '@folio_elector
			sql &= " " & IIf(IsDBNull(Me.TablaImportar.Item(8, i).Value), 0, Me.TablaImportar.Item(8, i).Value) & ", " '@rfc
			sql &= " '" & IIf(IsDBNull(Me.TablaImportar.Item(9, i).Value), "", Me.TablaImportar.Item(9, i).Value) & "', " '@direccion
			sql &= " '" & IIf(IsDBNull(Me.TablaImportar.Item(10, i).Value), "", Me.TablaImportar.Item(10, i).Value) & "', " '@telefono
			sql &= " '" & IIf(IsDBNull(Me.TablaImportar.Item(11, i).Value), "", Me.TablaImportar.Item(11, i).Value) & "', " '@codigo_postal
			sql &= " '" & IIf(IsDBNull(Me.TablaImportar.Item(12, i).Value), "", Me.TablaImportar.Item(12, i).Value) & "', " '@puesto
			sql &= " '" & IIf(IsDBNull(Me.TablaImportar.Item(13, i).Value), Calcula_Guia(), Me.TablaImportar.Item(13, i).Value) & "', " '@sueldo_promedio
			sql &= " '" & IIf(IsDBNull(Me.TablaImportar.Item(14, i).Value), "NULL", Me.TablaImportar.Item(14, i).Value) & "', " '@fecha_ingreso
			sql &= " " & IIf(IsDBNull(Me.TablaImportar.Item(15, i).Value), "", Eventos.Sql_hoy(Me.TablaImportar.Item(15, i).Value)) & ", " '@no_imss
			sql &= " " & Me.lstCliente.SelectItem & "   " '@Id_Empresa
			sql &= " )"

			If Eventos.Comando_sql(sql) > 0 Then
				Eventos.Insertar_usuariol("InsertaClientPMasivo", sql)
			End If
		Else
			Me.TablaImportar.Item(13, i).Style.BackColor = Color.Red
			RadMessageBox.Show("No se puede Ingresar un  numero de seguridad social duplicado verifique la informacion ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
		End If

	End Sub




	Private Sub Carga_Masiva_Personal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		activo = True
		Cargar_clientes()
		activo = False
		Eventos.DiseñoTabla(Me.TablaImportar)
		Eventos.DiseñoTabla(Me.Tabla)
	End Sub

	Private Sub LstDelegacion_cambio_item(value As String, texto As String) Handles LstDelegacion.Cambio_item
		If activo = False Then
			Me.LstSubDelegacion.Cargar("SELECT Sub_Delegaciones_IMSS.Id_Sub_Delegacion_IMSS, Sub_Delegaciones_IMSS.Sub_Delegacion   
                                FROM Sub_Delegaciones_IMSS 
                                INNER JOIN Delegaciones_IMSS ON Delegaciones_IMSS.Id_Delegacion_IMSS =Sub_Delegaciones_IMSS.Id_Delegacion_IMSS 
                                WHERE Delegaciones_IMSS.Id_Delegacion_IMSS =" & value & "")
			Me.LstSubDelegacion.SelectText = ""
		End If
	End Sub
	Public Function Calcula_Guia()
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		Dim Guia As String = ""

		If clave <> "" Then
			Guia = clave & Trim(Me.LstGuia.SelectText)
		Else
            RadMessageBox.Show("Selecciona parametros de Delegacion y Sub-Delegacion la Empresa " & Me.lstCliente.SelectText & "...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
		Return Guia
	End Function

	Private Sub CmdAsignaDelegacion_Click(sender As Object, e As EventArgs) Handles CmdAsignaDelegacion.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		Dim sql As String = ""
		If Me.LstDelegacion.SelectText <> "" And Me.LstSubDelegacion.SelectText <> "" Then
			If tiene = True Then
				sql = "UPDATE dbo.Clientes_Delegaciones SET 	Id_Sub_Delegacion_IMSS = " & Me.LstSubDelegacion.SelectItem & " WHERE Id_Empresa = " & Me.lstCliente.SelectItem & " "
			Else
				sql = "INSERT INTO dbo.Clientes_Delegaciones ( Id_Empresa, Id_Sub_Delegacion_IMSS ) VALUES 	(" & Me.lstCliente.SelectItem & "," & Me.LstSubDelegacion.SelectItem & ")"
			End If
			If Eventos.Comando_sql(sql) > 0 Then
				RadMessageBox.Show("Informacion Guardada...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
				Cargar_clientes()
			Else
				RadMessageBox.Show("No se pudo guardar la informacion...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)

			End If
		End If
	End Sub

	Private Sub TablaImportar_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles TablaImportar.CellClick
		Try


			Dim columna As Integer = Me.TablaImportar.CurrentCell.ColumnIndex
			Dim Nombre As String
			Nombre = Me.TablaImportar.Columns.Item(Me.TablaImportar.CurrentCell.ColumnIndex).Name.ToString
			If Me.Tabla.Rows.Count > 0 Then
				Me.Tabla.Rows.Clear()
			End If

			Select Case Nombre
				Case "Tipo_Salario"
					Me.Tabla.Rows.Add("0", "Salario Fijo")
					Me.Tabla.Rows.Add("1", "Salario Variable")
					Me.Tabla.Rows.Add("2", "Salario Mixto")


					Me.LstTexto.Cargar(" SELECT  0, 'Salario Fijo'  UNION SELECT  1 , 'Salario Variable' UNION SELECT 2 , 'Salario Mixto'")
					Me.LstTexto.SelectText = ""

				Case "Tipo_Trabajador"
					Me.Tabla.Rows.Add("1", "Trabajador Permanente")
					Me.Tabla.Rows.Add("2", "Trabajador Eventual Ciudad")
					Me.Tabla.Rows.Add("3", "Trabajador Eventual Construccion")
					Me.Tabla.Rows.Add("4", "Trabajador Eventual Campo")


					Me.LstTexto.Cargar(" SELECT  1, 'Trabajador Permanente'  UNION SELECT  2 , 'Trabajador Eventual Ciudad' UNION SELECT 3 , 'Trabajador Eventual Construccion' UNION SELECT 4 , 'Trabajador Eventual Campo'")
					Me.LstTexto.SelectText = ""
				Case "Sem_Jornada_Reducida"
					Me.Tabla.Rows.Add("0", "Jornada Normal")
					Me.Tabla.Rows.Add("1", "Un Dia")
					Me.Tabla.Rows.Add("2", "Dos Dias")
					Me.Tabla.Rows.Add("3", "Tres Dias")
					Me.Tabla.Rows.Add("4", "Cuatro Dias")
					Me.Tabla.Rows.Add("5", "Cinco Dias")
					Me.Tabla.Rows.Add("6", "Jornada Reducida")
					Me.LstTexto.Cargar(" SELECT  0, 'Jornada Normal'  UNION SELECT  1 , 'Un Dia' UNION SELECT 2 ,'Dos Dias' UNION SELECT 3 , 'Tres Dias' UNION SELECT 4 , 'Cuatro Dias' UNION SELECT 5 , 'Cinco Dias' UNION SELECT 6 , 'Jornada Reducida'")
					Me.LstTexto.SelectText = ""
				Case "Guia"
					Me.Tabla.Rows.Add("371", "Revisión por Art. 18")
					Me.Tabla.Rows.Add("373", "Visita Integral Art. 46")
					Me.Tabla.Rows.Add("374", "Revisión de Gabinete Art. 48")
					Me.Tabla.Rows.Add("375", "Revisión por Art. 12 A")
					Me.Tabla.Rows.Add("376", "Visita Específica Art. 46")
					Me.Tabla.Rows.Add("397", "Revisión Ágil Art. 17")
					Me.Tabla.Rows.Add("400", "Dispositivos Magnéticos (Reingresos, Modificaciones de Salario y Bajas)")
					Me.Tabla.Rows.Add("405", "Corrección por Invitación, Corrección Espontánea, SATICA o SATICB.")
					Me.Tabla.Rows.Add("406", "Programa de Dictamen (Obligado y Voluntario), Procedimiento de Revisión Interna (RO y RV)")

			End Select
		Catch ex As Exception

		End Try
	End Sub
	Private Function NO_IMSS(ByVal NoIMSS As String)
		Dim hacer As Boolean = True
		Dim SQL As String = "Select * From Personal_Clientes Where Id_Empresa = " & Me.lstCliente.SelectItem & " And NO_IMSS = '" & NoIMSS & "'"
		Dim DS As DataSet = Eventos.Obtener_DS(SQL)
		If DS.Tables(0).Rows.Count > 0 Then
			hacer = False
		Else
			hacer = True
		End If
		Return hacer
	End Function
	Private Sub LstTexto_Enters() Handles LstTexto.Enters
		Try

			If Me.TablaImportar.Rows.Count > 0 Then
				For Each Fila As DataGridViewRow In TablaImportar.Rows
					If Fila.Cells(9).Selected = True Then
						Try
							Fila.Cells(9).Value = Trim(Me.LstTexto.SelectItem)
						Catch ex As Exception

						End Try
					ElseIf Fila.Cells(10).Selected = True Then
						Try
							Fila.Cells(10).Value = Trim(Me.LstTexto.SelectItem)
						Catch ex As Exception

						End Try
					ElseIf Fila.Cells(11).Selected = True Then
						Try
							Fila.Cells(11).Value = Trim(Me.LstTexto.SelectItem)
						Catch ex As Exception

						End Try
					ElseIf Fila.Cells(12).Selected = True Then
						Try
							Fila.Cells(12).Value = Trim(Me.LstTexto.SelectItem)
						Catch ex As Exception

						End Try
					End If
				Next
			End If
		Catch ex As Exception

		End Try
	End Sub

	Private Sub LstGuia_Enters() Handles LstGuia.Enters
		Try

			If Me.TablaImportar.Rows.Count > 0 Then
				For Each Fila As DataGridViewRow In TablaImportar.Rows
					If Fila.Cells(13).Selected = True Then
						Try
							Fila.Cells(13).Value = Calcula_Guia()
						Catch ex As Exception

						End Try
					End If
				Next
			End If
		Catch ex As Exception

		End Try
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles CmdPlantlla.Click
		Dim Excel As Excel.Application = Eventos.NuevoExcel("Trabajadores", False)
		Excel.Visible = True
	End Sub
End Class
