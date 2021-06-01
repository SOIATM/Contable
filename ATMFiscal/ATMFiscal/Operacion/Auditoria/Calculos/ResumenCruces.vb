Imports Telerik.WinControls
Public Class ResumenCruces
    Dim m = Now.Date.Month.ToString
    Dim a = Str(DateTime.Now.Year)

    Private Sub CmdListar_Click(sender As Object, e As EventArgs) Handles CmdListar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim sql As String = " SELECT Descripcion,  Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre,Diciembre, Anio
	                               FROM     ParametroCruces 
	                               WHERE  Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio  =" & Me.Anio2.Text.Trim() & "  ORDER BY Id_Parametro_Cruce  "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If RadMessageBox.Show("Ya existe plantilla de la Empresa " & Me.lstCliente.SelectText & " para el año " & Me.Anio2.Text.Trim() & " deseas eliminarla?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                sql = "Delete From ParametroCruces where Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio  =" & Trim(Me.Anio2.Text) & " "
                If Eventos.Comando_sql(sql) > 0 Then
                    sql = " SELECT Descripcion,   Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre,Diciembre, Anio 
	                               FROM     ParametroCruces 
	                               WHERE  Id_Empresa =  0 and Anio  = 2019  ORDER BY Id_Parametro_Cruce  "
                    ds = Eventos.Obtener_DS(sql)
                    If ds.Tables(0).Rows.Count > 0 Then

                        Me.Tabla.RowCount = ds.Tables(0).Rows.Count

                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            Dim Fila As DataGridViewRow = Me.Tabla.Rows(i)
                            Me.Tabla.Item(Parametro.Index, i).Value = ds.Tables(0).Rows(i)("Descripcion")
                            Me.Tabla.Item(Enero.Index, i).Value = ds.Tables(0).Rows(i)("Enero")
                            Me.Tabla.Item(Febrero.Index, i).Value = ds.Tables(0).Rows(i)("Febrero")
                            Me.Tabla.Item(Marzo.Index, i).Value = ds.Tables(0).Rows(i)("Marzo")
                            Me.Tabla.Item(Abril.Index, i).Value = ds.Tables(0).Rows(i)("Abril")
                            Me.Tabla.Item(Mayo.Index, i).Value = ds.Tables(0).Rows(i)("Mayo")
                            Me.Tabla.Item(Junio.Index, i).Value = ds.Tables(0).Rows(i)("Junio")
                            Me.Tabla.Item(Julio.Index, i).Value = ds.Tables(0).Rows(i)("Julio")
                            Me.Tabla.Item(Agosto.Index, i).Value = ds.Tables(0).Rows(i)("Agosto")
                            Me.Tabla.Item(Septiembre.Index, i).Value = ds.Tables(0).Rows(i)("Septiembre")
                            Me.Tabla.Item(Octubre.Index, i).Value = ds.Tables(0).Rows(i)("Octubre")
                            Me.Tabla.Item(Noviembre.Index, i).Value = ds.Tables(0).Rows(i)("Noviembre")

                            Me.Tabla.Item(Diciembre.Index, i).Value = ds.Tables(0).Rows(i)("Diciembre")

                        Next
                        Carga_Obligaciones(Me.Tabla.RowCount)
                        Guarda_Ctas()
                    End If
                End If


            Else
                sql = " SELECT Descripcion,  Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre,Diciembre, Anio
	                               FROM     ParametroCruces 
	                               WHERE  Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio  =" & Me.Anio2.Text.Trim() & "  ORDER BY Id_Parametro_Cruce  "
                ds = Eventos.Obtener_DS(sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.Tabla.RowCount = ds.Tables(0).Rows.Count
                    Dim frm As New BarraProcesovb
                    frm.Show()
                    frm.Barra.Minimum = 0
                    frm.Barra.Maximum = Me.Tabla.RowCount - 1
                    Me.Cursor = Cursors.AppStarting
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim Fila As DataGridViewRow = Me.Tabla.Rows(i)
                        Me.Tabla.Item(Parametro.Index, i).Value = ds.Tables(0).Rows(i)("Descripcion")
                        Me.Tabla.Item(Enero.Index, i).Value = ds.Tables(0).Rows(i)("Enero")
                        Me.Tabla.Item(Febrero.Index, i).Value = ds.Tables(0).Rows(i)("Febrero")
                        Me.Tabla.Item(Marzo.Index, i).Value = ds.Tables(0).Rows(i)("Marzo")
                        Me.Tabla.Item(Abril.Index, i).Value = ds.Tables(0).Rows(i)("Abril")
                        Me.Tabla.Item(Mayo.Index, i).Value = ds.Tables(0).Rows(i)("Mayo")
                        Me.Tabla.Item(Junio.Index, i).Value = ds.Tables(0).Rows(i)("Junio")
                        Me.Tabla.Item(Julio.Index, i).Value = ds.Tables(0).Rows(i)("Julio")
                        Me.Tabla.Item(Agosto.Index, i).Value = ds.Tables(0).Rows(i)("Agosto")
                        Me.Tabla.Item(Septiembre.Index, i).Value = ds.Tables(0).Rows(i)("Septiembre")
                        Me.Tabla.Item(Octubre.Index, i).Value = ds.Tables(0).Rows(i)("Octubre")
                        Me.Tabla.Item(Noviembre.Index, i).Value = ds.Tables(0).Rows(i)("Noviembre")

                        Me.Tabla.Item(Diciembre.Index, i).Value = ds.Tables(0).Rows(i)("Diciembre")

                        frm.Barra.Value = i
                    Next
                    frm.Close()
                    RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                    Me.Cursor = Cursors.Arrow
                End If



            End If
        Else
            sql = " SELECT Descripcion,   Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre,Diciembre, Anio 
	                               FROM     ParametroCruces 
	                               WHERE  Id_Empresa =  0 and Anio  = 2019  ORDER BY Id_Parametro_Cruce  "
            ds = Eventos.Obtener_DS(sql)
            If ds.Tables(0).Rows.Count > 0 Then

                Me.Tabla.RowCount = ds.Tables(0).Rows.Count

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim Fila As DataGridViewRow = Me.Tabla.Rows(i)
                    Me.Tabla.Item(Parametro.Index, i).Value = ds.Tables(0).Rows(i)("Descripcion")
                    Me.Tabla.Item(Enero.Index, i).Value = ds.Tables(0).Rows(i)("Enero")
                    Me.Tabla.Item(Febrero.Index, i).Value = ds.Tables(0).Rows(i)("Febrero")
                    Me.Tabla.Item(Marzo.Index, i).Value = ds.Tables(0).Rows(i)("Marzo")
                    Me.Tabla.Item(Abril.Index, i).Value = ds.Tables(0).Rows(i)("Abril")
                    Me.Tabla.Item(Mayo.Index, i).Value = ds.Tables(0).Rows(i)("Mayo")
                    Me.Tabla.Item(Junio.Index, i).Value = ds.Tables(0).Rows(i)("Junio")
                    Me.Tabla.Item(Julio.Index, i).Value = ds.Tables(0).Rows(i)("Julio")
                    Me.Tabla.Item(Agosto.Index, i).Value = ds.Tables(0).Rows(i)("Agosto")
                    Me.Tabla.Item(Septiembre.Index, i).Value = ds.Tables(0).Rows(i)("Septiembre")
                    Me.Tabla.Item(Octubre.Index, i).Value = ds.Tables(0).Rows(i)("Octubre")
                    Me.Tabla.Item(Noviembre.Index, i).Value = ds.Tables(0).Rows(i)("Noviembre")
                    Me.Tabla.Item(Diciembre.Index, i).Value = ds.Tables(0).Rows(i)("Diciembre")

                Next
                Carga_Obligaciones(Me.Tabla.RowCount)
                Guarda_Ctas()
            End If
        End If
    End Sub
    Private Sub Crear_Filas(ByVal Anio As Integer, ByVal Cliente As Integer)

        Dim sql As String = " SELECT Descripcion,  Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre,Diciembre, Anio
	                               FROM     ParametroCruces 
	                               WHERE  Id_Empresa = " & Cliente & " and Anio  =" & Anio & "  ORDER BY Id_Parametro_Cruce  "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.RowCount = ds.Tables(0).Rows.Count
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.Tabla.RowCount - 1
            Me.Cursor = Cursors.AppStarting
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim Fila As DataGridViewRow = Me.Tabla.Rows(i)
                Me.Tabla.Item(Parametro.Index, i).Value = ds.Tables(0).Rows(i)("Descripcion")
                Me.Tabla.Item(Enero.Index, i).Value = ds.Tables(0).Rows(i)("Enero")
                Me.Tabla.Item(Febrero.Index, i).Value = ds.Tables(0).Rows(i)("Febrero")
                Me.Tabla.Item(Marzo.Index, i).Value = ds.Tables(0).Rows(i)("Marzo")
                Me.Tabla.Item(Abril.Index, i).Value = ds.Tables(0).Rows(i)("Abril")
                Me.Tabla.Item(Mayo.Index, i).Value = ds.Tables(0).Rows(i)("Mayo")
                Me.Tabla.Item(Junio.Index, i).Value = ds.Tables(0).Rows(i)("Junio")
                Me.Tabla.Item(Julio.Index, i).Value = ds.Tables(0).Rows(i)("Julio")
                Me.Tabla.Item(Agosto.Index, i).Value = ds.Tables(0).Rows(i)("Agosto")
                Me.Tabla.Item(Septiembre.Index, i).Value = ds.Tables(0).Rows(i)("Septiembre")
                Me.Tabla.Item(Octubre.Index, i).Value = ds.Tables(0).Rows(i)("Octubre")
                Me.Tabla.Item(Noviembre.Index, i).Value = ds.Tables(0).Rows(i)("Noviembre")

                Me.Tabla.Item(Diciembre.Index, i).Value = ds.Tables(0).Rows(i)("Diciembre")

                frm.Barra.Value = i
            Next
            frm.Close()
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow

        Else
            If RadMessageBox.Show("No existe plantilla de la Empresa " & Me.lstCliente.SelectText & " para el año " & Anio & " deseas crearla?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then


                sql = " SELECT Descripcion,   Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre,Diciembre, Anio 
	                               FROM     ParametroCruces 
	                               WHERE  Id_Empresa =  0 and Anio  = 2019  ORDER BY Id_Parametro_Cruce  "
                ds = Eventos.Obtener_DS(sql)
                If ds.Tables(0).Rows.Count > 0 Then

                    Me.Tabla.RowCount = ds.Tables(0).Rows.Count

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim Fila As DataGridViewRow = Me.Tabla.Rows(i)
                        Me.Tabla.Item(Parametro.Index, i).Value = ds.Tables(0).Rows(i)("Descripcion")
                        Me.Tabla.Item(Enero.Index, i).Value = ds.Tables(0).Rows(i)("Enero")
                        Me.Tabla.Item(Febrero.Index, i).Value = ds.Tables(0).Rows(i)("Febrero")
                        Me.Tabla.Item(Marzo.Index, i).Value = ds.Tables(0).Rows(i)("Marzo")
                        Me.Tabla.Item(Abril.Index, i).Value = ds.Tables(0).Rows(i)("Abril")
                        Me.Tabla.Item(Mayo.Index, i).Value = ds.Tables(0).Rows(i)("Mayo")
                        Me.Tabla.Item(Junio.Index, i).Value = ds.Tables(0).Rows(i)("Junio")
                        Me.Tabla.Item(Julio.Index, i).Value = ds.Tables(0).Rows(i)("Julio")
                        Me.Tabla.Item(Agosto.Index, i).Value = ds.Tables(0).Rows(i)("Agosto")
                        Me.Tabla.Item(Septiembre.Index, i).Value = ds.Tables(0).Rows(i)("Septiembre")
                        Me.Tabla.Item(Octubre.Index, i).Value = ds.Tables(0).Rows(i)("Octubre")
                        Me.Tabla.Item(Noviembre.Index, i).Value = ds.Tables(0).Rows(i)("Noviembre")

                        Me.Tabla.Item(Diciembre.Index, i).Value = ds.Tables(0).Rows(i)("Diciembre")

                    Next
                    Carga_Obligaciones(Me.Tabla.RowCount)
                    Guarda_Ctas()
                End If
            Else
                Exit Sub
            End If
        End If


    End Sub
    Private Sub Guarda_Ctas()
        Dim sql As String = ""
        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = Me.Tabla.RowCount - 1
        Me.Cursor = Cursors.AppStarting
        For i As Integer = 0 To Me.Tabla.Rows.Count - 1
            sql = "INSERT INTO dbo.ParametroCruces 	
	                            ( Descripcion,Enero,Febrero,Marzo,Abril,Mayo,Junio,Julio,Agosto,Septiembre,Octubre,Noviembre,Diciembre,Anio,Id_Empresa)
	                        	 VALUES 	( '" & Trim(Me.Tabla.Item(Parametro.Index, i).Value) & "',	'" & Trim(Me.Tabla.Item(Enero.Index, i).Value) & "',	
	                        '" & Trim(Me.Tabla.Item(Febrero.Index, i).Value) & "',	'" & Trim(Me.Tabla.Item(Marzo.Index, i).Value) & "','" & Trim(Me.Tabla.Item(Abril.Index, i).Value) & "',	
	                        '" & Trim(Me.Tabla.Item(Mayo.Index, i).Value) & "',	'" & Trim(Me.Tabla.Item(Junio.Index, i).Value) & "',	'" & Trim(Me.Tabla.Item(Julio.Index, i).Value) & "',
	                        '" & Trim(Me.Tabla.Item(Agosto.Index, i).Value) & "',	'" & Trim(Me.Tabla.Item(Septiembre.Index, i).Value) & "',	'" & Trim(Me.Tabla.Item(Octubre.Index, i).Value) & "',
	                        '" & Trim(Me.Tabla.Item(Noviembre.Index, i).Value) & "',	'" & Trim(Me.Tabla.Item(Diciembre.Index, i).Value) & "',	
	                        " & Trim(Me.Anio2.Text) & ",	" & Me.lstCliente.SelectItem & ")
	"
            If Eventos.Comando_sql(sql) = 0 Then
                RadMessageBox.Show("No se guardo la informacion error en " & Trim(Me.Tabla.Item(Parametro.Index, i).Value) & " verifique la Informacion ", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                sql = "Delete From ParametroCruces where Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio  =" & Trim(Me.Anio2.Text) & " "
                If Eventos.Comando_sql(sql) = 0 Then

                End If
            End If
            frm.Barra.Value = i
        Next
        frm.Close()
        RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If Me.lstCliente.SelectText <> "" Then
            If RadMessageBox.Show("Se actualizara la informacion de la Empresa  " & Me.lstCliente.SelectText & " para el año " & Me.Anio2.Text.Trim() & " esto es correcto?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim Sql As String = "Delete From ParametroCruces where Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio  =" & Trim(Me.Anio2.Text) & " "
                If Eventos.Comando_sql(Sql) > 0 Then
                    Guarda_Ctas()
                End If
            Else
                Exit Sub
            End If
        Else
            RadMessageBox.Show("Debes seleccionar una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub

    Private Sub CmdExportaanuales_Click(sender As Object, e As EventArgs) Handles CmdExportaanuales.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.Tabla.RowCount > 0 Then
            If Me.Tabla.Columns.Count > 256 Then
                RadMessageBox.Show("El rango de fechas sobrepasa las columnas de una hoja de excel, disminuye el rango...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                Exit Sub
            End If

            Dim excel As Microsoft.Office.Interop.Excel.Application = Eventos.NuevoExcel("vacio", False)
            'Enero
            For col As Integer = 1 To Me.Tabla.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.Tabla.Columns(col - 1).HeaderText, 1)
            Next
            For i As Integer = 0 To Me.Tabla.RowCount - 1
                For j As Integer = 1 To Me.Tabla.Columns.Count - 1
                    Eventos.EscribeExcelHojas(excel, i + 1, j, Me.Tabla.Item(j - 1, i).Value, 1)
                Next

            Next

            Eventos.Mostrar_Excel(excel)
        Else
            RadMessageBox.Show("No hay datos para exportar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
        End If
    End Sub

    Private Sub ResumenCruces_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Eventos.DiseñoTabla(Me.Tabla)
        Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = My.Forms.Inicio.Clt

        If Len(m) < 2 Then
            m = "0" & m
        End If
        Me.ComboMes.Text = m
        If Len(m) < 2 Then
            m = "0" & m
        End If
        Me.ComboMes2.Text = m
        Me.Anio2.Text = Str(DateTime.Now.Year)
        For i = DateTime.Now.Year To DateTime.Now.Year - 10 Step -1
            If i >= 2010 Then
                Me.Anio2.Items.Add(Str(i))
            End If
        Next
    End Sub
    Private Sub Carga_Obligaciones(ByVal posicion As Integer)
        Dim sql As String = " SELECT DISTINCT  Obligaciones.Descripcion FROM Obligaciones  INNER JOIN empresa ON Empresa.ID_empresa  = Obligaciones.Id_Empresa WHERE Obligaciones.Id_Empresa  = 1 "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.RowCount = Me.Tabla.RowCount + ds.Tables(0).Rows.Count

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim Fila As DataGridViewRow = Me.Tabla.Rows(i)
                Me.Tabla.Item(Parametro.Index, posicion + i).Value = ds.Tables(0).Rows(i)("Descripcion")
                Me.Tabla.Item(Enero.Index, posicion + i).Value = ""
                Me.Tabla.Item(Febrero.Index, posicion + i).Value = ""
                Me.Tabla.Item(Marzo.Index, posicion + i).Value = ""
                Me.Tabla.Item(Abril.Index, posicion + i).Value = ""
                Me.Tabla.Item(Mayo.Index, posicion + i).Value = ""
                Me.Tabla.Item(Junio.Index, posicion + i).Value = ""
                Me.Tabla.Item(Julio.Index, posicion + i).Value = ""
                Me.Tabla.Item(Agosto.Index, posicion + i).Value = ""
                Me.Tabla.Item(Septiembre.Index, posicion + i).Value = ""
                Me.Tabla.Item(Octubre.Index, posicion + i).Value = ""
                Me.Tabla.Item(Noviembre.Index, posicion + i).Value = ""
                Me.Tabla.Item(Diciembre.Index, posicion + i).Value = ""

            Next
        End If
    End Sub

    Private Sub CmbBuscarMasivo_Click(sender As Object, e As EventArgs) Handles CmbBuscarMasivo.Click
        If Me.lstCliente.SelectText <> "" Then
            If Trim(Me.Anio2.Text) <> "" Then
                If Me.Tabla.Rows.Count > 0 Then
                    Me.Tabla.Rows.Clear()
                    Crear_Filas(Trim(Me.Anio2.Text), Me.lstCliente.SelectItem)
                    For Columna As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
                        Dim Mes As String = Eventos.MesEnletra(IIf(Len(Columna.ToString()) = 1, "0" & Columna.ToString(), Columna))
                        For Each col As DataGridViewColumn In Me.Tabla.Columns
                            If Mes = col.Name Then
                                Cruzar(col.Index, "Activos", 7, Mes)
                                Cruzar(col.Index, "Aguinaldo", 8, Mes)
                                Cruzar(col.Index, "Amortizaciones", 9, Mes)
                                Cruzar(col.Index, "Depreciaciones", 10, Mes)
                                Cruzar(col.Index, "Gastos Diferidos", 11, Mes)
                                Cruzar(col.Index, "ISR provisional Ejercicio Anterior", 12, Mes)
                                Cruzar(col.Index, "Nominas", 13, Mes)
                                Cruzar(col.Index, "Patrimonio", 14, Mes)
                                Cruzar(col.Index, "Prima Vacacional", 15, Mes)
                                Cruzar(col.Index, "Provision C y V", 16, Mes)
                                Cruzar(col.Index, "Provision de ISN", 17, Mes)
                                Cruzar(col.Index, "Provision IMSS", 18, Mes)
                                Cruzar(col.Index, "Provision INFONAVIT", 19, Mes)
                                Cruzar(col.Index, "Provision ISR Mensual", 20, Mes)
                                Cruzar(col.Index, "Provision SAR", 21, Mes)
                            End If
                        Next
                    Next
                Else
                    Crear_Filas(Trim(Me.Anio2.Text), Me.lstCliente.SelectItem)
                    If Me.Tabla.Rows.Count > 0 Then

                        For Columna As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
                            Dim Mes As String = Eventos.MesEnletra(IIf(Len(Columna.ToString()) = 1, "0" & Columna.ToString(), Columna))
                            For Each col As DataGridViewColumn In Me.Tabla.Columns
                                If Mes = col.Name Then
                                    Cruzar(col.Index, "Activos", 7, Mes)
                                    Cruzar(col.Index, "Aguinaldo", 8, Mes)
                                    Cruzar(col.Index, "Amortizaciones", 9, Mes)
                                    Cruzar(col.Index, "Depreciaciones", 10, Mes)
                                    Cruzar(col.Index, "Gastos Diferidos", 11, Mes)
                                    Cruzar(col.Index, "ISR provisional Ejercicio Anterior", 12, Mes)
                                    Cruzar(col.Index, "Nominas", 13, Mes)
                                    Cruzar(col.Index, "Patrimonio", 14, Mes)
                                    Cruzar(col.Index, "Prima Vacacional", 15, Mes)
                                    Cruzar(col.Index, "Provision C y V", 16, Mes)
                                    Cruzar(col.Index, "Provision de ISN", 17, Mes)
                                    Cruzar(col.Index, "Provision IMSS", 18, Mes)
                                    Cruzar(col.Index, "Provision INFONAVIT", 19, Mes)
                                    Cruzar(col.Index, "Provision ISR Mensual", 20, Mes)
                                    Cruzar(col.Index, "Provision SAR", 21, Mes)
                                End If
                            Next
                        Next
                    End If
                End If
            Else
                RadMessageBox.Show("Debes seleccionar un Año para consultar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            End If
        Else
            RadMessageBox.Show("Debes seleccionar una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If

    End Sub
    Private Function Cargar_Cuentas_cero(ByVal Mes As String, ByVal Campo As String)
        Dim cuantos As Integer = 0
        Dim sql As String = "SELECT count (" & Campo & ") AS Cuantos FROM Balanzas  WHERE " & Campo & " <> '' AND " & Campo & " IS NOT NULL AND Id_Empresa = " & Me.lstCliente.SelectItem & " AND Mes = " & Mes & " AND Anio = " & Me.Anio2.Text.Trim() & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            cuantos = ds.Tables(0).Rows(0)(0)
        End If
        Return cuantos

    End Function
    Private Function Cargar_Cuentas_Impuestos(ByVal Campo As String)
        Dim cuantos As Decimal = 0
        Dim sql As String = "SELECT Sum(CASE WHEN " & Campo & " <0 THEN " & Campo & " *-1 WHEN " & Campo & " >0 THEN " & Campo & " END) AS Importe FROM Calculo_Impuestos  WHERE   Naturaleza = 'Diferencia'    AND Id_Empresa = " & Me.lstCliente.SelectItem & "  AND Anio = " & Me.Anio2.Text.Trim() & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim valor = IIf(IsDBNull(ds.Tables(0).Rows(0)(0)), 0, ds.Tables(0).Rows(0)(0))
            If valor > 1 Or valor < -1 Then
                cuantos = IIf(IsDBNull(ds.Tables(0).Rows(0)(0)), 0, ds.Tables(0).Rows(0)(0))
            End If

        Else
            RadMessageBox.Show("No se ha realizado el calculo de Impuestos del mes  de  " & Campo & "...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
        End If
        Return cuantos

    End Function
    Private Function BuscarObligacion(ByVal Campo As String, ByVal Descripcion As String)
        Dim Status As String = ""
        Dim Cuentas As New List(Of Tipos_Cruces)
        Dim contador As Integer = 1
        Dim sql As String = "SELECT Obligaciones.Descripcion, Obligaciones_Cuentas.Cuenta  ,CASE WHEN Obligaciones.Abono =1 THEN 'Abono' WHEN Obligaciones.Cargo =1 THEN 'Cargo' WHEN Obligaciones.Saldo_Final = 1 THEN  ' Cargo - Abono ' END AS 	Tipo  
	                            FROM Obligaciones 
	                            INNER JOIN Obligaciones_Cuentas  ON Obligaciones_Cuentas.Id_Obligacion = Obligaciones.Id_Obligacion  
	                            INNER JOIN Obligaciones_Cliente ON Obligaciones_Cliente.Id_Obligacion = Obligaciones.Id_Obligacion 
	                            WHERE obligaciones_cliente.Mes =  '" & Campo & "' AND obligaciones_cliente.Anio = " & Me.Anio2.Text.Trim() & "
                                AND obligaciones.Descripcion = '" & Descripcion.Trim() & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Cuentas.Add(New Tipos_Cruces() With {.Cuenta = Trim(ds.Tables(0).Rows(i)("Cuenta")).ToString, .ID = contador})
                contador += 1
            Next
            Status = Saldo(Trim(ds.Tables(0).Rows(0)("Tipo")).ToString, Cuentas, Campo)
        Else
            Status = "NO"
        End If
        Return Status
    End Function

    Private Function Saldo(ByVal Campo As String, ByVal Cuentas As List(Of Tipos_Cruces), ByVal Mes As String)
        Dim Importe As Decimal = 0
        Dim Status As String = ""
        Dim where As String = ""
        Dim sql As String = ""
        Dim ds As DataSet
        For Each cuenta In Cuentas
            If cuenta.Cuenta.ToString.Substring(4, 4) = "0000" And cuenta.Cuenta.ToString.Substring(8, 4) = "0000" Then ' Nivel 1
                where = " Nivel1 = " & cuenta.Cuenta.ToString.Substring(0, 4) & " AND Nivel2 >= 0000 "
            ElseIf cuenta.Cuenta.ToString.Substring(8, 4) = "0000" And cuenta.Cuenta.ToString.Substring(0, 4) <> "0000" And cuenta.Cuenta.ToString.Substring(4, 4) <> "0000" Then ' Nivel 2
                where = " Nivel1 = " & cuenta.Cuenta.ToString.Substring(0, 4) & " AND Nivel2 = " & cuenta.Cuenta.ToString.Substring(4, 4) & " AND Nivel3 >= " & cuenta.Cuenta.ToString.Substring(8, 4) & " "
            ElseIf cuenta.Cuenta.ToString.Substring(8, 4) <> "0000" And cuenta.Cuenta.ToString.Substring(0, 4) <> "0000" And cuenta.Cuenta.ToString.Substring(4, 4) <> "0000" And cuenta.Cuenta.ToString.Substring(12, 4) = "0000" Then ' Nivel 3
                where = " Nivel1 = " & cuenta.Cuenta.ToString.Substring(0, 4) & " AND Nivel2 = " & cuenta.Cuenta.ToString.Substring(4, 4) & " AND Nivel3 = " & cuenta.Cuenta.ToString.Substring(8, 4) & " AND Nivel4 >= " & cuenta.Cuenta.ToString.Substring(12, 4) & ""
            ElseIf cuenta.Cuenta.ToString.Substring(8, 4) <> "0000" And cuenta.Cuenta.ToString.Substring(0, 4) <> "0000" And cuenta.Cuenta.ToString.Substring(4, 4) <> "0000" And cuenta.Cuenta.ToString.Substring(12, 4) <> "0000" Then ' Nivel 4
                where = " Nivel1 = " & cuenta.Cuenta.ToString.Substring(0, 4) & " AND Nivel2 = " & cuenta.Cuenta.ToString.Substring(4, 4) & " AND Nivel3 = " & cuenta.Cuenta.ToString.Substring(8, 4) & " AND Nivel4 = " & cuenta.Cuenta.ToString.Substring(12, 4) & ""
            End If

            sql = "SELECT CASE WHEN sum(" & Campo & ") IS NULL THEN 0 WHEN sum(" & Campo & ") IS NOT NULL THEN sum(" & Campo & ") END AS Importe  FROM  Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
	                    INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
	                    INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
	                    INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta 
	                    AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
	                    WHERE (Detalle_Polizas." & Campo & " <> 0) AND Polizas.ID_anio = " & Me.Anio2.Text.Trim() & " AND Polizas.ID_mes = " & Eventos.MesEnNumero(Mes.Trim()) & " and " & where & " "
            ds = Eventos.Obtener_DS(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)(0) > 0 Then
                    Importe = Importe + ds.Tables(0).Rows(0)(0)
                End If
            End If

        Next
        If Importe > 0 Then
            Status = "SI"
        Else
            Status = "NO"
        End If

        Return Status

    End Function
    Public Class Tipos_Cruces
        Public Property ID As Integer
        Public Property Cuenta As String

    End Class
    Private Sub CmdCalcular_Click(sender As Object, e As EventArgs) Handles CmdCalcular.Click
        If Me.Tabla.Rows.Count > 0 Then

            For Columna As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)


                Dim Mes As String = Eventos.MesEnletra(IIf(Len(Columna.ToString()) = 1, "0" & Columna.ToString(), Columna))

                For Each col As DataGridViewColumn In Me.Tabla.Columns
                    If Mes = col.Name Then
                        Me.Tabla.Item(col.Index, 0).Value = Cargar_Cuentas_cero(Columna, "Cuenta_Cero")
                        Me.Tabla.Item(col.Index, 1).Value = Cargar_Cuentas_cero(Columna, "Cargo_Contrario")
                        Me.Tabla.Item(col.Index, 2).Value = Cargar_Cuentas_cero(Columna, "Abono_Contrario")
                        Me.Tabla.Item(col.Index, 3).Value = Cargar_Cuentas_cero(Columna, "Sin_Cargos")
                        Me.Tabla.Item(col.Index, 4).Value = Cargar_Cuentas_cero(Columna, "Sin_Abonos")
                        Me.Tabla.Item(col.Index, 5).Value = Me.Tabla.Item(col.Index, 0).Value + Me.Tabla.Item(col.Index, 1).Value + Me.Tabla.Item(col.Index, 2).Value + Me.Tabla.Item(col.Index, 3).Value + Me.Tabla.Item(col.Index, 4).Value
                        Me.Tabla.Item(col.Index, 6).Value = Cargar_Cuentas_Impuestos(Mes)
                        Me.Tabla.Item(col.Index, 7).Value = BuscarObligacion(Mes, "Activos")
                        Me.Tabla.Item(col.Index, 8).Value = BuscarObligacion(Mes, "Aguinaldo")
                        Me.Tabla.Item(col.Index, 9).Value = BuscarObligacion(Mes, "Amortizaciones")
                        Me.Tabla.Item(col.Index, 10).Value = BuscarObligacion(Mes, "Depreciaciones")
                        Me.Tabla.Item(col.Index, 11).Value = BuscarObligacion(Mes, "Gastos Diferidos")
                        Me.Tabla.Item(col.Index, 12).Value = BuscarObligacion(Mes, "ISR provisional Ejercicio Anterior")
                        Me.Tabla.Item(col.Index, 13).Value = BuscarObligacion(Mes, "Nominas")
                        Me.Tabla.Item(col.Index, 14).Value = BuscarObligacion(Mes, "Patrimonio")
                        Me.Tabla.Item(col.Index, 15).Value = BuscarObligacion(Mes, "Prima Vacacional")
                        Me.Tabla.Item(col.Index, 16).Value = BuscarObligacion(Mes, "Provision C y V")
                        Me.Tabla.Item(col.Index, 17).Value = BuscarObligacion(Mes, "Provision de ISN")
                        Me.Tabla.Item(col.Index, 18).Value = BuscarObligacion(Mes, "Provision IMSS")
                        Me.Tabla.Item(col.Index, 19).Value = BuscarObligacion(Mes, "Provision INFONAVIT")
                        Me.Tabla.Item(col.Index, 20).Value = BuscarObligacion(Mes, "Provision ISR Mensual")
                        Me.Tabla.Item(col.Index, 21).Value = BuscarObligacion(Mes, "Provision SAR")

                        Cruzar(col.Index, "Activos", 7, Mes)
                        Cruzar(col.Index, "Aguinaldo", 8, Mes)
                        Cruzar(col.Index, "Amortizaciones", 9, Mes)
                        Cruzar(col.Index, "Depreciaciones", 10, Mes)
                        Cruzar(col.Index, "Gastos Diferidos", 11, Mes)
                        Cruzar(col.Index, "ISR provisional Ejercicio Anterior", 12, Mes)
                        Cruzar(col.Index, "Nominas", 13, Mes)
                        Cruzar(col.Index, "Patrimonio", 14, Mes)
                        Cruzar(col.Index, "Prima Vacacional", 15, Mes)
                        Cruzar(col.Index, "Provision C y V", 16, Mes)
                        Cruzar(col.Index, "Provision de ISN", 17, Mes)
                        Cruzar(col.Index, "Provision IMSS", 18, Mes)
                        Cruzar(col.Index, "Provision INFONAVIT", 19, Mes)
                        Cruzar(col.Index, "Provision ISR Mensual", 20, Mes)
                        Cruzar(col.Index, "Provision SAR", 21, Mes)

                        Exit For
                    End If

                Next

            Next
        Else

        End If
    End Sub

    Private Sub CmdSalirF_Click(sender As Object, e As EventArgs) Handles CmdSalirF.Click
        Me.Close()
    End Sub

    Private Sub cmdLimpiarMasivo_Click(sender As Object, e As EventArgs) Handles cmdLimpiarMasivo.Click
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.Clear()
        End If
    End Sub
    Private Sub Cruzar(ByVal Columna As Integer, ByVal Obligacion As String, ByVal Posicion As Integer, ByVal Mes As String)
        Dim sql As String = "SELECT Obligaciones.Descripcion , Obligaciones_Cliente.Mes FROM Obligaciones_Cliente 
	                            INNER JOIN Obligaciones ON Obligaciones.Id_Obligacion = Obligaciones_Cliente.Id_Obligacion
	                            WHERE Obligaciones_Cliente.Id_Empresa =" & Me.lstCliente.SelectItem & " and Obligaciones_Cliente.Anio = " & Me.Anio2.Text.Trim() & " AND Obligaciones_Cliente.Mes = '" & Mes & "' AND Obligaciones.Descripcion ='" & Obligacion.Trim() & "' "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0) <> "" Then
                If Me.Tabla.Item(Columna, Posicion).Value = "SI" Then
                    Me.Tabla.Item(Columna, Posicion).Style.BackColor = Color.ForestGreen
                Else
                    Me.Tabla.Item(Columna, Posicion).Style.BackColor = Color.Red
                End If
            Else
                If Me.Tabla.Item(Columna, Posicion).Value = "SI" Then
                    Me.Tabla.Item(Columna, Posicion).Style.BackColor = Color.Red
                Else
                    Me.Tabla.Item(Columna, Posicion).Style.BackColor = Color.ForestGreen
                End If

            End If
        Else
            Try
                Me.Tabla.Item(Columna, Posicion).Style.BackColor = Color.White
            Catch ex As Exception

            End Try

        End If
    End Sub
End Class
