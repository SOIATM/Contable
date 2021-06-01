Imports Telerik.WinControls
Public Class VentadeTerrenos
    Dim Negrita_verde As New DataGridViewCellStyle
    Dim Negrita_morado As New DataGridViewCellStyle
    Dim Anio = Str(DateTime.Now.Year)
    Dim m = Now.Date.Month.ToString
    Private Sub VentadeTerrenos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Eventos.DiseñoTabla(Me.TablaImportar)
        Cargar_Listas()
        Dim i As Integer
        For i = DateTime.Now.Year To DateTime.Now.Year - 5 Step -1
            If i >= 2004 Then
                Me.LstAnio.Items.Add(Str(i))
            End If
        Next
        Me.LstAnio.Text = Anio
        CargarRegistros(Me.lstCliente.SelectItem, Anio.ToString.Trim())
    End Sub
    Private Sub Cargar_Listas()
        Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = My.Forms.Inicio.Clt

        Dim Tipo As DataSet = Eventos.Obtener_DS(" Select convert(NVARCHAR,Clave,103)  +' - ' + Nombre as Clave  from Tipos_Poliza_Sat INNER JOIN Tipo_Poliza ON Tipo_Poliza.Id_Tipo_poliza = Tipos_Poliza_Sat.Id_Tipo_poliza  where Id_Empresa= " & Me.lstCliente.SelectItem & " AND Tipos_Poliza_Sat.Id_Tipo_poliza IN (3,2)     ")
        If Tipo.Tables(0).Rows.Count > 0 Then
            If Me.TipoPoliza.Items.Count = 0 Then
                For i As Integer = 0 To Tipo.Tables(0).Rows.Count - 1
                    Me.TipoPoliza.Items.Add(Trim(Tipo.Tables(0).Rows(i)("Clave")))
                Next
            Else
                Me.TipoPoliza.Items.Clear()
                For i As Integer = 0 To Tipo.Tables(0).Rows.Count - 1
                    Me.TipoPoliza.Items.Add(Trim(Tipo.Tables(0).Rows(i)("Clave")))
                Next
            End If
        End If
        Dim contab As DataSet = Eventos.Obtener_DS(" Select 'T' as Clave union select 'H' AS Clave union select 'P' AS Clave ")
        If contab.Tables(0).Rows.Count > 0 Then
            If Me.Tipo.Items.Count = 0 Then

                For i As Integer = 0 To contab.Tables(0).Rows.Count - 1
                    Me.Tipo.Items.Add(Trim(contab.Tables(0).Rows(i)("Clave")))
                Next
            Else
                Me.Tipo.Items.Clear()
                For i As Integer = 0 To contab.Tables(0).Rows.Count - 1
                    Me.Tipo.Items.Add(Trim(contab.Tables(0).Rows(i)("clave")))
                Next
            End If
        End If
        Colorear()
    End Sub
    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        Me.TablaImportar.Rows.Add()
    End Sub
    Private Sub Colorear()
        Dim column As New DataGridViewTextBoxColumn()
        Me.TablaImportar.Columns(Precio_Venta.Index).DefaultCellStyle.BackColor = Color.LightGreen
        Me.TablaImportar.Columns(CtaPago.Index).DefaultCellStyle.BackColor = Color.Gold
        Me.TablaImportar.Columns(Precio_Adquisicion.Index).DefaultCellStyle.BackColor = Color.LightGreen
        Me.TablaImportar.Columns(Referencia.Index).DefaultCellStyle.BackColor = Color.Gold
        Me.TablaImportar.Columns(Tipo.Index).DefaultCellStyle.BackColor = Color.DeepSkyBlue
        Me.TablaImportar.Columns(Bo.Index).DefaultCellStyle.BackColor = Color.DeepSkyBlue
        Me.TablaImportar.Columns(BD.Index).DefaultCellStyle.BackColor = Color.DeepSkyBlue
        Me.TablaImportar.Columns(CtaOrigen.Index).DefaultCellStyle.BackColor = Color.DeepSkyBlue
        Me.TablaImportar.Columns(CtaDestino.Index).DefaultCellStyle.BackColor = Color.DeepSkyBlue
        Me.TablaImportar.Columns(FechaMov.Index).DefaultCellStyle.BackColor = Color.DeepSkyBlue
        Me.TablaImportar.Columns(Fecha_Adq.Index).DefaultCellStyle.BackColor = Color.LightGreen
        Me.TablaImportar.Columns(Fecha_de_Venta.Index).DefaultCellStyle.BackColor = Color.LightGreen
        Me.TablaImportar.Columns(CtaUtilidad.Index).DefaultCellStyle.BackColor = Color.Gold
        Me.TablaImportar.Columns(CtaPerdida.Index).DefaultCellStyle.BackColor = Color.Gold
        Me.TablaImportar.Columns(TipoPoliza.Index).DefaultCellStyle.BackColor = Color.PaleGoldenrod
        Me.TablaImportar.Columns(Numero.Index).DefaultCellStyle.BackColor = Color.PaleGoldenrod
        Me.TablaImportar.Columns(Año.Index).DefaultCellStyle.BackColor = Color.PaleGoldenrod
        Me.TablaImportar.Columns(Mes.Index).DefaultCellStyle.BackColor = Color.PaleGoldenrod

    End Sub
    Private Sub CmdListar_Click(sender As Object, e As EventArgs)
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.lstCliente.SelectText <> "" Then

            If Trim(Me.LstAnio.Text) <> "" Then

            Else
                RadMessageBox.Show("Debes seleccionar un Año para consultar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            End If
        Else
            RadMessageBox.Show("Debes seleccionar una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If
    End Sub

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If Me.lstCliente.SelectText <> "" Then
            Dim sql As String = " select * from VentaTerrenos where Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.LstAnio.Text.Trim() & " "
            Dim ds As DataSet = Eventos.Obtener_DS(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If RadMessageBox.Show("Se actualizara la informacion de la Empresa  " & Me.lstCliente.SelectText & " para el año " & Anio & " esto es correcto?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    sql = "Delete from VentaTerrenos where Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.LstAnio.Text.Trim() & ""
                    If Eventos.Comando_sql(sql) > 0 Then
                        Guardar()
                    End If
                Else
                    Exit Sub
                End If
            Else
                Guardar()
            End If
        Else
            RadMessageBox.Show("Debes seleccionar una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub
    Private Sub Guardar()
        Dim sql As String = ""

        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1

            sql = "INSERT INTO dbo.VentaTerrenos 	(	Referencia,	Cliente,	Fecha_Adquisicion,	Precio_Adquisicion,	Fecha_de_Venta,	Precio_Venta,	CtaPago,	Utilidad_Contable,	CtaUtilidad,
    	Perdida_Contable,	CtaPerdida,	UltimoMes,	ImporteUltimomes,	Fecha_Adq,	INPC_Adquisicion,	Factor_Actualizacion,	Precio_Adq_Actualizado,	Precio_Venta_Fiscal,
    	Utilidad_Fiscal,	Perdida_Fiscal,	Anio,	Mes,	TipoPol,	Numpol,	UUID,	Tipo,	Banco_Origen,	Cuenta_Origen,	Banco_Destino,Cuenta_Destino,	Fecha_Transaccion,	No_Cheque,	Id_Empresa	)
        VALUES "
            sql &= " ('" & IIf(Me.TablaImportar.Item(Referencia.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(Referencia.Index, i).Value) & "',	'" & IIf(Me.TablaImportar.Item(Cliente.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(Cliente.Index, i).Value) & "', " & IIf(Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value Is Nothing Or IsDBNull(Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value.ToString()) = True, "NULL", Eventos.Sql_hoy(Me.TablaImportar.Item(Fecha_de_Venta.Index, i).Value)) & ","
            sql &= " " & Me.TablaImportar.Item(Precio_Adquisicion.Index, i).Value & "," & IIf(Me.TablaImportar.Item(Fecha_de_Venta.Index, i).Value Is Nothing Or IsDBNull(Me.TablaImportar.Item(Fecha_de_Venta.Index, i).Value.ToString()) = True, "NULL", Eventos.Sql_hoy(Me.TablaImportar.Item(Fecha_de_Venta.Index, i).Value)) & ","
            sql &= " " & Me.TablaImportar.Item(Precio_Venta.Index, i).Value & ",'" & IIf(Me.TablaImportar.Item(CtaPago.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(CtaPago.Index, i).Value) & "'," & IIf(Me.TablaImportar.Item(Utilidad_Contable.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(Utilidad_Contable.Index, i).Value) & ","
            sql &= " '" & IIf(Me.TablaImportar.Item(CtaUtilidad.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(CtaUtilidad.Index, i).Value) & "'," & IIf(Me.TablaImportar.Item(Perdida_Contable.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(Perdida_Contable.Index, i).Value) & ",'" & IIf(Me.TablaImportar.Item(CtaPerdida.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(CtaPerdida.Index, i).Value) & "',"
            sql &= " " & IIf(Me.TablaImportar.Item(UltimoMes.Index, i).Value Is Nothing Or IsDBNull(Me.TablaImportar.Item(UltimoMes.Index, i).Value.ToString()) = True, "NULL", Eventos.Sql_hoy(Me.TablaImportar.Item(UltimoMes.Index, i).Value)) & ","
            sql &= " " & Me.TablaImportar.Item(ImporteUltimomes.Index, i).Value & "," & IIf(Me.TablaImportar.Item(Fecha_Adq.Index, i).Value Is Nothing Or IsDBNull(Me.TablaImportar.Item(Fecha_Adq.Index, i).Value.ToString()) = True, "NULL", Eventos.Sql_hoy(Me.TablaImportar.Item(Fecha_Adq.Index, i).Value)) & ","
            sql &= " " & Me.TablaImportar.Item(INPC_Adquisicion.Index, i).Value & "," & Me.TablaImportar.Item(Factor_Actualizacion.Index, i).Value & "," & Me.TablaImportar.Item(Precio_Adq_Actualizado.Index, i).Value & ","
            sql &= " " & Me.TablaImportar.Item(Precio_Venta_Fiscal.Index, i).Value & "," & IIf(Me.TablaImportar.Item(Utilidad_Fiscal.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(Utilidad_Fiscal.Index, i).Value) & "," & IIf(Me.TablaImportar.Item(Perdida_Fiscal.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(Perdida_Fiscal.Index, i).Value) & ","
            sql &= " " & IIf(Me.TablaImportar.Item(Año.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(Año.Index, i).Value) & ",'" & IIf(Me.TablaImportar.Item(Mes.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(Mes.Index, i).Value) & "','" & IIf(Me.TablaImportar.Item(TipoPoliza.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(TipoPoliza.Index, i).Value) & "',"
            sql &= " '" & IIf(Me.TablaImportar.Item(Numero.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(Numero.Index, i).Value) & "','" & IIf(Me.TablaImportar.Item(UUID.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(UUID.Index, i).Value) & "','" & IIf(Me.TablaImportar.Item(Tipo.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(Tipo.Index, i).Value) & "',"
            sql &= " '" & IIf(Me.TablaImportar.Item(Bo.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(Bo.Index, i).Value) & "','" & IIf(Me.TablaImportar.Item(CtaOrigen.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(CtaOrigen.Index, i).Value) & "','" & IIf(Me.TablaImportar.Item(BD.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(BD.Index, i).Value) & "',"
            sql &= " '" & IIf(Me.TablaImportar.Item(CtaDestino.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(CtaDestino.Index, i).Value) & "'," & IIf(Me.TablaImportar.Item(FechaMov.Index, i).Value Is Nothing Or IsDBNull(Me.TablaImportar.Item(FechaMov.Index, i).Value) = True, "NULL", Eventos.Sql_hoy(Me.TablaImportar.Item(FechaMov.Index, i).Value)) & ","
            sql &= " '" & IIf(Me.TablaImportar.Item(Nocheque.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(Nocheque.Index, i).Value) & "'," & Me.lstCliente.SelectItem & ")"

            If Eventos.Comando_sql(sql) > 0 Then

            End If

        Next
    End Sub
    Private Sub TablaImportar_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles TablaImportar.CellEndEdit
        If Me.TablaImportar.Item(Fecha_Adquisicion.Index, Me.TablaImportar.CurrentRow.Index).Value IsNot Nothing Then
            Me.TablaImportar.Item(Fecha_Adq.Index, Me.TablaImportar.CurrentRow.Index).Value = Me.TablaImportar.Item(Fecha_Adquisicion.Index, Me.TablaImportar.CurrentRow.Index).Value
        End If
        If Me.TablaImportar.Item(Precio_Adquisicion.Index, Me.TablaImportar.CurrentRow.Index).Value > 0 Then
            Me.TablaImportar.Item(Precio_Adquisicion.Index, Me.TablaImportar.CurrentRow.Index).Value = Me.TablaImportar.Item(Precio_Adquisicion.Index, Me.TablaImportar.CurrentRow.Index).Value / 1
            If Me.TablaImportar.Item(Precio_Venta.Index, Me.TablaImportar.CurrentRow.Index).Value > 0 Then
                Me.TablaImportar.Item(Precio_Venta.Index, Me.TablaImportar.CurrentRow.Index).Value = Me.TablaImportar.Item(Precio_Venta.Index, Me.TablaImportar.CurrentRow.Index).Value / 1
                If Me.TablaImportar.Item(Precio_Venta.Index, Me.TablaImportar.CurrentRow.Index).Value > Me.TablaImportar.Item(Precio_Adquisicion.Index, Me.TablaImportar.CurrentRow.Index).Value Then
                    Me.TablaImportar.Item(Utilidad_Contable.Index, Me.TablaImportar.CurrentRow.Index).Value = Me.TablaImportar.Item(Precio_Venta.Index, Me.TablaImportar.CurrentRow.Index).Value - Me.TablaImportar.Item(Precio_Adquisicion.Index, Me.TablaImportar.CurrentRow.Index).Value
                ElseIf Me.TablaImportar.Item(Precio_Venta.Index, Me.TablaImportar.CurrentRow.Index).Value < Me.TablaImportar.Item(Precio_Adquisicion.Index, Me.TablaImportar.CurrentRow.Index).Value Then
                    Me.TablaImportar.Item(Utilidad_Contable.Index, Me.TablaImportar.CurrentRow.Index).Value = Me.TablaImportar.Item(Precio_Adquisicion.Index, Me.TablaImportar.CurrentRow.Index).Value - Me.TablaImportar.Item(Precio_Venta.Index, Me.TablaImportar.CurrentRow.Index).Value
                End If
            End If
        End If

        CalculamesDep(Me.TablaImportar.CurrentRow.Index)

        If Me.TablaImportar.Item(Precio_Adq_Actualizado.Index, Me.TablaImportar.CurrentRow.Index).Value > 0 Then
            If Me.TablaImportar.Item(Precio_Venta.Index, Me.TablaImportar.CurrentRow.Index).Value > 0 Then
                Me.TablaImportar.Item(Precio_Venta_Fiscal.Index, Me.TablaImportar.CurrentRow.Index).Value = Me.TablaImportar.Item(Precio_Venta.Index, Me.TablaImportar.CurrentRow.Index).Value

                If Me.TablaImportar.Item(Precio_Venta_Fiscal.Index, Me.TablaImportar.CurrentRow.Index).Value > Me.TablaImportar.Item(Precio_Adq_Actualizado.Index, Me.TablaImportar.CurrentRow.Index).Value Then
                    Me.TablaImportar.Item(Utilidad_Fiscal.Index, Me.TablaImportar.CurrentRow.Index).Value = Me.TablaImportar.Item(Precio_Venta_Fiscal.Index, Me.TablaImportar.CurrentRow.Index).Value - Me.TablaImportar.Item(Precio_Adq_Actualizado.Index, Me.TablaImportar.CurrentRow.Index).Value
                ElseIf Me.TablaImportar.Item(Precio_Venta_Fiscal.Index, Me.TablaImportar.CurrentRow.Index).Value < Me.TablaImportar.Item(Precio_Adq_Actualizado.Index, Me.TablaImportar.CurrentRow.Index).Value Then
                    Me.TablaImportar.Item(Utilidad_Fiscal.Index, Me.TablaImportar.CurrentRow.Index).Value = Me.TablaImportar.Item(Precio_Adq_Actualizado.Index, Me.TablaImportar.CurrentRow.Index).Value - Me.TablaImportar.Item(Precio_Venta_Fiscal.Index, Me.TablaImportar.CurrentRow.Index).Value
                End If
            End If
        End If
        If Me.TablaImportar.Item(UUID.Index, Me.TablaImportar.CurrentRow.Index).Value <> Nothing Then
            BuscarFactura(Me.TablaImportar.Item(UUID.Index, Me.TablaImportar.CurrentRow.Index).Value.TRIM(), Me.TablaImportar.CurrentRow.Index)
        End If
        If Me.TablaImportar.Item(Fecha_de_Venta.Index, Me.TablaImportar.CurrentRow.Index).Value IsNot Nothing Then
            If Me.TablaImportar.Item(Año.Index, Me.TablaImportar.CurrentRow.Index).Value Is Nothing Then
                Me.TablaImportar.Item(Año.Index, Me.TablaImportar.CurrentRow.Index).Value = Me.TablaImportar.Item(Fecha_de_Venta.Index, Me.TablaImportar.CurrentRow.Index).Value.ToString.Substring(6, 4)
            End If
            If Me.TablaImportar.Item(Mes.Index, Me.TablaImportar.CurrentRow.Index).Value Is Nothing Then
                Me.TablaImportar.Item(Mes.Index, Me.TablaImportar.CurrentRow.Index).Value = Me.TablaImportar.Item(Fecha_de_Venta.Index, Me.TablaImportar.CurrentRow.Index).Value.ToString.Substring(3, 2)
            End If
        End If
        Liberar_Proceso(Me.TablaImportar.CurrentRow.Index)

    End Sub
    Private Sub BuscarFactura(ByVal UUID As String, ByVal I As Integer)
        Dim sql As String = "SELECT  Xml_Sat.Total   FROM Xml_Sat WHERE Id_Empresa = " & Me.lstCliente.SelectItem & " AND UUID ='" & UUID & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaImportar.Item(Precio_Adquisicion.Index, I).Value = IIf(IsDBNull(ds.Tables(0).Rows(0)("Total")), 0, ds.Tables(0).Rows(0)("Total"))
        End If
    End Sub
    Private Sub CalculamesDep(ByVal i As Integer)
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim MI As Decimal = 0
        Dim MF As Decimal = 0
        Dim F As String = ""

        Try
            If Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value.ToString.Substring(6, 4) > Me.LstAnio.Text.Trim() Then
                RadMessageBox.Show("La fecha de Adquisicion es Mayor al Año procesado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Else

                If (Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value <> Nothing And Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value.ToString <> "") Then
                    If Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value.ToString.Substring(6, 4) = Me.LstAnio.Text.Trim() Then

                        If (Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value <> Nothing And Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value.ToString <> "") And (Me.TablaImportar.Item(Fecha_de_Venta.Index, i).Value Is Nothing Or IsDBNull(Me.TablaImportar.Item(Fecha_de_Venta.Index, i).Value) = True) Then
                            MI = 12 - (Val(Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value.ToString.Substring(3, 2)))
                            MF = 12 - Val(Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value.ToString.Substring(3, 2))
                            MF = MF + 1
                            MF = Math.Truncate(MF / 2)
                            MF = (MF - 1) + Val(Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value.ToString.Substring(3, 2))
                            F = Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value.ToString.Substring(0, 2) & "/" & IIf(Len(MF.ToString) = 1, "0" & MF, MF) & "/" & Me.LstAnio.Text.Trim()

                        Else
                            If Me.TablaImportar.Item(Fecha_de_Venta.Index, i).Value.ToString.Substring(6, 4) = Me.LstAnio.Text.Trim() Then

                                MI = Val(Me.TablaImportar.Item(Fecha_de_Venta.Index, i).Value.ToString.Substring(3, 2))
                                MI = MI - (Val(Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value.ToString.Substring(3, 2)) + 1)

                                MF = Val(Me.TablaImportar.Item(Fecha_de_Venta.Index, i).Value.ToString.Substring(3, 2)) - Val(Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value.ToString.Substring(3, 2))
                                MF = MF + 1
                                MF = Math.Truncate(MF / 2)
                                MF = (MF - 1) + Val(Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value.ToString.Substring(3, 2))
                                F = Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value.ToString.Substring(0, 2) & "/" & IIf(Len(MF.ToString) = 1, "0" & MF, MF) & "/" & Me.LstAnio.Text.Trim()

                            Else
                                MessageBox.Show("El año de la fecha de baja no puede ser mayor al año procesado...", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                        End If

                    ElseIf Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value.ToString.Substring(6, 4) < Me.LstAnio.Text.Trim() Then
                        If (Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value <> Nothing And Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value.ToString <> "") And (Me.TablaImportar.Item(Fecha_de_Venta.Index, i).Value Is Nothing Or IsDBNull(Me.TablaImportar.Item(Fecha_de_Venta.Index, i).Value) = True) = False Then
                            MI = 12
                            F = Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value.ToString.Substring(0, 2) & "/06/" & Me.LstAnio.Text.Trim()
                            MF = "06"
                        Else
                            If Me.TablaImportar.Item(Fecha_de_Venta.Index, i).Value.ToString.Substring(6, 4) = Me.LstAnio.Text.Trim() Then

                                MI = Val(Me.TablaImportar.Item(Fecha_de_Venta.Index, i).Value.ToString.Substring(3, 2)) - 1
                                MF = Val(Me.TablaImportar.Item(Fecha_de_Venta.Index, i).Value.ToString.Substring(3, 2))
                                MF = Math.Truncate(MF / 2)

                                F = Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value.ToString.Substring(0, 2) & "/" & IIf(Len(MF.ToString) = 1, "0" & MF, MF) & "/" & Me.LstAnio.Text.Trim()
                            Else
                                MessageBox.Show("El año de la fecha de baja no puede ser diferente al año procesado...", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                        End If
                    End If
                End If
            End If



            Me.TablaImportar.Item(UltimoMes.Index, i).Value = F
            'Me.TablaImportar.Item(MesesDep.Index, i).Value = MI
            'Me.TablaImportar.Item(INPCMes.Index, i).Value = F
            Try
                Me.TablaImportar.Item(ImporteUltimomes.Index, i).Value = Eventos.ObtenerValorDB("INPC", "Importe", " datepart(month,fecha)=" & F.ToString.Substring(3, 2) & " AND datepart(year,fecha)=" & Me.LstAnio.Text.Trim() & "", True)
            Catch ex As Exception
                Me.TablaImportar.Item(ImporteUltimomes.Index, i).Value = 0
            End Try
            Try
                Me.TablaImportar.Item(INPC_Adquisicion.Index, i).Value = Eventos.ObtenerValorDB("INPC", "Importe", " datepart(month,fecha)=" & Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value.ToString.Substring(3, 2) & " AND datepart(year,fecha)=" & Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value.ToString.Substring(6, 4) & "", True)
            Catch ex As Exception
                Me.TablaImportar.Item(INPC_Adquisicion.Index, i).Value = 0
            End Try
            Try
                Dim caden As String = Me.TablaImportar.Item(ImporteUltimomes.Index, i).Value / Me.TablaImportar.Item(INPC_Adquisicion.Index, i).Value
                Dim valores = Split(caden, ".")
                If Me.TablaImportar.Item(Precio_Adquisicion.Index, i).Value > 0.01 Then
                    If Me.TablaImportar.Item(INPC_Adquisicion.Index, i).Value > Me.TablaImportar.Item(ImporteUltimomes.Index, i).Value Then
                        Me.TablaImportar.Item(Factor_Actualizacion.Index, i).Value = 1
                    Else
                        Me.TablaImportar.Item(Factor_Actualizacion.Index, i).Value = Convert.ToDecimal(valores(0) & "." & valores(1).Substring(0, 4))
                    End If
                End If

            Catch ex As Exception

            End Try
            Try
                Me.TablaImportar.Item(Precio_Adq_Actualizado.Index, i).Value = Me.TablaImportar.Item(Precio_Adquisicion.Index, i).Value * Me.TablaImportar.Item(Factor_Actualizacion.Index, i).Value
            Catch ex As Exception

            End Try
            ' =SI(D10>0.01,SI(M10>K10,1,TRUNCAR(K10/M10,4)),0)



        Catch ex As Exception

        End Try
    End Sub

    Private Sub Liberar_Proceso(ByVal i As Integer)

        Dim contador As Integer = 0

        Try

            If Me.TablaImportar.Item(Tipo.Index, i).Value = "T" Then ' Bloqueo transferencia
                If Me.TablaImportar.Item(Bo.Index, i).Value Is Nothing Or Me.TablaImportar.Item(BD.Index, i).Value Is Nothing Or Me.TablaImportar.Item(CtaOrigen.Index, i).Value Is Nothing Or Me.TablaImportar.Item(CtaDestino.Index, i).Value = Nothing Or Me.TablaImportar.Item(FechaMov.Index, i).Value Is Nothing Then
                    Me.TablaImportar.Item(Aplicar.Index, i).Value = False
                    Exit Sub
                Else
                    Me.TablaImportar.Item(Aplicar.Index, i).Value = True
                End If
            ElseIf Me.TablaImportar.Item(Tipo.Index, i).Value = "H" Then ' Bloqueo cheques
                If Me.TablaImportar.Item(Bo.Index, i).Value Is Nothing Or Me.TablaImportar.Item(CtaOrigen.Index, i).Value Is Nothing Or Me.TablaImportar.Item(Nocheque.Index, i).Value Is Nothing Or Me.TablaImportar.Item(FechaMov.Index, i).Value Is Nothing Then
                    Me.TablaImportar.Item(Aplicar.Index, i).Value = False
                    Exit Sub
                Else
                    Me.TablaImportar.Item(Aplicar.Index, i).Value = True
                End If
            ElseIf Me.TablaImportar.Item(Tipo.Index, i).Value = "P" Then ' Bloqueo cheques
                Me.TablaImportar.Item(Aplicar.Index, i).Value = True
            End If

            If Me.TablaImportar.Item(UUID.Index, i).Value = "" Then
                Me.TablaImportar.Item(Aplicar.Index, i).Value = False
                Exit Sub
            Else
                Me.TablaImportar.Item(Aplicar.Index, i).Value = True
            End If
            If Me.TablaImportar.Item(Referencia.Index, i).Value = "" Then
                Me.TablaImportar.Item(Aplicar.Index, i).Value = False
                Exit Sub
            Else
                Me.TablaImportar.Item(Aplicar.Index, i).Value = True
            End If

            If Me.TablaImportar.Item(Utilidad_Contable.Index, i).Value - Me.TablaImportar.Item(Perdida_Contable.Index, i).Value <> 0 Then
                If Me.TablaImportar.Item(Utilidad_Contable.Index, i).Value > 0 And Me.TablaImportar.Item(CtaUtilidad.Index, i).Value <> "" Then
                    Me.TablaImportar.Item(Aplicar.Index, i).Value = True
                ElseIf Me.TablaImportar.Item(Utilidad_Contable.Index, i).Value > 0 And Me.TablaImportar.Item(CtaUtilidad.Index, i).Value = "" Then
                    Me.TablaImportar.Item(Aplicar.Index, i).Value = False
                    Exit Sub
                End If
                If Me.TablaImportar.Item(Perdida_Contable.Index, i).Value > 0 And Me.TablaImportar.Item(CtaPerdida.Index, i).Value <> "" Then
                    Me.TablaImportar.Item(Aplicar.Index, i).Value = True
                ElseIf Me.TablaImportar.Item(Perdida_Contable.Index, i).Value > 0 And Me.TablaImportar.Item(CtaPerdida.Index, i).Value = "" Then
                    Me.TablaImportar.Item(Aplicar.Index, i).Value = False
                    Exit Sub
                End If

            End If
            If Me.TablaImportar.Item(Precio_Venta.Index, i).Value > 0 And Me.TablaImportar.Item(CtaPago.Index, i).Value <> "" Then
                Me.TablaImportar.Item(Aplicar.Index, i).Value = True
            Else
                Me.TablaImportar.Item(Aplicar.Index, i).Value = False
                Exit Sub
            End If
            If Me.TablaImportar.Item(Año.Index, i).Value.ToString() <> "" Then
                Me.TablaImportar.Item(Aplicar.Index, i).Value = True
            Else
                Me.TablaImportar.Item(Aplicar.Index, i).Value = False
                Exit Sub
            End If
            If Me.TablaImportar.Item(Mes.Index, i).Value.ToString() <> "" Then
                Me.TablaImportar.Item(Aplicar.Index, i).Value = True
            Else
                Me.TablaImportar.Item(Aplicar.Index, i).Value = False
                Exit Sub
            End If
            If Me.TablaImportar.Item(Tipo.Index, i).Value <> "" Then
                Me.TablaImportar.Item(Aplicar.Index, i).Value = True
            Else
                Me.TablaImportar.Item(Aplicar.Index, i).Value = False
                Exit Sub
            End If
            If Me.TablaImportar.Item(TipoPoliza.Index, i).Value <> "" Then
                Me.TablaImportar.Item(Aplicar.Index, i).Value = True
            Else
                Me.TablaImportar.Item(Aplicar.Index, i).Value = False
                Exit Sub
            End If
            If Me.TablaImportar.Item(Numero.Index, i).Value <> "" Then
                Me.TablaImportar.Item(Aplicar.Index, i).Value = True
            Else
                Me.TablaImportar.Item(Aplicar.Index, i).Value = False
                Exit Sub
            End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub Contabilidad(ByVal Tipo_Pol As String, ByVal Tipo As String,
                             ByVal Importe As Decimal, ByVal BO As String, ByVal CO As String, ByVal BD As String, ByVal CD As String, ByVal NCh As String,
                             ByVal i As Integer, ByVal Poliza As String, ByVal Anio As String,
                             ByVal Mes As String, ByVal Fecha As String, ByVal RFC As String, ByVal UUID As String)
        If Buscafactura(UUID, "C") = True Then
            'Se inserta la Factura
            Inserta_Comprobante_Fiscal(Poliza, Anio, Mes, RFC, Fecha, UUID, "Factura " & Trim(RFC) & " C", Importe)
        Else
            'Se Edita la Factura
            Edita_Factura(UUID, "C", Poliza)
        End If
        If Tipo > "P" Then
            ' Insertar registro contabiidad electronica efectivo
            Inserta_Comprobante_Fiscal_Efectivo(Poliza, Anio, Mes, RFC, Tipo_Pol, Fecha, "", "", "", "", Importe)
        ElseIf Tipo > "T" Then

            Inserta_Comprobante_Fiscal_Transf(Poliza, Anio, Mes, RFC, Tipo_Pol, Fecha, "", BO, CO, UUID, Importe, BD, CD)
        ElseIf Tipo > "H" Then
            Inserta_Comprobante_Fiscal_Cheque(Poliza, Anio, Mes, RFC, Tipo_Pol, Fecha, NCh, BO, CO, UUID, Importe)

        End If

    End Sub
    Private Function Buscafactura(ByVal Folio_Fiscal As String, ByVal detaclle As String)
        Dim hacer As Boolean
        Dim sql As String = "select * from Facturas where Folio_Fiscal = '" & Folio_Fiscal & "' and Detalle_Comp_Electronico ='" & detaclle & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hacer = False
        Else
            hacer = True
        End If
        Return hacer
    End Function
    Private Sub Inserta_Comprobante_Fiscal(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
                      ByVal Rfc_Emisor As String, ByVal fecha As String,
                      ByVal Folio_Fiscal As String, ByVal Referencia As String, ByVal Importe As Decimal)
        Dim sql As String = "INSERT INTO dbo.Facturas"
        sql &= " 	(                   "
        sql &= " 	ID_anio,                    "
        sql &= " 	ID_mes,                     "
        sql &= " 	Id_Poliza,                  "
        sql &= " 	RFC_Emisor,                 "
        sql &= " 	Folio_Fiscal,               "
        sql &= " 	Referencia,                 "
        sql &= " 	Importe,                "
        sql &= " 	Fecha_Comprobante,          "
        sql &= " 	Detalle_Comp_Electronico,Id_Empresa"
        sql &= "    )                         "
        sql &= " VALUES "
        sql &= "(                             "
        sql &= " '" & anio & "',	" '@id_anio,                   
        sql &= " '" & mes & "'," '@id_mes,                    
        sql &= " '" & id_poliza & "'," '@id_poliza,                 
        sql &= " '" & Rfc_Emisor & "'," '@rfc_emisor,                
        sql &= " '" & Folio_Fiscal & "'," '@folio_fiscal,              
        sql &= " '" & Referencia & "'," '@referencia,                
        sql &= " " & Importe & "	," '@importe,                   
        sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_comprobante,         
        sql &= " 'C'," & Me.lstCliente.SelectItem & "" '@detalle_comp_electronico   
        sql &= " )"
        If Eventos.Comando_sql(sql) > 0 Then
        End If
    End Sub
    Private Sub Edita_Factura(ByVal Folio_Fiscal As String, ByVal detaclle As String, ByVal Poliza As String)
        Dim sql As String = " UPDATE dbo.Facturas
                            SET Id_Poliza = '" & Poliza & "'
                            WHERE Folio_Fiscal = '" & Folio_Fiscal & "' and Detalle_Comp_Electronico ='" & detaclle & "' "
        If Eventos.Comando_sql(sql) > 0 Then
        End If
    End Sub
    Private Sub Inserta_Comprobante_Fiscal_Efectivo(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
                           ByVal Rfc_Emisor As String, ByVal tipo As String, ByVal fecha As String,
                           ByVal No_cheque As String, ByVal no_banco As String, ByVal cuenta_origen As String, ByVal Referencia As String, ByVal Importe As Decimal)
        Dim sql As String = "  INSERT INTO dbo.Conta_E_Sistema
    	(     anio,    mes,    tipo,       RFC_Ce,
        No_Cheque,    No_Banco,    Cuenta_Origen,    Fecha_Mov,    Importe,
        Id_Poliza,    Tipo_CE	) VALUES	("

        sql &= " '" & anio & "',	" '@id_anio,                   
        sql &= " '" & mes & "'," '@id_mes,     
        sql &= " '" & tipo & "'," '@tipo    

        sql &= " '" & Rfc_Emisor & "'," '@rfc_ce,                
        sql &= " '" & No_cheque & "'," '@no_cheque,  
        sql &= " '" & no_banco & "'," '@no_banco,  
        sql &= " '" & cuenta_origen & "'," '@cuenta_origen,  
        sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_mov,    
        sql &= " " & Importe & "	," '@importe,                    
        sql &= " '" & id_poliza & "', " '@id_poliza,  
        sql &= " 'P' " '@tipo_ce, 
        sql &= " )"
        If Eventos.Comando_sql(sql) > 0 Then
        End If
    End Sub
    Private Sub Inserta_Comprobante_Fiscal_Transf(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
                           ByVal Rfc_Emisor As String, ByVal tipo As String, ByVal fecha As String,
                           ByVal No_cheque As String, ByVal no_banco As String, ByVal cuenta_origen As String, ByVal Referencia As String, ByVal Importe As Decimal, ByVal bancoD As String, ByVal cuentaD As String)
        Dim sql As String = "  INSERT INTO dbo.Conta_E_Sistema
    	(
        anio,    mes,    tipo,       RFC_Ce,
        No_Cheque,    No_Banco,    Cuenta_Origen,    Fecha_Mov,    Importe,
        Id_Poliza,    Tipo_CE,Banco_Destino,Cuenta_Destino	) VALUES	("

        sql &= " '" & anio & "',	" '@id_anio,                   
        sql &= " '" & mes & "'," '@id_mes,     
        sql &= " '" & tipo & "'," '@tipo    

        sql &= " '" & Rfc_Emisor & "'," '@rfc_ce,                
        sql &= " '" & No_cheque & "'," '@no_cheque,  
        sql &= " '" & no_banco & "'," '@no_banco,  
        sql &= " '" & cuenta_origen & "'," '@cuenta_origen,  
        sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_mov,    
        sql &= " " & Importe & "	," '@importe,                    
        sql &= " '" & id_poliza & "', " '@id_poliza,  
        sql &= " 'T','" & Trim(bancoD) & "', '" & Trim(cuentaD.Replace("/", "")) & "' " '@tipo_ce, 
        sql &= " )"
        If Eventos.Comando_sql(sql) > 0 Then
        End If
    End Sub
    Private Sub Inserta_Comprobante_Fiscal_Cheque(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
                           ByVal Rfc_Emisor As String, ByVal tipo As String, ByVal fecha As String,
                           ByVal No_cheque As String, ByVal no_banco As String, ByVal cuenta_origen As String, ByVal Referencia As String, ByVal Importe As Decimal)
        Dim sql As String = "  INSERT INTO dbo.Conta_E_Sistema
    	(
        anio,    mes,    tipo,      RFC_Ce,
        No_Cheque,    No_Banco,    Cuenta_Origen,    Fecha_Mov,    Importe,
        Id_Poliza,    Tipo_CE	) VALUES	("
        sql &= " '" & anio & "',	" '@id_anio,                   
        sql &= " '" & mes & "'," '@id_mes,     
        sql &= " '" & tipo & "'," '@tipo    

        sql &= " '" & Rfc_Emisor & "'," '@rfc_ce,                
        sql &= " '" & No_cheque & "'," '@no_cheque,  
        sql &= " '" & no_banco & "'," '@no_banco,  
        sql &= " '" & cuenta_origen & "'," '@cuenta_origen,  
        sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_mov,    
        sql &= " " & Importe & "	," '@importe,                    
        sql &= " '" & id_poliza & "', " '@id_poliza,  
        sql &= " 'H' " '@tipo_ce, 
        sql &= " )"
        If Eventos.Comando_sql(sql) > 0 Then
        End If
    End Sub

    Private Sub CmdCalcular_Click(sender As Object, e As EventArgs) Handles CmdCalcular.Click
        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
            Dim ee As New System.Windows.Forms.DataGridViewCellEventArgs(0, i)
            Me.TablaImportar_CellEndEdit(sender, ee)
        Next

    End Sub

    Private Sub CmdPoliza_Click(sender As Object, e As EventArgs) Handles CmdPoliza.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Guardar()
        If RadMessageBox.Show("La Empresa " & Me.lstCliente.SelectText & " es correcto?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
            For p As Integer = 0 To Me.TablaImportar.RowCount - 1
                If Me.TablaImportar.Item(Aplicar.Index, p).Value = True Then ' se paso todos los filtros de creacion
                    Codificar_polizas(p)
                End If
            Next
            RadMessageBox.Show("Proceso Terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub
    Private Sub Codificar_polizas(ByVal posicion As Integer)
        Dim poliza_Sistema As String = ""

        poliza_Sistema = Calcula_poliza(posicion)

        Dim posi As Integer = InStr(1, poliza_Sistema, "-", CompareMethod.Binary)
        Dim cuantos As Integer = Len(poliza_Sistema) - Len(poliza_Sistema.Substring(0, posi))
        Dim consecutivo As Integer = Val(poliza_Sistema.Substring(posi, cuantos))

        Dim RFC As String = Eventos.ObtenerValorDB("Clientes", "RFC", " Id_Empresa = " & Me.lstCliente.SelectItem & " ", False)

        If Creapoliza(poliza_Sistema, Me.TablaImportar.Item(Anio.Index, posicion).Value, Me.TablaImportar.Item(Mes.Index, posicion).Value, Me.TablaImportar.Item(FechaMov.Index, posicion).Value.ToString.Substring(0, 2),
                   consecutivo, Checa_tipo(Me.TablaImportar.Item(TipoPoliza.Index, posicion).Value, Me.lstCliente.SelectItem),
                   Me.TablaImportar.Item(FechaMov.Index, posicion).Value, "Venta Terreno" & " " & Trim(Me.TablaImportar.Item(UUID.Index, posicion).Value), "Carga", Me.TablaImportar.Item(Numero.Index, posicion).Value, Trim(Me.TablaImportar.Item(UUID.Index, posicion).Value)) = True Then


            Contabilidad(Me.TablaImportar.Item(TipoPoliza.Index, posicion).Value.ToString.Substring(0, 3), Me.TablaImportar.Item(Tipo.Index, posicion).Value,
                              Me.TablaImportar.Item(Precio_Venta.Index, posicion).Value, Me.TablaImportar.Item(Bo.Index, posicion).Value, Me.TablaImportar.Item(CtaOrigen.Index, posicion).Value, Me.TablaImportar.Item(BD.Index, posicion).Value, Me.TablaImportar.Item(CtaDestino.Index, posicion).Value, Me.TablaImportar.Item(Nocheque.Index, posicion).Value,
                              posicion, poliza_Sistema, Me.TablaImportar.Item(Año.Index, posicion).Value,
                               Me.TablaImportar.Item(Mes.Index, posicion).Value, Me.TablaImportar.Item(FechaMov.Index, posicion).Value, RFC, Me.TablaImportar.Item(UUID.Index, posicion).Value)
            Crear_detalle(posicion, poliza_Sistema)
        End If
    End Sub
    Private Sub Actualiza_Registro(ByVal poliza As String, ByVal UUID As Integer)
        Dim sql As String = " UPDATE dbo.VentaTerrenos
                            SET Id_Poliza = '" & poliza & "'
                            WHERE UUID = '" & UUID & "'  AND Id_Empresa = " & Me.lstCliente.SelectItem & ""
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("Terrenos", sql)
        End If

    End Sub
    Private Sub Crear_detalle(ByVal p As Integer, ByVal pol As String)

        Dim item As Integer = 1
        If Me.TablaImportar.Item(CtaPago.Index, p).Value <> "" Then
            Crea_detalle_poliza(pol, item, Me.TablaImportar.Item(Precio_Venta.Index, p).Value, 0, Me.TablaImportar.Item(CtaPago.Index, p).Value.ToString().Replace("-", ""), "")
            item += 1
        End If
        If Me.TablaImportar.Item(Referencia.Index, p).Value <> "" Then
            Crea_detalle_poliza(pol, item, 0, Me.TablaImportar.Item(Precio_Adquisicion.Index, p).Value, Me.TablaImportar.Item(Referencia.Index, p).Value.ToString().Replace("-", ""), "")
            item += 1
        End If
        If Me.TablaImportar.Item(Utilidad_Contable.Index, p).Value > 0 Then
            Crea_detalle_poliza(pol, item, 0, Me.TablaImportar.Item(Utilidad_Contable.Index, p).Value, Me.TablaImportar.Item(CtaUtilidad.Index, p).Value.ToString().Replace("-", ""), "")
        End If
        If Me.TablaImportar.Item(Perdida_Contable.Index, p).Value <> "" Then
            Crea_detalle_poliza(pol, item, Me.TablaImportar.Item(Perdida_Contable.Index, p).Value, 0, Me.TablaImportar.Item(CtaPerdida.Index, p).Value.ToString().Replace("-", ""), "")
        End If
        Exit Sub
    End Sub
    Private Sub Crea_detalle_poliza(ByVal id_poliza As String, ByVal item As Integer, ByVal cargo As Decimal,
                                       ByVal Abono As Decimal, ByVal cuenta As String, ByVal cheque As String)
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Detalle_Polizas"
        sql &= "(   "
        sql &= " ID_poliza,      "
        sql &= " ID_item,       "
        sql &= " Cargo,          "
        sql &= " Abono,         "
        sql &= " Fecha_captura,  "
        sql &= " Movto,"
        sql &= " Cuenta, "
        sql &= " No_cheque  "
        sql &= " ) "
        sql &= " VALUES "
        sql &= "( "
        sql &= " '" & id_poliza & "'	," '@id_poliza,     
        sql &= "" & item & "," '@id_item,       
        sql &= "" & cargo & "," '@cargo,         
        sql &= "" & Abono & "," '@abono,         
        sql &= "" & Eventos.Sql_hoy() & "," '@fecha_captura, 
        sql &= " 'A'	," '@movto,         
        sql &= " " & cuenta & "	," '@cuenta,        
        sql &= " '" & cheque & "'" '@no_cheque      
        sql &= " 	)"
        If Eventos.Comando_sql(sql) > 0 Then
        End If
    End Sub
    Private Function Creapoliza(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String, ByVal dia As String,
                         ByVal consecutivo As Integer, ByVal tipo As Integer, ByVal fecha As String,
                         ByVal concepto As String, ByVal movimiento As String, ByVal num_pol As Integer, ByVal UUID As String)
        Dim hacer As Boolean
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Polizas"
        sql &= "("
        sql &= " 	ID_poliza,      "
        sql &= "     ID_anio,        "
        sql &= "     ID_mes,        "
        sql &= "     ID_dia,        "
        sql &= "     consecutivo,    "
        sql &= "     Num_Pol,    "
        sql &= "     Id_Tipo_Pol_Sat,"
        sql &= "     Fecha,          "
        sql &= "     Concepto,      "
        sql &= "     Id_Empresa,     "
        sql &= "     No_Mov,        "
        sql &= "     Fecha_captura,  "
        sql &= "     Movto,         "
        sql &= "     Usuario,Aplicar_Poliza         "
        sql &= " 	)               "
        sql &= " VALUES              "
        sql &= " 	(               "
        sql &= " 	'" & id_poliza & "'," '@id_poliza,         
        sql &= " 	" & anio & "," '@id_anio,           
        sql &= " 	'" & mes & "'," '@id_mes,     
        sql &= " 	'" & dia & "'," '@id_dia,     
        sql &= " 	" & consecutivo & "," '@consecutivo,   
        sql &= " 	" & num_pol & "," '@num_pol,  
        sql &= " 	" & tipo & "," '@id_tipo_poliza, 
        sql &= " 	" & Eventos.Sql_hoy(fecha) & "," '@fecha,             
        sql &= " 	'" & concepto & "'," '@concepto,          
        sql &= " 	" & Me.lstCliente.SelectItem & "," '@Id_Empresa,        
        sql &= " 	'" & movimiento & "'," '@no_mov,            
        sql &= " 	" & Eventos.Sql_hoy("" & dia & "/" & mes & "/" & anio & "") & "," '@fecha_captura,     
        sql &= " 	'A'," '@movto,             
        sql &= "  '" & Eventos.Usuario(My.Forms.Inicio.LblUsuario.Text) & "', 1" '@usuario            
        sql &= " 	) "

        If Eventos.Comando_sql(sql) > 0 Then
            hacer = True
            Actualiza_Registro(id_poliza, UUID)
        Else
            hacer = False
        End If
        Return hacer
    End Function

    Private Function Calcula_poliza(ByVal i As Integer)
        Dim mess As String = IIf(Len(Me.TablaImportar.Item(Mes.Index, i).Value) = 1, "0" & Me.TablaImportar.Item(Mes.Index, i).Value, Me.TablaImportar.Item(Mes.Index, i).Value)
        Dim poliza As String = Eventos.Num_polizaS(Me.lstCliente.SelectItem, Checa_tipo(Me.TablaImportar.Item(TipoPoliza.Index, i).Value, Me.lstCliente.SelectItem), Me.TablaImportar.Item(Anio.Index, i).Value, mess, Busca_tipificar(Me.TablaImportar.Item(TipoPoliza.Index, i).Value))

        Return poliza
    End Function
    Private Function Checa_tipo(ByVal tipo As String, ByVal cliente As Integer)
        Dim clave As String = ""
        Dim sql As String = "SELECT Id_Tipo_Pol_Sat FROM Tipos_Poliza_Sat WHERE Id_Empresa= " & cliente & " AND clave = '" & tipo.Substring(0, 3) & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            clave = ds.Tables(0).Rows(0)("Id_Tipo_Pol_Sat")
        Else
            clave = 0
        End If
        Return clave
    End Function
    Private Function Busca_tipificar(ByVal tipos As String)
        Dim tipo As String = ""
        Dim sql As String = " SELECT Id_Tipo_Pol_Sat FROM Tipos_Poliza_Sat WHERE Id_Empresa= " & Me.lstCliente.SelectItem & " AND clave = '" & tipos.Substring(0, 3) & "' "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            tipo = ds.Tables(0).Rows(0)(0)
        Else
            tipo = "N/A"
        End If
        Return tipo
    End Function
    Private Sub CargarRegistros(ByVal Clt As Integer, ByVal anio As Integer)
        Dim sql As String = "SELECT 	Id_Venta,	Referencia,	Cliente,	Convert(varchar,Fecha_Adquisicion,103) as Fecha_Adquisicion ,	Precio_Adquisicion,	Convert(varchar,Fecha_de_Venta,103) as Fecha_de_Venta ,	Precio_Venta,	CtaPago,
    	Utilidad_Contable,	CtaUtilidad,	Perdida_Contable,	CtaPerdida,Convert(varchar, UltimoMes, 103) as UltimoMes,	ImporteUltimomes,	Convert(varchar,Fecha_Adq,103) as Fecha_Adq ,	INPC_Adquisicion,	Factor_Actualizacion,	Precio_Adq_Actualizado,
    	Precio_Venta_Fiscal,	Utilidad_Fiscal,	Perdida_Fiscal,	Anio,	Mes,	TipoPol,	Numpol,	UUID,	Tipo,	Banco_Origen,	Cuenta_Origen,	Banco_Destino,	Cuenta_Destino,
    	Convert(varchar,Fecha_Transaccion,103) as Fecha_Transaccion ,	No_Cheque,	Id_Empresa,	Id_Poliza FROM dbo.VentaTerrenos where Id_Empresa = " & Clt & " and Anio = " & anio & " and Id_Poliza IS NULL "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(i)
                Me.TablaImportar.Item(0, i).Value = False
                Me.TablaImportar.Item(Referencia.Index, i).Value = ds.Tables(0).Rows(i)("Referencia")
                Me.TablaImportar.Item(Cliente.Index, i).Value = ds.Tables(0).Rows(i)("Cliente")
                Me.TablaImportar.Item(Fecha_Adquisicion.Index, i).Value = ds.Tables(0).Rows(i)("Fecha_Adquisicion")
                Me.TablaImportar.Item(Precio_Adquisicion.Index, i).Value = ds.Tables(0).Rows(i)("Precio_Adquisicion")
                Me.TablaImportar.Item(Fecha_de_Venta.Index, i).Value = ds.Tables(0).Rows(i)("Fecha_de_Venta")
                Me.TablaImportar.Item(Precio_Venta.Index, i).Value = ds.Tables(0).Rows(i)("Precio_Venta")

                Me.TablaImportar.Item(CtaPago.Index, i).Value = ds.Tables(0).Rows(i)("CtaPago")
                Me.TablaImportar.Item(Utilidad_Contable.Index, i).Value = ds.Tables(0).Rows(i)("Utilidad_Contable")
                Me.TablaImportar.Item(CtaUtilidad.Index, i).Value = ds.Tables(0).Rows(i)("CtaUtilidad")
                Me.TablaImportar.Item(Perdida_Contable.Index, i).Value = Trim(ds.Tables(0).Rows(i)("Perdida_Contable"))
                Me.TablaImportar.Item(CtaPerdida.Index, i).Value = ds.Tables(0).Rows(i)("CtaPerdida")
                Me.TablaImportar.Item(UltimoMes.Index, i).Value = ds.Tables(0).Rows(i)("UltimoMes")
                Me.TablaImportar.Item(ImporteUltimomes.Index, i).Value = ds.Tables(0).Rows(i)("ImporteUltimomes")
                Me.TablaImportar.Item(Fecha_Adq.Index, i).Value = ds.Tables(0).Rows(i)("Fecha_Adq")

                Me.TablaImportar.Item(INPC_Adquisicion.Index, i).Value = ds.Tables(0).Rows(i)("INPC_Adquisicion")
                Me.TablaImportar.Item(Factor_Actualizacion.Index, i).Value = ds.Tables(0).Rows(i)("Factor_Actualizacion")
                Me.TablaImportar.Item(Precio_Adq_Actualizado.Index, i).Value = ds.Tables(0).Rows(i)("Precio_Adq_Actualizado")
                Me.TablaImportar.Item(Precio_Venta_Fiscal.Index, i).Value = ds.Tables(0).Rows(i)("Precio_Venta_Fiscal")
                Me.TablaImportar.Item(Utilidad_Fiscal.Index, i).Value = ds.Tables(0).Rows(i)("Utilidad_Fiscal")

                Me.TablaImportar.Item(Perdida_Fiscal.Index, i).Value = ds.Tables(0).Rows(i)("Perdida_Fiscal")
                Me.TablaImportar.Item(Año.Index, i).Value = ds.Tables(0).Rows(i)("Anio")
                Me.TablaImportar.Item(Mes.Index, i).Value = ds.Tables(0).Rows(i)("Mes")
                Try
                    If Trim(ds.Tables(0).Rows(i)("TipoPol")) <> "" Then
                        Fila.Cells(TipoPoliza.Index).Value = Me.TipoPoliza.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("TipoPol")), Me.TipoPoliza))
                    End If

                Catch ex As Exception
                End Try


                Me.TablaImportar.Item(Numero.Index, i).Value = ds.Tables(0).Rows(i)("Numpol")
                Me.TablaImportar.Item(UUID.Index, i).Value = ds.Tables(0).Rows(i)("UUID")
                Try
                    If Trim(ds.Tables(0).Rows(i)("Tipo")) <> "" Then
                        Fila.Cells(Tipo.Index).Value = Me.Tipo.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("Tipo")), Me.Tipo))
                    End If

                Catch ex As Exception
                End Try
                Me.TablaImportar.Item(Bo.Index, i).Value = ds.Tables(0).Rows(i)("Banco_Origen")
                Me.TablaImportar.Item(CtaOrigen.Index, i).Value = ds.Tables(0).Rows(i)("Cuenta_Origen")
                Me.TablaImportar.Item(BD.Index, i).Value = ds.Tables(0).Rows(i)("Banco_Destino")
                Me.TablaImportar.Item(CtaDestino.Index, i).Value = ds.Tables(0).Rows(i)("Cuenta_Destino")
                Me.TablaImportar.Item(FechaMov.Index, i).Value = ds.Tables(0).Rows(i)("Fecha_Transaccion")
                Me.TablaImportar.Item(Nocheque.Index, i).Value = ds.Tables(0).Rows(i)("No_Cheque")
                Liberar_Proceso(i)
            Next
        End If
    End Sub
    Private Function Obtener_Index(ByVal valor As String, ByVal Col As DataGridViewComboBoxColumn)
        Dim Indice As Integer = -1

        For i As Integer = 0 To Col.Items.Count - 1
            If valor = Trim(Col.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next

        Return Indice
    End Function

    Private Sub LstAnio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstAnio.SelectedIndexChanged
        CargarRegistros(Me.lstCliente.SelectItem, Me.LstAnio.Text.Trim())
    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub
End Class