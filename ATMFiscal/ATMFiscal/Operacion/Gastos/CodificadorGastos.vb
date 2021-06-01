Imports Telerik.WinControls
Public Class CodificadorGastos
    Dim Polizas As New List(Of Contabilizar_XML)
    Private Sub CodificadorGastos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = My.Forms.Inicio.Clt
        Eventos.DiseñoTabla(Me.Tabla)
        Eventos.DiseñoTabla(Me.TablaMigrar)
    End Sub
    Private Sub CmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        If Me.TabControl1.SelectedIndex = 0 Then
            If Me.Tabla.Rows.Count > 0 Then
                Me.Tabla.Columns.Clear()
            End If
        Else
            If Me.TablaMigrar.Rows.Count > 0 Then
                Me.TablaMigrar.Columns.Clear()
            End If
        End If

    End Sub

	Private Sub CmdBuscar_Click(sender As Object, e As EventArgs) Handles CmdBuscar.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		If Me.lstCliente.SelectText <> "" Then


			If Me.TabControl1.SelectedIndex = 0 Then
				If Me.Tabla.Rows.Count > 0 Then
					Me.Tabla.Columns.Clear()
				End If
				BuscarTusa()

			Else
				If Me.TablaMigrar.Rows.Count > 0 Then
					Me.TablaMigrar.Columns.Clear()
				End If
				BuscarATM()
			End If
		Else
			RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
		End If
	End Sub
	Private Sub BuscarTusa()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim ds As DataSet


        Dim SQL = "SELECT XmlAuditados.Orden, XmlAuditados.UUID , XmlAuditados.Tasa ,XmlAuditados.ImpGrabado as [Importe Grabado], "
        SQL &= " XmlAuditados.ImpExcento as  [Importe Exento] ,XmlAuditados.ImpIva as [Iva] , XmlAuditados.Fecha,XmlAuditados.Emisor ,"
        SQL &= " XmlAuditados.Nombre ,XmlAuditados.Metodo  FROM XmlAuditados WHERE  Id_PolizaS IS NULL AND Id_Poliza_Tusa IS NULL AND ( Orden IS NOT NULL AND Orden <>'')"
        SQL &= "  AND XmlAuditados.Fecha >= " & Eventos.Sql_hoy(Me.Dtfin.Value) & " AND  XmlAuditados.Fecha <=" & Eventos.Sql_hoy(DtInicio.Value) & " "
        SQL &= "and XmlAuditados.ID_Empresa= " & Me.lstCliente.SelectItem & ""
        ds = Eventos.Obtener_DS(SQL)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.DataSource = ds.Tables(0)
            Dim Col As New DataGridViewTextBoxColumn
            Col.HeaderText = "Matricula"
            Col.Name = "Mat"
            Tabla.Columns.Add(Col)
            Col = New DataGridViewTextBoxColumn
            Col.HeaderText = "Nombre"
            Col.Name = "Nam"
            Tabla.Columns.Add(Col)
            Col = New DataGridViewTextBoxColumn
            Col.HeaderText = "RFC"
            Col.Name = "RFC"
            Tabla.Columns.Add(Col)

            SQL = "SELECT O.ID_orden AS OT , X.UUDI ,O.ID_matricula AS M, P.Nombres +  ' ' + p.Ap_paterno  + ' ' + P.Ap_materno AS Deudor, P.Reg_fed_causantes AS RFC  FROM XMLPolizas AS X "
            SQL &= " LEFT OUTER JOIN Orden_traslados AS O ON O.ID_orden = X.Id_orden "
            SQL &= " LEFT OUTER JOIN PERSONAL AS P ON O.ID_matricula= p.ID_matricula "
            SQL &= " WHERE O.Fecha_traslado > = " & Eventos.Sql_hoy(Me.Dtfin.Value) & " AND O.Fecha_traslado <= " & Eventos.Sql_hoy(DtInicio.Value) & ""
            ds = Eventos.Obtener_DSTusa(SQL)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim dt = ds.Tables(0)

                For i As Integer = 0 To Me.Tabla.Rows.Count - 1
                    Dim rows = dt.Select("UUDI ='" & Me.Tabla.Item(1, i).Value & "'", "UUDI ASC")
                    If rows.Length > 0 Then
                        Me.Tabla.Item(10, i).Value = rows(0).Item("M")
                        Me.Tabla.Item(11, i).Value = rows(0).Item("Deudor")
                        Me.Tabla.Item(12, i).Value = rows(0).Item("RFC")
                    End If

                Next

            End If

        Else
            RadMessageBox.Show("No hay Ordenes en el Periodo", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub
    Private Sub BuscarATM()
        Dim SQL = ""
        Dim ds As DataSet

        SQL = "SELECT O.ID_orden AS OT , X.UUDI , "
        SQL &= " Case when X.Tipo = 1 then 'Combustible' when X.Tipo = 2 then 'Combustible Extra' when X.Tipo = 3 then 'Peaje' when X.Tipo = 4 then 'Alimentos' "
        SQL &= " when X.Tipo = 5 then 'Pasaje' when X.Tipo = 6 then 'Otros' end as Tipo  ,O.ID_matricula AS M, P.Nombres +  ' ' + p.Ap_paterno  + ' ' + P.Ap_materno AS Deudor, P.Reg_fed_causantes AS RFC  FROM XMLPolizas AS X "
        SQL &= " LEFT OUTER JOIN Orden_traslados AS O ON O.ID_orden = X.Id_orden "
        SQL &= " LEFT OUTER JOIN PERSONAL AS P ON O.ID_matricula= p.ID_matricula "
        SQL &= " WHERE O.Fecha_traslado > = " & Eventos.Sql_hoy(Me.Dtfin.Value) & " AND O.Fecha_traslado <= " & Eventos.Sql_hoy(DtInicio.Value) & ""
        ds = Eventos.Obtener_DSTusa(SQL)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaMigrar.DataSource = ds.Tables(0)
            Dim Col As New DataGridViewTextBoxColumn
            Col.HeaderText = "Tasa"
            Col.Name = "Tasa"
            TablaMigrar.Columns.Add(Col)

            Col = New DataGridViewTextBoxColumn
            Col.HeaderText = "Importe Grabado"
            Col.Name = "ImporteGrabado"
            TablaMigrar.Columns.Add(Col)

            Col = New DataGridViewTextBoxColumn
            Col.HeaderText = "Importe Exento"
            Col.Name = "ImporteExento"
            TablaMigrar.Columns.Add(Col)

            Col = New DataGridViewTextBoxColumn
            Col.HeaderText = "Iva"
            Col.Name = "Iva"
            TablaMigrar.Columns.Add(Col)

            Col = New DataGridViewTextBoxColumn
            Col.HeaderText = "Fecha"
            Col.Name = "Fecha"
            TablaMigrar.Columns.Add(Col)

            Col = New DataGridViewTextBoxColumn
            Col.HeaderText = "Emisor"
            Col.Name = "Emisor"
            TablaMigrar.Columns.Add(Col)

            Col = New DataGridViewTextBoxColumn
            Col.HeaderText = "Nombre"
            Col.Name = "Nombre"
            TablaMigrar.Columns.Add(Col)

            Col = New DataGridViewTextBoxColumn
            Col.HeaderText = "Metodo"
            Col.Name = "Metodo"
            TablaMigrar.Columns.Add(Col)


            SQL = "SELECT XmlAuditados.Orden, XmlAuditados.UUID , XmlAuditados.Tasa AS T ,XmlAuditados.ImpGrabado as G, "
            SQL &= " XmlAuditados.ImpExcento as  E ,XmlAuditados.ImpIva as I , XmlAuditados.Fecha AS F,XmlAuditados.Emisor AS EM ,"
            SQL &= " XmlAuditados.Nombre AS N,XmlAuditados.Metodo AS MT  FROM XmlAuditados WHERE Id_PolizaS IS NULL AND Id_Poliza_Tusa IS NULL AND "
            SQL &= " XmlAuditados.ID_Empresa= " & Me.lstCliente.SelectItem & ""
            ds = Eventos.Obtener_DS(SQL)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim dt = ds.Tables(0)

                For i As Integer = 0 To Me.TablaMigrar.Rows.Count - 1
                    Dim rows = dt.Select("UUID ='" & Me.TablaMigrar.Item(1, i).Value & "'", "UUID ASC")
                    If rows.Length > 0 Then
                        Me.TablaMigrar.Item(6, i).Value = rows(0).Item("T")
                        Me.TablaMigrar.Item(7, i).Value = rows(0).Item("G")
                        Me.TablaMigrar.Item(8, i).Value = rows(0).Item("E")
                        Me.TablaMigrar.Item(9, i).Value = rows(0).Item("I")
                        Me.TablaMigrar.Item(10, i).Value = rows(0).Item("F")
                        Me.TablaMigrar.Item(11, i).Value = rows(0).Item("EM")
                        Me.TablaMigrar.Item(12, i).Value = rows(0).Item("N")
                        Me.TablaMigrar.Item(13, i).Value = rows(0).Item("MT")

                        Polizas.Add(New Contabilizar_XML() With {.Importe_Grabado = rows(0).Item("G"),
                                       .Importe_Exento = rows(0).Item("E"),
                                       .Iva = rows(0).Item("I"),
                                       .Orden = Me.TablaMigrar.Item(0, i).Value,
                                       .Estatus = "Comprobada",
                                       .Total = rows(0).Item("E") + rows(0).Item("G") + rows(0).Item("I"),
                                       .Tasa = rows(0).Item("T"),
                                       .UUID = Me.TablaMigrar.Item(1, i).Value, .Serie = Me.TablaMigrar.Item(2, i).Value,
                                       .Fecha = rows(0).Item("F").ToString.Substring(0, 10),
                                       .Emisor = rows(0).Item("EM"),
                                       .Nombre = rows(0).Item("N"),
                                       .Matricula = Me.TablaMigrar.Item(3, i).Value,
                                       .Nombres_Deudor = Me.TablaMigrar.Item(4, i).Value,
                                       .RFC_Deudor = Me.TablaMigrar.Item(5, i).Value,
                                       .Metodo = rows(0).Item("MT")
                                       })
                    End If

                Next
                Dim filas As Integer = Me.TablaMigrar.RowCount - 1
                For j As Integer = 0 To filas
                    For i As Integer = 0 To Me.TablaMigrar.RowCount - 1
                        If Me.TablaMigrar.Item(10, i).Value Is Nothing Then
                            Me.TablaMigrar.Rows.RemoveAt(i)
                            Exit For
                        End If
                    Next
                Next
            End If
        End If


    End Sub

    Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
        If Polizas.Count > 0 Then
            Eventos.Crear_Polizas_Fiscales(Polizas)
        End If

    End Sub
End Class
