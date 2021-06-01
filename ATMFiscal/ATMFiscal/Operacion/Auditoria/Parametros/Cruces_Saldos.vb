Imports System.ComponentModel
Imports Telerik.WinControls
Public Class Cruces_Saldos
    Dim activo As Boolean
    Dim m = Now.Date.Month.ToString
    Dim a = Str(DateTime.Now.Year)
    Public serV As String = My.Forms.Inicio.txtServerDB.Text
    Private Sub Diseño()
        Eventos.DiseñoTablaEnca(Me.Tablaenero)
        Eventos.DiseñoTablaEnca(Me.TablaFebrero)
        Eventos.DiseñoTablaEnca(Me.TablaMarzo)
        Eventos.DiseñoTablaEnca(Me.TablaAbril)
        Eventos.DiseñoTablaEnca(Me.TablaMayo)
        Eventos.DiseñoTablaEnca(Me.TablaJunio)
        Eventos.DiseñoTablaEnca(Me.TablaJulio)
        Eventos.DiseñoTablaEnca(Me.TablaAgosto)
        Eventos.DiseñoTablaEnca(Me.TablaSeptiembre)
        Eventos.DiseñoTablaEnca(Me.TablaOctubre)
        Eventos.DiseñoTablaEnca(Me.TablaNoviembre)
        Eventos.DiseñoTablaEnca(Me.TablaDiciembre)
        Eventos.DiseñoTablaEnca(Me.TablaAnual)
    End Sub
    Private Sub Cruces_Saldos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        activo = True
        Cargar_clientes()
        Diseño()
        For i As Integer = 0 To Me.Tablaenero.Columns.Count - 1
            Tablaenero.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
            TablaFebrero.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
            TablaMarzo.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
            TablaAbril.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
            TablaMayo.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
            TablaJunio.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
            TablaJulio.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
            TablaAgosto.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
            TablaSeptiembre.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
            TablaOctubre.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
            TablaNoviembre.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
            TablaDiciembre.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
            TablaAnual.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        activo = False
    End Sub
    Private Sub Cargar_clientes()
        Me.lstClientesMasivos.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstClientesMasivos.SelectItem = My.Forms.Inicio.Clt

        Me.lstNivel2.Cargar("SELECT 1, 'Nivel1' UNION SELECT 2, 'Nivel2' UNION SELECT 3, 'Nivel3' UNION SELECT 4, 'Nivel4' ")
        Me.lstNivel2.SelectText = ""
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
    Private Sub cmdLimpiarMasivo_Click(sender As Object, e As EventArgs) Handles cmdLimpiarMasivo.Click
        Limpiar3()
        Me.lstClientesMasivos.SelectText = ""
        Me.lstNivel2.SelectText = ""
    End Sub
    Private Sub Limpiar3()
        Me.Tablaenero.Rows.Clear()
        Me.TablaFebrero.Rows.Clear()
        Me.TablaMarzo.Rows.Clear()
        Me.TablaAbril.Rows.Clear()
        Me.TablaMayo.Rows.Clear()
        Me.TablaJunio.Rows.Clear()
        Me.TablaJulio.Rows.Clear()
        Me.TablaAgosto.Rows.Clear()
        Me.TablaSeptiembre.Rows.Clear()
        Me.TablaOctubre.Rows.Clear()
        Me.TablaNoviembre.Rows.Clear()
        Me.TablaDiciembre.Rows.Clear()
        Me.TablaAnual.Rows.Clear()
    End Sub
    Private Sub CmdQuitarMasivo_Click(sender As Object, e As EventArgs) Handles CmdQuitarMasivo.Click
        If Me.Tablaenero.Rows.Count > 0 Then
            QuitacuentasceroBalanzapormes()
        End If
    End Sub
    Private Sub QuitacuentasceroBalanzapormes()
        Dim filas As Integer = Me.Tablaenero.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.Tablaenero.RowCount - 1
                If Me.Tablaenero.Item(5, i).Value = 0 And Me.Tablaenero.Item(6, i).Value = 0 And Me.Tablaenero.Item(7, i).Value = 0 Then
                    Me.Tablaenero.Rows.RemoveAt(i)
                    Exit For
                End If
            Next
        Next
        filas = Me.TablaFebrero.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaFebrero.RowCount - 1
                If Me.TablaFebrero.Item(5, i).Value = 0 And Me.TablaFebrero.Item(6, i).Value = 0 And Me.TablaFebrero.Item(7, i).Value = 0 Then
                    Me.TablaFebrero.Rows.RemoveAt(i)
                    Exit For
                End If
            Next
        Next
        filas = Me.TablaMarzo.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaMarzo.RowCount - 1
                If Me.TablaMarzo.Item(5, i).Value = 0 And Me.TablaMarzo.Item(6, i).Value = 0 And Me.TablaMarzo.Item(7, i).Value = 0 Then
                    Me.TablaMarzo.Rows.RemoveAt(i)
                    Exit For
                End If
            Next
        Next
        filas = Me.TablaAbril.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaAbril.RowCount - 1
                If Me.TablaAbril.Item(5, i).Value = 0 And Me.TablaAbril.Item(6, i).Value = 0 And Me.TablaAbril.Item(7, i).Value = 0 Then
                    Me.TablaAbril.Rows.RemoveAt(i)
                    Exit For
                End If
            Next
        Next
        filas = Me.TablaMayo.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaMayo.RowCount - 1
                If Me.TablaMayo.Item(5, i).Value = 0 And Me.TablaMayo.Item(6, i).Value = 0 And Me.TablaMayo.Item(7, i).Value = 0 Then
                    Me.TablaMayo.Rows.RemoveAt(i)
                    Exit For
                End If
            Next
        Next
        filas = Me.TablaJunio.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaJunio.RowCount - 1
                If Me.TablaJunio.Item(5, i).Value = 0 And Me.TablaJunio.Item(6, i).Value = 0 And Me.TablaJunio.Item(7, i).Value = 0 Then
                    Me.TablaJunio.Rows.RemoveAt(i)
                    Exit For
                End If
            Next
        Next

        filas = Me.TablaJulio.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaJulio.RowCount - 1
                If Me.TablaJulio.Item(5, i).Value = 0 And Me.TablaJulio.Item(6, i).Value = 0 And Me.TablaJulio.Item(7, i).Value = 0 Then
                    Me.TablaJulio.Rows.RemoveAt(i)
                    Exit For
                End If
            Next
        Next
        filas = Me.TablaAgosto.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaAgosto.RowCount - 1
                If Me.TablaAgosto.Item(5, i).Value = 0 And Me.TablaAgosto.Item(6, i).Value = 0 And Me.TablaAgosto.Item(7, i).Value = 0 Then
                    Me.TablaAgosto.Rows.RemoveAt(i)
                    Exit For
                End If
            Next
        Next
        filas = Me.TablaSeptiembre.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaSeptiembre.RowCount - 1
                If Me.TablaSeptiembre.Item(5, i).Value = 0 And Me.TablaSeptiembre.Item(6, i).Value = 0 And Me.TablaSeptiembre.Item(7, i).Value = 0 Then
                    Me.TablaSeptiembre.Rows.RemoveAt(i)
                    Exit For
                End If
            Next
        Next
        filas = Me.TablaOctubre.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaOctubre.RowCount - 1
                If Me.TablaOctubre.Item(5, i).Value = 0 And Me.TablaOctubre.Item(6, i).Value = 0 And Me.TablaOctubre.Item(7, i).Value = 0 Then
                    Me.TablaOctubre.Rows.RemoveAt(i)
                    Exit For
                End If
            Next
        Next
        filas = Me.TablaNoviembre.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaNoviembre.RowCount - 1
                If Me.TablaNoviembre.Item(5, i).Value = 0 And Me.TablaNoviembre.Item(6, i).Value = 0 And Me.TablaNoviembre.Item(7, i).Value = 0 Then
                    Me.TablaNoviembre.Rows.RemoveAt(i)
                    Exit For
                End If
            Next
        Next
        filas = Me.TablaDiciembre.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaDiciembre.RowCount - 1
                If Me.TablaDiciembre.Item(5, i).Value = 0 And Me.TablaDiciembre.Item(6, i).Value = 0 And Me.TablaDiciembre.Item(7, i).Value = 0 Then
                    Me.TablaDiciembre.Rows.RemoveAt(i)
                    Exit For
                End If
            Next
        Next
        filas = Me.TablaAnual.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaAnual.RowCount - 1
                If Me.TablaAnual.Item(5, i).Value = 0 And Me.TablaAnual.Item(6, i).Value = 0 And Me.TablaAnual.Item(7, i).Value = 0 Then
                    Me.TablaAnual.Rows.RemoveAt(i)
                    Exit For
                End If
            Next
        Next
    End Sub

    Private Sub CmdExportaanuales_Click(sender As Object, e As EventArgs) Handles CmdExportaanuales.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.Tablaenero.RowCount > 0 Then
            If Me.Tablaenero.Columns.Count > 256 Then
                RadMessageBox.Show("El rango de fechas sobrepasa las columnas de una hoja de excel, disminuye el rango...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                Exit Sub
            End If


            Dim excel As Microsoft.Office.Interop.Excel.Application = Eventos.NuevoExcel("Balanzas", False)
            'Enero
            For col As Integer = 1 To Me.Tablaenero.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.Tablaenero.Columns(col).HeaderText, 1)
            Next
            For i As Integer = 0 To Me.Tablaenero.RowCount - 1
                For j As Integer = 1 To Me.Tablaenero.Columns.Count - 1
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Me.Tablaenero.Item(j, i).Value), 1)
                Next

            Next
            'Febrero
            For col As Integer = 1 To Me.TablaFebrero.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaFebrero.Columns(col).HeaderText, 2)
            Next
            For i As Integer = 0 To Me.TablaFebrero.RowCount - 1
                For j As Integer = 1 To Me.TablaFebrero.Columns.Count - 1
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Me.TablaFebrero.Item(j, i).Value), 2)
                Next

            Next
            'Marzo
            For col As Integer = 1 To Me.TablaMarzo.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaMarzo.Columns(col).HeaderText, 3)
            Next
            For i As Integer = 0 To Me.TablaMarzo.RowCount - 1
                For j As Integer = 1 To Me.TablaMarzo.Columns.Count - 1
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Me.TablaMarzo.Item(j, i).Value), 3)
                Next

            Next

            'Abril
            For col As Integer = 1 To Me.TablaAbril.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaAbril.Columns(col).HeaderText, 4)
            Next
            For i As Integer = 0 To Me.TablaAbril.RowCount - 1
                For j As Integer = 1 To Me.TablaAbril.Columns.Count - 1
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Me.TablaAbril.Item(j, i).Value), 4)
                Next

            Next
            'Mayo
            For col As Integer = 1 To Me.TablaMayo.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaMayo.Columns(col).HeaderText, 5)
            Next
            For i As Integer = 0 To Me.TablaMayo.RowCount - 1
                For j As Integer = 1 To Me.TablaMayo.Columns.Count - 1
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Me.TablaMayo.Item(j, i).Value), 5)
                Next

            Next
            'Junio
            For col As Integer = 1 To Me.TablaJunio.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaJunio.Columns(col).HeaderText, 6)
            Next
            For i As Integer = 0 To Me.TablaJunio.RowCount - 1
                For j As Integer = 1 To Me.TablaJunio.Columns.Count - 1
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Me.TablaJunio.Item(j, i).Value), 6)
                Next

            Next
            'Julio
            For col As Integer = 1 To Me.TablaJulio.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaJulio.Columns(col).HeaderText, 7)
            Next
            For i As Integer = 0 To Me.TablaJulio.RowCount - 1
                For j As Integer = 1 To Me.TablaJulio.Columns.Count - 1
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Me.TablaJulio.Item(j, i).Value), 7)
                Next

            Next
            'Agosto
            For col As Integer = 1 To Me.TablaAgosto.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaAgosto.Columns(col).HeaderText, 8)
            Next
            For i As Integer = 0 To Me.TablaAgosto.RowCount - 1
                For j As Integer = 1 To Me.TablaAgosto.Columns.Count - 1
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Me.TablaAgosto.Item(j, i).Value), 8)
                Next

            Next
            'Septiembre
            For col As Integer = 1 To Me.TablaSeptiembre.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaSeptiembre.Columns(col).HeaderText, 9)
            Next
            For i As Integer = 0 To Me.TablaSeptiembre.RowCount - 1
                For j As Integer = 1 To Me.TablaSeptiembre.Columns.Count - 1
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Me.TablaSeptiembre.Item(j, i).Value), 9)
                Next

            Next
            'Octubre
            For col As Integer = 1 To Me.TablaOctubre.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaOctubre.Columns(col).HeaderText, 10)
            Next
            For i As Integer = 0 To Me.TablaOctubre.RowCount - 1
                For j As Integer = 1 To Me.TablaOctubre.Columns.Count - 1
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Me.TablaOctubre.Item(j, i).Value), 10)
                Next

            Next
            'Noviembre
            For col As Integer = 1 To Me.TablaNoviembre.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaNoviembre.Columns(col).HeaderText, 11)
            Next
            For i As Integer = 0 To Me.TablaNoviembre.RowCount - 1
                For j As Integer = 1 To Me.TablaNoviembre.Columns.Count - 1
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Me.TablaNoviembre.Item(j, i).Value), 11)
                Next

            Next
            'Diciembre
            For col As Integer = 1 To Me.TablaDiciembre.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaDiciembre.Columns(col).HeaderText, 12)
            Next
            For i As Integer = 0 To Me.TablaDiciembre.RowCount - 1
                For j As Integer = 1 To Me.TablaDiciembre.Columns.Count - 1
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Me.TablaDiciembre.Item(j, i).Value), 12)
                Next

            Next
            'Anual
            For col As Integer = 1 To Me.TablaAnual.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaAnual.Columns(col).HeaderText, 13)
            Next
            For i As Integer = 0 To Me.TablaAnual.RowCount - 1
                For j As Integer = 1 To Me.TablaAnual.Columns.Count - 1
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Me.TablaAnual.Item(j, i).Value), 13)
                Next

            Next
            Eventos.Mostrar_Excel(excel)
        Else
            RadMessageBox.Show("No hay datos para exportar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
        End If
    End Sub

    Private Sub CmbBuscarMasivo_Click(sender As Object, e As EventArgs) Handles CmbBuscarMasivo.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim Tablas As DataGridView() = {Tablaenero, TablaFebrero, TablaMarzo, TablaAbril, TablaMayo, TablaJunio, TablaJulio, TablaAgosto, TablaSeptiembre, TablaOctubre, TablaNoviembre, TablaDiciembre, TablaAnual}
        Dim sql As String = ""
        Dim mes As String = ""
        Dim cont As Integer = 1

        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = Tablas.Count
        Me.Cursor = Cursors.AppStarting
        For Each Ta In Tablas
            mes = cont
            If Len(mes) < 2 Then
                mes = "0" & mes
            End If

            sql = " SELECT 	 Cuenta,	Alias,	Nat ,Descripcion,	Saldo_Anterior,	Debe,	Haber,	
                        Saldo_Final,	Cuenta_Cero,	Cargo_Contrario,	Abono_Contrario,	Sin_Cargos,	Sin_Abonos FROM dbo.Balanzas WHERE  Anio= '" & Trim(Me.Anio2.Text) & "'  and 
                        Mes = '" & Trim(mes) & "'  and Id_Empresa = " & Me.lstClientesMasivos.SelectItem & ""
            Dim ds As DataSet = Eventos.Obtener_DS(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Ta.RowCount = ds.Tables(0).Rows.Count
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Ta.Item(1, i).Value = ds.Tables(0).Rows(i)("Cuenta")
                    Ta.Item(0, i).Value = ds.Tables(0).Rows(i)("Alias")
                    Ta.Item(2, i).Value = ds.Tables(0).Rows(i)("Nat")
                    Ta.Item(3, i).Value = ds.Tables(0).Rows(i)("Descripcion")
                    Ta.Item(4, i).Value = ds.Tables(0).Rows(i)("Saldo_Anterior")
                    Ta.Item(5, i).Value = ds.Tables(0).Rows(i)("Debe")
                    Ta.Item(6, i).Value = ds.Tables(0).Rows(i)("Haber")
                    Ta.Item(7, i).Value = ds.Tables(0).Rows(i)("Saldo_Final")
                    Ta.Item(8, i).Value = ""
                    Ta.Item(9, i).Value = ds.Tables(0).Rows(i)("Cuenta_Cero")
                    Ta.Item(10, i).Value = ds.Tables(0).Rows(i)("Cargo_Contrario")
                    Ta.Item(11, i).Value = ds.Tables(0).Rows(i)("Abono_Contrario")
                    Ta.Item(12, i).Value = ds.Tables(0).Rows(i)("Sin_Cargos")
                    Ta.Item(13, i).Value = ds.Tables(0).Rows(i)("Sin_Abonos")

                Next
            End If
            cont += 1

            frm.Barra.value = cont - 1
        Next
        Cruces_por_mes()
        frm.Close()
        RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub Buscar_Balanzas(ByVal Anio As String, ByVal Nivel As String)
        Dim Where As String = ""

        Select Case Nivel
            Case 1
                Where = " And  Nivel1 > 0 and Nivel2='0000' "
            Case 2
                Where = " And  Nivel1 > 0 And  Nivel2 >='0000' and Nivel3='0000' "
            Case 3
                Where = " And  Nivel1 > 0 And  Nivel2  >='0000' And  Nivel3  >='0000' and Nivel4='0000' "
            Case 4
                Where = " And  Nivel1 > 0 And  Nivel2  >='0000' And  Nivel3  >='0000' and Nivel4 >='0000' "
        End Select

        Dim sql As String = "SELECT * FROM (
                                Select cuenta,   CASE WHEN nivel4 >'0000' THEN nivel1 + '-' + nivel2 + '-' + nivel3 + '-' + nivel4 
                                WHEN nivel3 >'0000' THEN nivel1 + '-' + nivel2 + '-' + nivel3 
                                WHEN nivel2 >'0000' THEN nivel1 + '-' + nivel2 
                                WHEN  nivel2 ='0000' then nivel1 END  AS ID_cuenta,  Rtrim(Descripcion) as Descripcion ,Naturaleza,
                                CASE WHEN nivel4 >'0000' THEN 4 
                                WHEN nivel3 >'0000' THEN 3 
                                WHEN nivel2 >'0000' THEN 2 
                                WHEN  nivel2 ='0000' then 1 END  AS Nivel,RFC
                                from catalogo_de_cuentas where Id_Empresa= " & Me.lstClientesMasivos.SelectItem & "
                                " & Where & " AND catalogo_de_cuentas.Cuenta IN (SELECT Cuenta FROM Cuentas_Con_Saldo WHERE Anio = " & Me.Anio2.Text.Trim() & " AND Id_Empresa = " & Me.lstClientesMasivos.SelectItem & "  )) AS Tabla    order by cuenta"

        Dim Posicion As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To ds.Tables(0).Rows.Count
                'If ds.Tables(0).Rows(i)("Saldo") Then

                'End If
            Next

            Me.Tablaenero.RowCount = ds.Tables(0).Rows.Count
            Me.TablaFebrero.RowCount = ds.Tables(0).Rows.Count
            Me.TablaMarzo.RowCount = ds.Tables(0).Rows.Count
            Me.TablaAbril.RowCount = ds.Tables(0).Rows.Count
            Me.TablaMayo.RowCount = ds.Tables(0).Rows.Count
            Me.TablaJunio.RowCount = ds.Tables(0).Rows.Count
            Me.TablaJulio.RowCount = ds.Tables(0).Rows.Count
            Me.TablaAgosto.RowCount = ds.Tables(0).Rows.Count
            Me.TablaSeptiembre.RowCount = ds.Tables(0).Rows.Count
            Me.TablaOctubre.RowCount = ds.Tables(0).Rows.Count
            Me.TablaNoviembre.RowCount = ds.Tables(0).Rows.Count
            Me.TablaDiciembre.RowCount = ds.Tables(0).Rows.Count
            Me.TablaAnual.RowCount = ds.Tables(0).Rows.Count




            Me.Barra.Minimum = 0
            Me.Barra.Maximum = Me.Tablaenero.RowCount - 1
            Me.Cursor = Cursors.AppStarting
            Dim mes As String = ""
            Where = ""
            'Enero

            For j As Integer = 0 To Me.Tablaenero.Rows.Count - 1
                Me.Tablaenero.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.Tablaenero.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.Tablaenero.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.Tablaenero.Item(3, j).Value = ds.Tables(0).Rows(j)(2)
                'Febrero
                Me.TablaFebrero.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaFebrero.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaFebrero.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaFebrero.Item(3, j).Value = ds.Tables(0).Rows(j)(2)
                'Marzo
                Me.TablaMarzo.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaMarzo.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaMarzo.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaMarzo.Item(3, j).Value = ds.Tables(0).Rows(j)(2)
                'Abril
                Me.TablaAbril.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaAbril.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaAbril.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaAbril.Item(3, j).Value = ds.Tables(0).Rows(j)(2)
                'Mayo
                Me.TablaMayo.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaMayo.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaMayo.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaMayo.Item(3, j).Value = ds.Tables(0).Rows(j)(2)

                'Junio
                Me.TablaJunio.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaJunio.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaJunio.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaJunio.Item(3, j).Value = ds.Tables(0).Rows(j)(2)
                'julio
                Me.TablaJulio.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaJulio.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaJulio.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaJulio.Item(3, j).Value = ds.Tables(0).Rows(j)(2)

                'Agosto
                Me.TablaAgosto.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaAgosto.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaAgosto.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaAgosto.Item(3, j).Value = ds.Tables(0).Rows(j)(2)

                'Septiembre
                Me.TablaSeptiembre.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaSeptiembre.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaSeptiembre.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaSeptiembre.Item(3, j).Value = ds.Tables(0).Rows(j)(2)

                'octubre
                Me.TablaOctubre.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaOctubre.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaOctubre.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaOctubre.Item(3, j).Value = ds.Tables(0).Rows(j)(2)

                'Noviembre
                Me.TablaNoviembre.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaNoviembre.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaNoviembre.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaNoviembre.Item(3, j).Value = ds.Tables(0).Rows(j)(2)

                'Diciembre
                Me.TablaDiciembre.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaDiciembre.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaDiciembre.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaDiciembre.Item(3, j).Value = ds.Tables(0).Rows(j)(2)

                'Anual
                Me.TablaAnual.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaAnual.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaAnual.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaAnual.Item(3, j).Value = ds.Tables(0).Rows(j)(2)

                Select Case Len(Trim(Me.Tablaenero.Item(0, j).Value.ToString().Replace("-", "")))
                    Case 4
                        Where = "   Nivel1 = '" & Trim(Me.Tablaenero.Item(0, j).Value) & "' and Nivel2 >= '0000' "
                    Case 8
                        Where = "  Nivel1 = '" & Trim(Me.Tablaenero.Item(0, j).Value.ToString.Substring(0, 4)) & "' and Nivel2   = '" & Trim(Me.Tablaenero.Item(0, j).Value.Replace("-", "").ToString.Substring(4, 4)) & "' and Nivel3 >= '0000' "
                    Case 12
                        Where = "  Nivel1 = '" & Trim(Me.Tablaenero.Item(0, j).Value.ToString.Substring(0, 4)) & "' and Nivel2   = '" & Trim(Me.Tablaenero.Item(0, j).Value.Replace("-", "").ToString.Substring(4, 4)) & "' and Nivel3='" & Trim(Me.Tablaenero.Item(0, j).Value.Replace("-", "").ToString.Substring(8, 4)) & "' and Nivel4 >= '0000' "
                    Case 16
                        Where = "   Nivel1 = '" & Trim(Me.Tablaenero.Item(0, j).Value.ToString.Substring(0, 4)) & "' and Nivel2   = '" & Trim(Me.Tablaenero.Item(0, j).Value.Replace("-", "").ToString.Substring(4, 4)) & "' and Nivel3='" & Trim(Me.Tablaenero.Item(0, j).Value.Replace("-", "").ToString.Substring(8, 4)) & "' and Nivel4 ='" & Trim(Me.Tablaenero.Item(0, j).Value.Replace("-", "").ToString.Substring(12, 4)) & "'  "
                End Select
                For a As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
                    mes = a
                    If Len(mes) < 2 Then
                        mes = "0" & mes
                    End If


                    Dim Procedimiento As String = "BalanzasMeses '" & Me.lstClientesMasivos.SelectItem & "','" & Me.Anio2.Text & "','" & mes & "','" & Convert.ToInt32(Me.Anio2.Text) - 1 & "','" & Where.Replace("'", "") & "'"

                    Dim DP As DataSet = Eventos.Obtener_DS(Procedimiento)
                    If DP.Tables(0).Rows.Count > 0 Or DP.Tables(1).Rows.Count > 0 Or DP.Tables(2).Rows.Count > 0 Then
                        Dim Saldo As Decimal = 0

                        If DP.Tables(1).Rows.Count > 0 Then
                            If DP.Tables(1).Rows(0)(0) <> 0 Then
                                Saldo = DP.Tables(1).Rows(0)(0)
                            Else
                                Saldo = DP.Tables(1).Rows(0)(1)
                            End If
                        Else
                            Saldo = 0
                        End If
                        If DP.Tables(2).Rows.Count > 0 Then
                            Saldo += IIf(IsDBNull(DP.Tables(2).Rows(0)(0)) = True, 0, DP.Tables(2).Rows(0)(0))
                        Else
                            Saldo += 0
                        End If
                        If DP.Tables(0).Rows.Count > 0 Then

                            Select Case a
                                Case 1
                                    Me.Tablaenero.Item(4, j).Value = Saldo
                                    Me.Tablaenero.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                    Me.Tablaenero.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                Case 2
                                    Me.TablaFebrero.Item(4, j).Value = Saldo
                                    Me.TablaFebrero.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                    Me.TablaFebrero.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                Case 3
                                    Me.TablaMarzo.Item(4, j).Value = Saldo
                                    Me.TablaMarzo.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                    Me.TablaMarzo.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                Case 4
                                    Me.TablaAbril.Item(4, j).Value = Saldo
                                    Me.TablaAbril.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                    Me.TablaAbril.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                Case 5
                                    Me.TablaMayo.Item(4, j).Value = Saldo
                                    Me.TablaMayo.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                    Me.TablaMayo.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                Case 6
                                    Me.TablaJunio.Item(4, j).Value = Saldo
                                    Me.TablaJunio.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                    Me.TablaJunio.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                Case 7
                                    Me.TablaJulio.Item(4, j).Value = Saldo
                                    Me.TablaJulio.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                    Me.TablaJulio.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                Case 8
                                    Me.TablaAgosto.Item(4, j).Value = Saldo
                                    Me.TablaAgosto.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                    Me.TablaAgosto.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                Case 9
                                    Me.TablaSeptiembre.Item(4, j).Value = Saldo
                                    Me.TablaSeptiembre.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                    Me.TablaSeptiembre.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                Case 10
                                    Me.TablaOctubre.Item(4, j).Value = Saldo
                                    Me.TablaOctubre.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                    Me.TablaOctubre.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                Case 11
                                    Me.TablaNoviembre.Item(4, j).Value = Saldo
                                    Me.TablaNoviembre.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                    Me.TablaNoviembre.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                Case 12
                                    Me.TablaDiciembre.Item(4, j).Value = Saldo
                                    Me.TablaDiciembre.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                    Me.TablaDiciembre.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                            End Select
                        Else
                            Select Case a
                                Case 1
                                    Me.Tablaenero.Item(4, j).Value = Saldo
                                Case 2
                                    Me.TablaFebrero.Item(4, j).Value = Saldo
                                Case 3
                                    Me.TablaMarzo.Item(4, j).Value = Saldo
                                Case 4
                                    Me.TablaAbril.Item(4, j).Value = Saldo
                                Case 5
                                    Me.TablaMayo.Item(4, j).Value = Saldo
                                Case 6
                                    Me.TablaJunio.Item(4, j).Value = Saldo
                                Case 7
                                    Me.TablaJulio.Item(4, j).Value = Saldo
                                Case 8
                                    Me.TablaAgosto.Item(4, j).Value = Saldo
                                Case 9
                                    Me.TablaSeptiembre.Item(4, j).Value = Saldo
                                Case 10
                                    Me.TablaOctubre.Item(4, j).Value = Saldo
                                Case 11
                                    Me.TablaNoviembre.Item(4, j).Value = Saldo
                                Case 12
                                    Me.TablaDiciembre.Item(4, j).Value = Saldo
                            End Select
                        End If
                        Saldo = 0


                    End If



                Next
                'Buscar Anual

                Dim Procedimiento2 As String = "BalanzaAnual '" & Me.lstClientesMasivos.SelectItem & "','" & Me.Anio2.Text & "','" & Convert.ToInt32(Me.Anio2.Text) - 1 & "','" & Where.Replace("'", "") & "'"

                Dim DP2 As DataSet = Eventos.Obtener_DS(Procedimiento2)
                If DP2.Tables(0).Rows.Count > 0 Or DP2.Tables(1).Rows.Count > 0 Then
                    Dim Saldo As Decimal = 0
                    If DP2.Tables(1).Rows.Count > 0 Then
                        If ds.Tables(0).Rows(0)(0) <> 0 Then
                            Saldo = DP2.Tables(1).Rows(0)(0)
                        Else
                            Saldo = DP2.Tables(1).Rows(0)(1)
                        End If
                    Else
                        Saldo = 0
                    End If
                    Me.TablaAnual.Item(4, j).Value = Saldo
                    Me.TablaAnual.Item(5, j).Value = IIf(IsDBNull(DP2.Tables(0).Rows(0)(0)) = True, 0, DP2.Tables(0).Rows(0)(0))
                    Me.TablaAnual.Item(6, j).Value = IIf(IsDBNull(DP2.Tables(0).Rows(0)(0)) = True, 0, DP2.Tables(0).Rows(0)(1))
                    Saldo = 0
                End If


                'sal2 = 0
                Me.Barra.Value1 = j
            Next

            FinalMeses()
            Cruces_por_mes()

            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow
        End If

    End Sub
    Private Function Calcula_anteriores(ByVal Sql As String)
        Dim saldo As Decimal = 0
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0) <> 0 Then
                saldo = ds.Tables(0).Rows(0)(0)
            Else
                saldo = ds.Tables(0).Rows(0)(1)
            End If
        Else
            saldo = 0
        End If
        Return saldo
    End Function
    Private Sub FinalMeses()


        For i As Integer = 0 To Me.Tablaenero.RowCount - 1

            Me.Tablaenero.Item(7, i).Value = (Me.Tablaenero.Item(4, i).Value + Me.Tablaenero.Item(5, i).Value) - Me.Tablaenero.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaFebrero.RowCount - 1
            Me.TablaFebrero.Item(4, i).Value = Me.Tablaenero.Item(7, i).Value

            Me.TablaFebrero.Item(7, i).Value = (Me.TablaFebrero.Item(4, i).Value + Me.TablaFebrero.Item(5, i).Value) - Me.TablaFebrero.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaMarzo.RowCount - 1
            Me.TablaMarzo.Item(4, i).Value = Me.TablaFebrero.Item(7, i).Value

            Me.TablaMarzo.Item(7, i).Value = (Me.TablaMarzo.Item(4, i).Value + Me.TablaMarzo.Item(5, i).Value) - Me.TablaMarzo.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaAbril.RowCount - 1
            Me.TablaAbril.Item(4, i).Value = Me.TablaMarzo.Item(7, i).Value
            Me.TablaAbril.Item(7, i).Value = (Me.TablaAbril.Item(4, i).Value + Me.TablaAbril.Item(5, i).Value) - Me.TablaAbril.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaMayo.RowCount - 1
            Me.TablaMayo.Item(4, i).Value = Me.TablaAbril.Item(7, i).Value

            Me.TablaMayo.Item(7, i).Value = (Me.TablaMayo.Item(4, i).Value + Me.TablaMayo.Item(5, i).Value) - Me.TablaMayo.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaJunio.RowCount - 1
            Me.TablaJunio.Item(4, i).Value = Me.TablaMayo.Item(7, i).Value

            Me.TablaJunio.Item(7, i).Value = (Me.TablaJunio.Item(4, i).Value + Me.TablaJunio.Item(5, i).Value) - Me.TablaJunio.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaJulio.RowCount - 1
            Me.TablaJulio.Item(4, i).Value = Me.TablaJunio.Item(7, i).Value

            Me.TablaJulio.Item(7, i).Value = (Me.TablaJulio.Item(4, i).Value + Me.TablaJulio.Item(5, i).Value) - Me.TablaJulio.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaAgosto.RowCount - 1
            Me.TablaAgosto.Item(4, i).Value = Me.TablaJulio.Item(7, i).Value

            Me.TablaAgosto.Item(7, i).Value = (Me.TablaAgosto.Item(4, i).Value + Me.TablaAgosto.Item(5, i).Value) - Me.TablaAgosto.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaSeptiembre.RowCount - 1
            Me.TablaSeptiembre.Item(4, i).Value = Me.TablaAgosto.Item(7, i).Value
            Me.TablaSeptiembre.Item(7, i).Value = (Me.TablaSeptiembre.Item(4, i).Value + Me.TablaSeptiembre.Item(5, i).Value) - Me.TablaSeptiembre.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaOctubre.RowCount - 1
            Me.TablaOctubre.Item(4, i).Value = Me.TablaSeptiembre.Item(7, i).Value
            Me.TablaOctubre.Item(7, i).Value = (Me.TablaOctubre.Item(4, i).Value + Me.TablaOctubre.Item(5, i).Value) - Me.TablaOctubre.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaNoviembre.RowCount - 1
            Me.TablaNoviembre.Item(4, i).Value = Me.TablaOctubre.Item(7, i).Value
            Me.TablaNoviembre.Item(7, i).Value = (Me.TablaNoviembre.Item(4, i).Value + Me.TablaNoviembre.Item(5, i).Value) - Me.TablaNoviembre.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaDiciembre.RowCount - 1
            Me.TablaDiciembre.Item(4, i).Value = Me.TablaNoviembre.Item(7, i).Value
            Me.TablaDiciembre.Item(7, i).Value = (Me.TablaDiciembre.Item(4, i).Value + Me.TablaDiciembre.Item(5, i).Value) - Me.TablaDiciembre.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaAnual.RowCount - 1
            Me.TablaAnual.Item(7, i).Value = (Me.TablaAnual.Item(4, i).Value + Me.TablaAnual.Item(5, i).Value) - Me.TablaAnual.Item(6, i).Value
        Next
    End Sub
    Private Sub Inserta_finales()
        Me.Tablaenero.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(Tablaenero), Total_debeB(Tablaenero), Total_haberB(Tablaenero), Total_saldo_final(Tablaenero))
        Me.TablaFebrero.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaFebrero), Total_debeB(TablaFebrero), Total_haberB(TablaFebrero), Total_saldo_final(TablaFebrero))
        Me.TablaMarzo.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaMarzo), Total_debeB(TablaMarzo), Total_haberB(TablaMarzo), Total_saldo_final(TablaMarzo))
        Me.TablaAbril.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaAbril), Total_debeB(TablaAbril), Total_haberB(TablaAbril), Total_saldo_final(TablaAbril))
        Me.TablaMayo.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaMayo), Total_debeB(TablaMayo), Total_haberB(TablaMayo), Total_saldo_final(TablaMayo))
        Me.TablaJunio.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaJunio), Total_debeB(TablaJunio), Total_haberB(TablaJunio), Total_saldo_final(TablaJunio))
        Me.TablaJulio.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaJulio), Total_debeB(TablaJulio), Total_haberB(TablaJulio), Total_saldo_final(TablaJulio))
        Me.TablaAgosto.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaAgosto), Total_debeB(TablaAgosto), Total_haberB(TablaAgosto), Total_saldo_final(TablaAgosto))
        Me.TablaSeptiembre.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaSeptiembre), Total_debeB(TablaSeptiembre), Total_haberB(TablaSeptiembre), Total_saldo_final(TablaSeptiembre))
        Me.TablaOctubre.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaOctubre), Total_debeB(TablaOctubre), Total_haberB(TablaOctubre), Total_saldo_final(TablaOctubre))
        Me.TablaNoviembre.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaNoviembre), Total_debeB(TablaNoviembre), Total_haberB(TablaNoviembre), Total_saldo_final(TablaNoviembre))
        Me.TablaDiciembre.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaDiciembre), Total_debeB(TablaDiciembre), Total_haberB(TablaDiciembre), Total_saldo_final(TablaDiciembre))
        Me.TablaAnual.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaAnual), Total_debeB(TablaAnual), Total_haberB(TablaAnual), Total_saldo_final(TablaAnual))

    End Sub
    Private Function TotalSaldoanterior(ByVal TA As DataGridView)

        Dim total As Decimal = 0
        Dim A As Decimal = 0
        Dim B As Decimal = 0
        For i As Integer = 0 To TA.RowCount - 1
            Try
                If Len(Trim(TA.Item(0, i).Value.ToString)) = 4 Then
                    If Trim(TA.Item(4, i).Value.ToString) = "A" Then
                        A = A + TA.Item(4, i).Value
                    ElseIf Trim(TA.Item(4, i).Value.ToString) = "D" Then

                        B = B + TA.Item(4, i).Value
                    End If
                End If
            Catch ex As Exception

            End Try

        Next
        total = A - B
        Return total

    End Function
    Private Sub Cru(ByVal TA As DataGridView)
        Dim sql As String = ""

        For i As Integer = 0 To TA.RowCount - 1
            If Len(Trim(TA.Item(0, i).Value.ToString)) = 4 Then




                If Trim(TA.Item(4, i).Value.ToString) = "A" Then

                ElseIf Trim(TA.Item(4, i).Value.ToString) = "D" Then


                End If

            End If
        Next

    End Sub
    Private Function Total_haberB(ByVal TA As DataGridView)

        Dim total As Decimal = 0
        For i As Integer = 0 To TA.RowCount - 1
            If Len(Trim(TA.Item(0, i).Value.ToString)) = 4 Then
                total = total + TA.Item(6, i).Value

            End If
        Next
        Return total
    End Function
    Private Function Total_debeB(ByVal TA As DataGridView)
        Dim total As Decimal = 0
        For i As Integer = 0 To TA.RowCount - 1
            If Len(Trim(TA.Item(0, i).Value.ToString)) = 4 Then
                total = total + TA.Item(5, i).Value

            End If
        Next
        Return total
    End Function
    Private Function Total_saldo_final(ByVal TA As DataGridView)

        Dim total As Decimal = 0
        For i As Integer = 0 To TA.RowCount - 1
            If Len(Trim(TA.Item(0, i).Value.ToString)) = 4 Then

                total = total + TA.Item(7, i).Value

            End If
        Next
        Return total
    End Function

    Private Sub Cruces_por_mes()

        For i As Integer = 0 To Me.Tablaenero.RowCount - 1

            Buscar_Cuentas_Cero(Trim(Me.Tablaenero.Item(1, i).Value), Tablaenero, i)
            Buscar_Cuentas_Abono_Cero(Trim(Me.Tablaenero.Item(1, i).Value), Tablaenero, i)
            Buscar_Cuentas_Cargo_Cero(Trim(Me.Tablaenero.Item(1, i).Value), Tablaenero, i)

            If Trim(Me.Tablaenero.Item(2, i).Value) = "D" Then
                If Me.Tablaenero.Item(7, i).Value < 0 Then
                    Me.Tablaenero.Item(10, i).Value = "Saldo contario debe ser Positivo"
                End If
            ElseIf Trim(Me.Tablaenero.Item(2, i).Value) = "A" Then
                If Me.Tablaenero.Item(7, i).Value > 0 Then
                    Me.Tablaenero.Item(10, i).Value = "Saldo contario debe ser Negativo"
                End If
            End If
        Next
        For i As Integer = 0 To Me.TablaFebrero.RowCount - 1
            Buscar_Cuentas_Cero(Trim(Me.TablaFebrero.Item(1, i).Value), TablaFebrero, i)
            Buscar_Cuentas_Abono_Cero(Trim(Me.TablaFebrero.Item(1, i).Value), TablaFebrero, i)
            Buscar_Cuentas_Cargo_Cero(Trim(Me.TablaFebrero.Item(1, i).Value), TablaFebrero, i)

            If Trim(Me.TablaFebrero.Item(2, i).Value) = "D" Then
                If Me.TablaFebrero.Item(7, i).Value < 0 Then
                    Me.TablaFebrero.Item(10, i).Value = "Saldo contario debe ser Positivo"
                End If
            ElseIf Trim(Me.TablaFebrero.Item(2, i).Value) = "A" Then
                If Me.TablaFebrero.Item(7, i).Value > 0 Then
                    Me.TablaFebrero.Item(10, i).Value = "Saldo contario debe ser Negativo"
                End If
            End If
        Next
        For i As Integer = 0 To Me.TablaMarzo.RowCount - 1
            Buscar_Cuentas_Cero(Trim(Me.TablaMarzo.Item(1, i).Value), TablaMarzo, i)
            Buscar_Cuentas_Abono_Cero(Trim(Me.TablaMarzo.Item(1, i).Value), TablaMarzo, i)
            Buscar_Cuentas_Cargo_Cero(Trim(Me.TablaMarzo.Item(1, i).Value), TablaMarzo, i)

            If Trim(Me.TablaMarzo.Item(2, i).Value) = "D" Then
                If Me.TablaMarzo.Item(7, i).Value < 0 Then
                    Me.TablaMarzo.Item(10, i).Value = "Saldo contario debe ser Positivo"
                End If
            ElseIf Trim(Me.TablaMarzo.Item(2, i).Value) = "A" Then
                If Me.TablaMarzo.Item(7, i).Value > 0 Then
                    Me.TablaMarzo.Item(10, i).Value = "Saldo contario debe ser Negativo"
                End If
            End If
        Next
        For i As Integer = 0 To Me.TablaAbril.RowCount - 1
            Buscar_Cuentas_Cero(Trim(Me.TablaAbril.Item(1, i).Value), TablaAbril, i)
            Buscar_Cuentas_Abono_Cero(Trim(Me.TablaAbril.Item(1, i).Value), TablaAbril, i)
            Buscar_Cuentas_Cargo_Cero(Trim(Me.TablaAbril.Item(1, i).Value), TablaAbril, i)

            If Trim(Me.TablaAbril.Item(2, i).Value) = "D" Then
                If Me.TablaAbril.Item(7, i).Value < 0 Then
                    Me.TablaAbril.Item(10, i).Value = "Saldo contario debe ser Positivo"
                End If
            ElseIf Trim(Me.TablaAbril.Item(2, i).Value) = "A" Then
                If Me.TablaAbril.Item(7, i).Value > 0 Then
                    Me.TablaAbril.Item(10, i).Value = "Saldo contario debe ser Negativo"
                End If
            End If
        Next
        For i As Integer = 0 To Me.TablaMayo.RowCount - 1

            Buscar_Cuentas_Cero(Trim(Me.TablaMayo.Item(1, i).Value), TablaMayo, i)
            Buscar_Cuentas_Abono_Cero(Trim(Me.TablaMayo.Item(1, i).Value), TablaMayo, i)
            Buscar_Cuentas_Cargo_Cero(Trim(Me.TablaMayo.Item(1, i).Value), TablaMayo, i)

            If Trim(Me.TablaMayo.Item(2, i).Value) = "D" Then
                If Me.TablaMayo.Item(7, i).Value < 0 Then
                    Me.TablaMayo.Item(10, i).Value = "Saldo contario debe ser Positivo"
                End If
            ElseIf Trim(Me.TablaMayo.Item(2, i).Value) = "A" Then
                If Me.TablaMayo.Item(7, i).Value > 0 Then
                    Me.TablaMayo.Item(10, i).Value = "Saldo contario debe ser Negativo"
                End If
            End If

        Next
        For i As Integer = 0 To Me.TablaJunio.RowCount - 1
            Buscar_Cuentas_Cero(Trim(Me.TablaJunio.Item(1, i).Value), TablaJunio, i)
            Buscar_Cuentas_Abono_Cero(Trim(Me.TablaJunio.Item(1, i).Value), TablaJunio, i)
            Buscar_Cuentas_Cargo_Cero(Trim(Me.TablaJunio.Item(1, i).Value), TablaJunio, i)
            If Trim(Me.TablaJunio.Item(2, i).Value) = "D" Then
                If Me.TablaJunio.Item(7, i).Value < 0 Then
                    Me.TablaJunio.Item(10, i).Value = "Saldo contario debe ser Positivo"
                End If
            ElseIf Trim(Me.TablaJunio.Item(2, i).Value) = "A" Then
                If Me.TablaJunio.Item(7, i).Value > 0 Then
                    Me.TablaJunio.Item(10, i).Value = "Saldo contario debe ser Negativo"
                End If
            End If
        Next
        For i As Integer = 0 To Me.TablaJulio.RowCount - 1
            Buscar_Cuentas_Cero(Trim(Me.TablaJulio.Item(1, i).Value), TablaJulio, i)
            Buscar_Cuentas_Abono_Cero(Trim(Me.TablaJulio.Item(1, i).Value), TablaJulio, i)
            Buscar_Cuentas_Cargo_Cero(Trim(Me.TablaJulio.Item(1, i).Value), TablaJulio, i)

            If Trim(Me.TablaJulio.Item(2, i).Value) = "D" Then
                If Me.TablaJulio.Item(7, i).Value < 0 Then
                    Me.TablaJulio.Item(10, i).Value = "Saldo contario debe ser Positivo"
                End If
            ElseIf Trim(Me.TablaJulio.Item(2, i).Value) = "A" Then
                If Me.TablaJulio.Item(7, i).Value > 0 Then
                    Me.TablaJulio.Item(10, i).Value = "Saldo contario debe ser Negativo"
                End If
            End If
        Next
        For i As Integer = 0 To Me.TablaAgosto.RowCount - 1
            Buscar_Cuentas_Cero(Trim(Me.TablaAgosto.Item(1, i).Value), TablaAgosto, i)
            Buscar_Cuentas_Abono_Cero(Trim(Me.TablaAgosto.Item(1, i).Value), TablaAgosto, i)
            Buscar_Cuentas_Cargo_Cero(Trim(Me.TablaAgosto.Item(1, i).Value), TablaAgosto, i)

            If Trim(Me.TablaAgosto.Item(2, i).Value) = "D" Then
                If Me.TablaAgosto.Item(7, i).Value < 0 Then
                    Me.TablaAgosto.Item(10, i).Value = "Saldo contario debe ser Positivo"
                End If
            ElseIf Trim(Me.TablaAgosto.Item(2, i).Value) = "A" Then
                If Me.TablaAgosto.Item(7, i).Value > 0 Then
                    Me.TablaAgosto.Item(10, i).Value = "Saldo contario debe ser Negativo"
                End If
            End If
        Next
        For i As Integer = 0 To Me.TablaSeptiembre.RowCount - 1
            Buscar_Cuentas_Cero(Trim(Me.TablaSeptiembre.Item(1, i).Value), TablaSeptiembre, i)
            Buscar_Cuentas_Abono_Cero(Trim(Me.TablaSeptiembre.Item(1, i).Value), TablaSeptiembre, i)
            Buscar_Cuentas_Cargo_Cero(Trim(Me.TablaSeptiembre.Item(1, i).Value), TablaSeptiembre, i)
            If Trim(Me.TablaSeptiembre.Item(2, i).Value) = "D" Then
                If Me.TablaSeptiembre.Item(7, i).Value < 0 Then
                    Me.TablaSeptiembre.Item(10, i).Value = "Saldo contario debe ser Positivo"
                End If
            ElseIf Trim(Me.TablaSeptiembre.Item(2, i).Value) = "A" Then
                If Me.TablaSeptiembre.Item(7, i).Value > 0 Then
                    Me.TablaSeptiembre.Item(10, i).Value = "Saldo contario debe ser Negativo"
                End If
            End If
        Next
        For i As Integer = 0 To Me.TablaOctubre.RowCount - 1
            Buscar_Cuentas_Cero(Trim(Me.TablaOctubre.Item(1, i).Value), TablaOctubre, i)
            Buscar_Cuentas_Abono_Cero(Trim(Me.TablaOctubre.Item(1, i).Value), TablaOctubre, i)
            Buscar_Cuentas_Cargo_Cero(Trim(Me.TablaOctubre.Item(1, i).Value), TablaOctubre, i)

            If Trim(Me.TablaOctubre.Item(2, i).Value) = "D" Then
                If Me.TablaOctubre.Item(7, i).Value < 0 Then
                    Me.TablaOctubre.Item(10, i).Value = "Saldo contario debe ser Positivo"
                End If
            ElseIf Trim(Me.TablaOctubre.Item(2, i).Value) = "A" Then
                If Me.TablaOctubre.Item(7, i).Value > 0 Then
                    Me.TablaOctubre.Item(10, i).Value = "Saldo contario debe ser Negativo"
                End If
            End If
        Next
        For i As Integer = 0 To Me.TablaNoviembre.RowCount - 1
            Buscar_Cuentas_Cero(Trim(Me.TablaNoviembre.Item(1, i).Value), TablaNoviembre, i)
            Buscar_Cuentas_Abono_Cero(Trim(Me.TablaNoviembre.Item(1, i).Value), TablaNoviembre, i)
            Buscar_Cuentas_Cargo_Cero(Trim(Me.TablaNoviembre.Item(1, i).Value), TablaNoviembre, i)

            If Trim(Me.TablaNoviembre.Item(2, i).Value) = "D" Then
                If Me.TablaNoviembre.Item(7, i).Value < 0 Then
                    Me.TablaNoviembre.Item(10, i).Value = "Saldo contario debe ser Positivo"
                End If
            ElseIf Trim(Me.TablaNoviembre.Item(2, i).Value) = "A" Then
                If Me.TablaNoviembre.Item(7, i).Value > 0 Then
                    Me.TablaNoviembre.Item(10, i).Value = "Saldo contario debe ser Negativo"
                End If
            End If
        Next
        For i As Integer = 0 To Me.TablaDiciembre.RowCount - 1
            Buscar_Cuentas_Cero(Trim(Me.TablaDiciembre.Item(1, i).Value), TablaDiciembre, i)
            Buscar_Cuentas_Abono_Cero(Trim(Me.TablaDiciembre.Item(1, i).Value), TablaDiciembre, i)
            Buscar_Cuentas_Cargo_Cero(Trim(Me.TablaDiciembre.Item(1, i).Value), TablaDiciembre, i)

            If Trim(Me.TablaDiciembre.Item(2, i).Value) = "D" Then
                If Me.TablaDiciembre.Item(7, i).Value < 0 Then
                    Me.TablaDiciembre.Item(10, i).Value = "Saldo contario debe ser Positivo"
                End If
            ElseIf Trim(Me.TablaDiciembre.Item(2, i).Value) = "A" Then
                If Me.TablaDiciembre.Item(7, i).Value > 0 Then
                    Me.TablaDiciembre.Item(10, i).Value = "Saldo contario debe ser Negativo"
                End If
            End If
        Next
        For i As Integer = 0 To Me.TablaAnual.RowCount - 1
            Buscar_Cuentas_Cero(Trim(Me.TablaAnual.Item(1, i).Value), TablaAnual, i)
            Buscar_Cuentas_Abono_Cero(Trim(Me.TablaAnual.Item(1, i).Value), TablaAnual, i)
            Buscar_Cuentas_Cargo_Cero(Trim(Me.TablaAnual.Item(1, i).Value), TablaAnual, i)
            If Trim(Me.TablaAnual.Item(2, i).Value) = "D" Then
                If Me.TablaAnual.Item(7, i).Value < 0 Then
                    Me.TablaAnual.Item(10, i).Value = "Saldo contario debe ser Positivo"
                End If
            ElseIf Trim(Me.TablaAnual.Item(2, i).Value) = "A" Then
                If Me.TablaAnual.Item(7, i).Value > 0 Then
                    Me.TablaAnual.Item(10, i).Value = "Saldo contario debe ser Negativo"
                End If
            End If
        Next
    End Sub
    Private Sub Buscar_Cuentas_Cero(ByVal cuenta As String, ByVal TA As DataGridView, ByVal posicion As Integer)
        Dim ds As DataSet = Eventos.Obtener_DS(" SELECT    Cuenta  FROM dbo.Cuentas_Cero ")
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Try


                If TA.Item(7, posicion).Value <> 0 Then
                    If TA.Item(1, posicion).Value.ToString().Substring(0, 4) = ds.Tables(0).Rows(i)("Cuenta").ToString().Substring(0, 4) Then
                        TA.Item(9, posicion).Value = "El saldo de esta cuenta debe ser Cero "
                        TA.Item(9, posicion).Style.BackColor = Color.Red
                    End If
                End If
            Catch ex As Exception

            End Try
        Next
    End Sub
    Private Sub Buscar_Cuentas_Cargo_Cero(ByVal cuenta As String, ByVal TA As DataGridView, ByVal posicion As Integer)
        Dim ds As DataSet = Eventos.Obtener_DS(" SELECT    Cuenta  FROM dbo.Cargos_Cero ")
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Try


                If TA.Item(5, posicion).Value <> 0 Then
                    If TA.Item(1, posicion).Value.ToString().Substring(0, 4) = ds.Tables(0).Rows(i)("Cuenta").ToString().Substring(0, 4) Then
                        TA.Item(12, posicion).Value = "El saldo de esta cuenta debe ser Cero "
                        TA.Item(12, posicion).Style.BackColor = Color.Red
                    End If
                End If
            Catch ex As Exception

            End Try
        Next
    End Sub
    Private Sub Buscar_Cuentas_Abono_Cero(ByVal cuenta As String, ByVal TA As DataGridView, ByVal posicion As Integer)
        Dim ds As DataSet = Eventos.Obtener_DS(" SELECT    Cuenta  FROM dbo.Abonos_Cero ")
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

            If TA.Item(6, posicion).Value <> 0 Then
                Try


                    If TA.Item(1, posicion).Value.ToString().Substring(0, 4) = ds.Tables(0).Rows(i)("Cuenta").ToString().Substring(0, 4) Then
                        TA.Item(13, posicion).Value = "El saldo de esta cuenta debe ser Cero "
                        TA.Item(13, posicion).Style.BackColor = Color.Red
                    End If
                Catch ex As Exception

                End Try
            End If

        Next
    End Sub

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.lstClientesMasivos.SelectText <> "" Then
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Val(Convert.ToInt32(Me.ComboMes2.Text))
            Me.Cursor = Cursors.AppStarting
            Dim Sql As String = ""
            For a As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
                Dim mes As String = a
                If Len(mes) < 2 Then
                    mes = "0" & mes
                End If



                Sql = " Delete From Balanzas where Id_Empresa = " & Me.lstClientesMasivos.SelectItem & " and Anio  ='" & Trim(Me.Anio2.Text) & "'  and Mes = '" & Trim(mes) & "'"
                If Eventos.Comando_sql(Sql) >= 0 Then
                    Select Case mes

                        Case "01"

                            For i As Integer = 0 To Me.Tablaenero.RowCount - 1

                                Guarda_Balanza(Me.Anio2.Text, mes, Me.Tablaenero.Item(1, i).Value, Me.Tablaenero.Item(0, i).Value, Me.Tablaenero.Item(2, i).Value, Me.Tablaenero.Item(3, i).Value,
                                               IIf(IsDBNull(Me.Tablaenero.Item(4, i).Value) = True, 0, Me.Tablaenero.Item(4, i).Value), IIf(IsDBNull(Me.Tablaenero.Item(5, i).Value) = True, 0, Me.Tablaenero.Item(5, i).Value),
                                       IIf(IsDBNull(Me.Tablaenero.Item(6, i).Value) = True, 0, Me.Tablaenero.Item(6, i).Value), IIf(IsDBNull(Me.Tablaenero.Item(7, i).Value) = True, 0, Me.Tablaenero.Item(7, i).Value),
                                        Me.Tablaenero.Item(9, i).Value, Me.Tablaenero.Item(10, i).Value, Me.Tablaenero.Item(11, i).Value,
                                       Me.Tablaenero.Item(12, i).Value, Me.Tablaenero.Item(13, i).Value)

                            Next

                        Case "02"
                            For i As Integer = 0 To Me.TablaFebrero.RowCount - 1
                                Guarda_Balanza(Me.Anio2.Text, mes, Me.TablaFebrero.Item(1, i).Value, Me.TablaFebrero.Item(0, i).Value, Me.TablaFebrero.Item(2, i).Value, Me.TablaFebrero.Item(3, i).Value,
                                              IIf(IsDBNull(Me.TablaFebrero.Item(4, i).Value) = True, 0, Me.TablaFebrero.Item(4, i).Value), IIf(IsDBNull(Me.TablaFebrero.Item(5, i).Value) = True, 0, Me.TablaFebrero.Item(5, i).Value),
                                      IIf(IsDBNull(Me.TablaFebrero.Item(6, i).Value) = True, 0, Me.TablaFebrero.Item(6, i).Value), IIf(IsDBNull(Me.TablaFebrero.Item(7, i).Value) = True, 0, Me.TablaFebrero.Item(7, i).Value),
                                       Me.TablaFebrero.Item(9, i).Value, Me.TablaFebrero.Item(10, i).Value, Me.TablaFebrero.Item(11, i).Value,
                                      Me.TablaFebrero.Item(12, i).Value, Me.TablaFebrero.Item(13, i).Value)
                            Next
                        Case "03"
                            For i As Integer = 0 To Me.TablaMarzo.RowCount - 1
                                Guarda_Balanza(Me.Anio2.Text, mes, Me.TablaMarzo.Item(1, i).Value, Me.TablaMarzo.Item(0, i).Value, Me.TablaMarzo.Item(2, i).Value, Me.TablaMarzo.Item(3, i).Value,
                                              IIf(IsDBNull(Me.TablaMarzo.Item(4, i).Value) = True, 0, Me.TablaMarzo.Item(4, i).Value), IIf(IsDBNull(Me.TablaMarzo.Item(5, i).Value) = True, 0, Me.TablaMarzo.Item(5, i).Value),
                                      IIf(IsDBNull(Me.TablaMarzo.Item(6, i).Value) = True, 0, Me.TablaMarzo.Item(6, i).Value), IIf(IsDBNull(Me.TablaMarzo.Item(7, i).Value) = True, 0, Me.TablaMarzo.Item(7, i).Value),
                                       Me.TablaMarzo.Item(9, i).Value, Me.TablaMarzo.Item(10, i).Value, Me.TablaMarzo.Item(11, i).Value,
                                      Me.TablaMarzo.Item(12, i).Value, Me.TablaMarzo.Item(13, i).Value)
                            Next
                        Case "04"
                            For i As Integer = 0 To Me.TablaAbril.RowCount - 1
                                Guarda_Balanza(Me.Anio2.Text, mes, Me.TablaAbril.Item(1, i).Value, Me.TablaAbril.Item(0, i).Value, Me.TablaAbril.Item(2, i).Value, Me.TablaAbril.Item(3, i).Value,
                                             IIf(IsDBNull(Me.TablaAbril.Item(4, i).Value) = True, 0, Me.TablaAbril.Item(4, i).Value), IIf(IsDBNull(Me.TablaAbril.Item(5, i).Value) = True, 0, Me.TablaAbril.Item(5, i).Value),
                                     IIf(IsDBNull(Me.TablaAbril.Item(6, i).Value) = True, 0, Me.TablaAbril.Item(6, i).Value), IIf(IsDBNull(Me.TablaAbril.Item(7, i).Value) = True, 0, Me.TablaAbril.Item(7, i).Value),
                                      Me.TablaAbril.Item(9, i).Value, Me.TablaAbril.Item(10, i).Value, Me.TablaAbril.Item(11, i).Value,
                                     Me.TablaAbril.Item(12, i).Value, Me.TablaAbril.Item(13, i).Value)
                            Next
                        Case "05"
                            For i As Integer = 0 To Me.TablaMayo.RowCount - 1

                                Guarda_Balanza(Me.Anio2.Text, mes, Me.TablaMayo.Item(1, i).Value, Me.TablaMayo.Item(0, i).Value, Me.TablaMayo.Item(2, i).Value, Me.TablaMayo.Item(3, i).Value,
                                            IIf(IsDBNull(Me.TablaMayo.Item(4, i).Value) = True, 0, Me.TablaMayo.Item(4, i).Value), IIf(IsDBNull(Me.TablaMayo.Item(5, i).Value) = True, 0, Me.TablaMayo.Item(5, i).Value),
                                    IIf(IsDBNull(Me.TablaMayo.Item(6, i).Value) = True, 0, Me.TablaMayo.Item(6, i).Value), IIf(IsDBNull(Me.TablaMayo.Item(7, i).Value) = True, 0, Me.TablaMayo.Item(7, i).Value),
                                     Me.TablaMayo.Item(9, i).Value, Me.TablaMayo.Item(10, i).Value, Me.TablaMayo.Item(11, i).Value,
                                    Me.TablaMayo.Item(12, i).Value, Me.TablaMayo.Item(13, i).Value)

                            Next

                        Case "06"
                            For i As Integer = 0 To Me.TablaJunio.RowCount - 1
                                Guarda_Balanza(Me.Anio2.Text, mes, Me.TablaJunio.Item(1, i).Value, Me.TablaJunio.Item(0, i).Value, Me.TablaJunio.Item(2, i).Value, Me.TablaJunio.Item(3, i).Value,
                                            IIf(IsDBNull(Me.TablaJunio.Item(4, i).Value) = True, 0, Me.TablaJunio.Item(4, i).Value), IIf(IsDBNull(Me.TablaJunio.Item(5, i).Value) = True, 0, Me.TablaJunio.Item(5, i).Value),
                                    IIf(IsDBNull(Me.TablaJunio.Item(6, i).Value) = True, 0, Me.TablaJunio.Item(6, i).Value), IIf(IsDBNull(Me.TablaJunio.Item(7, i).Value) = True, 0, Me.TablaJunio.Item(7, i).Value),
                                     Me.TablaJunio.Item(9, i).Value, Me.TablaJunio.Item(10, i).Value, Me.TablaJunio.Item(11, i).Value,
                                    Me.TablaJunio.Item(12, i).Value, Me.TablaJunio.Item(13, i).Value)
                            Next
                        Case "07"
                            For i As Integer = 0 To Me.TablaJulio.RowCount - 1
                                Guarda_Balanza(Me.Anio2.Text, mes, Me.TablaJulio.Item(1, i).Value, Me.TablaJulio.Item(0, i).Value, Me.TablaJulio.Item(2, i).Value, Me.TablaJulio.Item(3, i).Value,
                                            IIf(IsDBNull(Me.TablaJulio.Item(4, i).Value) = True, 0, Me.TablaJulio.Item(4, i).Value), IIf(IsDBNull(Me.TablaJulio.Item(5, i).Value) = True, 0, Me.TablaJulio.Item(5, i).Value),
                                    IIf(IsDBNull(Me.TablaJulio.Item(6, i).Value) = True, 0, Me.TablaJulio.Item(6, i).Value), IIf(IsDBNull(Me.TablaJulio.Item(7, i).Value) = True, 0, Me.TablaJulio.Item(7, i).Value),
                                     Me.TablaJulio.Item(9, i).Value, Me.TablaJulio.Item(10, i).Value, Me.TablaJulio.Item(11, i).Value,
                                    Me.TablaJulio.Item(12, i).Value, Me.TablaJulio.Item(13, i).Value)
                            Next
                        Case "08"
                            For i As Integer = 0 To Me.TablaAgosto.RowCount - 1
                                Guarda_Balanza(Me.Anio2.Text, mes, Me.TablaAgosto.Item(1, i).Value, Me.TablaAgosto.Item(0, i).Value, Me.TablaAgosto.Item(2, i).Value, Me.TablaAgosto.Item(3, i).Value,
                                           IIf(IsDBNull(Me.TablaAgosto.Item(4, i).Value) = True, 0, Me.TablaAgosto.Item(4, i).Value), IIf(IsDBNull(Me.TablaAgosto.Item(5, i).Value) = True, 0, Me.TablaAgosto.Item(5, i).Value),
                                   IIf(IsDBNull(Me.TablaAgosto.Item(6, i).Value) = True, 0, Me.TablaAgosto.Item(6, i).Value), IIf(IsDBNull(Me.TablaAgosto.Item(7, i).Value) = True, 0, Me.TablaAgosto.Item(7, i).Value),
                                    Me.TablaAgosto.Item(9, i).Value, Me.TablaAgosto.Item(10, i).Value, Me.TablaAgosto.Item(11, i).Value,
                                   Me.TablaAgosto.Item(12, i).Value, Me.TablaAgosto.Item(13, i).Value)
                            Next


                        Case "09"
                            For i As Integer = 0 To Me.TablaSeptiembre.RowCount - 1
                                Guarda_Balanza(Me.Anio2.Text, mes, Me.TablaSeptiembre.Item(1, i).Value, Me.TablaSeptiembre.Item(0, i).Value, Me.TablaSeptiembre.Item(2, i).Value, Me.TablaSeptiembre.Item(3, i).Value,
                                                                       IIf(IsDBNull(Me.TablaSeptiembre.Item(4, i).Value) = True, 0, Me.TablaSeptiembre.Item(4, i).Value), IIf(IsDBNull(Me.TablaSeptiembre.Item(5, i).Value) = True, 0, Me.TablaSeptiembre.Item(5, i).Value),
                                                               IIf(IsDBNull(Me.TablaSeptiembre.Item(6, i).Value) = True, 0, Me.TablaSeptiembre.Item(6, i).Value), IIf(IsDBNull(Me.TablaSeptiembre.Item(7, i).Value) = True, 0, Me.TablaSeptiembre.Item(7, i).Value),
                                                                Me.TablaSeptiembre.Item(9, i).Value, Me.TablaSeptiembre.Item(10, i).Value, Me.TablaSeptiembre.Item(11, i).Value,
                                                               Me.TablaSeptiembre.Item(12, i).Value, Me.TablaSeptiembre.Item(13, i).Value)
                            Next


                        Case "10"
                            For i As Integer = 0 To Me.TablaOctubre.RowCount - 1
                                Guarda_Balanza(Me.Anio2.Text, mes, Me.TablaOctubre.Item(1, i).Value, Me.TablaOctubre.Item(0, i).Value, Me.TablaOctubre.Item(2, i).Value, Me.TablaOctubre.Item(3, i).Value,
                                                                      IIf(IsDBNull(Me.TablaOctubre.Item(4, i).Value) = True, 0, Me.TablaOctubre.Item(4, i).Value), IIf(IsDBNull(Me.TablaOctubre.Item(5, i).Value) = True, 0, Me.TablaOctubre.Item(5, i).Value),
                                                              IIf(IsDBNull(Me.TablaOctubre.Item(6, i).Value) = True, 0, Me.TablaOctubre.Item(6, i).Value), IIf(IsDBNull(Me.TablaOctubre.Item(7, i).Value) = True, 0, Me.TablaOctubre.Item(7, i).Value),
                                                               Me.TablaOctubre.Item(9, i).Value, Me.TablaOctubre.Item(10, i).Value, Me.TablaOctubre.Item(11, i).Value,
                                                              Me.TablaOctubre.Item(12, i).Value, Me.TablaOctubre.Item(13, i).Value)
                            Next
                        Case "11"
                            For i As Integer = 0 To Me.TablaNoviembre.RowCount - 1
                                Guarda_Balanza(Me.Anio2.Text, mes, Me.TablaNoviembre.Item(1, i).Value, Me.TablaNoviembre.Item(0, i).Value, Me.TablaNoviembre.Item(2, i).Value, Me.TablaNoviembre.Item(3, i).Value,
                                                                     IIf(IsDBNull(Me.TablaNoviembre.Item(4, i).Value) = True, 0, Me.TablaNoviembre.Item(4, i).Value), IIf(IsDBNull(Me.TablaNoviembre.Item(5, i).Value) = True, 0, Me.TablaNoviembre.Item(5, i).Value),
                                                             IIf(IsDBNull(Me.TablaNoviembre.Item(6, i).Value) = True, 0, Me.TablaNoviembre.Item(6, i).Value), IIf(IsDBNull(Me.TablaNoviembre.Item(7, i).Value) = True, 0, Me.TablaNoviembre.Item(7, i).Value),
                                                              Me.TablaNoviembre.Item(9, i).Value, Me.TablaNoviembre.Item(10, i).Value, Me.TablaNoviembre.Item(11, i).Value,
                                                             Me.TablaNoviembre.Item(12, i).Value, Me.TablaNoviembre.Item(13, i).Value)
                            Next
                        Case "12"
                            For i As Integer = 0 To Me.TablaDiciembre.RowCount - 1
                                Guarda_Balanza(Me.Anio2.Text, mes, Me.TablaDiciembre.Item(1, i).Value, Me.TablaDiciembre.Item(0, i).Value, Me.TablaDiciembre.Item(2, i).Value, Me.TablaDiciembre.Item(3, i).Value,
                                                                    IIf(IsDBNull(Me.TablaDiciembre.Item(4, i).Value) = True, 0, Me.TablaDiciembre.Item(4, i).Value), IIf(IsDBNull(Me.TablaDiciembre.Item(5, i).Value) = True, 0, Me.TablaDiciembre.Item(5, i).Value),
                                                            IIf(IsDBNull(Me.TablaDiciembre.Item(6, i).Value) = True, 0, Me.TablaDiciembre.Item(6, i).Value), IIf(IsDBNull(Me.TablaDiciembre.Item(7, i).Value) = True, 0, Me.TablaDiciembre.Item(7, i).Value),
                                                             Me.TablaDiciembre.Item(9, i).Value, Me.TablaDiciembre.Item(10, i).Value, Me.TablaDiciembre.Item(11, i).Value,
                                                            Me.TablaDiciembre.Item(12, i).Value, Me.TablaDiciembre.Item(13, i).Value)
                            Next


                    End Select
                End If

                frm.Barra.value = a
            Next
            frm.Close()
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow

            Sql = " Delete From Balanzas where Id_Empresa = " & Me.lstClientesMasivos.SelectItem & " and Anio  ='" & Trim(Me.Anio2.Text) & "'  and Mes = '13'"
            If Eventos.Comando_sql(Sql) >= 0 Then
                For i As Integer = 0 To Me.TablaAnual.RowCount - 1
                    Guarda_Balanza(Me.Anio2.Text, 13, Me.TablaAnual.Item(1, i).Value, Me.TablaAnual.Item(0, i).Value, Me.TablaAnual.Item(2, i).Value, Me.TablaAnual.Item(3, i).Value,
                                                               IIf(IsDBNull(Me.TablaAnual.Item(4, i).Value) = True, 0, Me.TablaAnual.Item(4, i).Value), IIf(IsDBNull(Me.TablaAnual.Item(5, i).Value) = True, 0, Me.TablaAnual.Item(5, i).Value),
                                                       IIf(IsDBNull(Me.TablaAnual.Item(6, i).Value) = True, 0, Me.TablaAnual.Item(6, i).Value), IIf(IsDBNull(Me.TablaAnual.Item(7, i).Value) = True, 0, Me.TablaAnual.Item(7, i).Value),
                                                        Me.TablaAnual.Item(9, i).Value, Me.TablaAnual.Item(10, i).Value, Me.TablaAnual.Item(11, i).Value,
                                                       Me.TablaAnual.Item(12, i).Value, Me.TablaAnual.Item(13, i).Value)
                Next
            End If

        Else
            RadMessageBox.Show("Debes seleccionar una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub

    Private Sub Guarda_Balanza(ByVal Anio As String, ByVal Mes As String, ByVal Cuenta As String, ByVal Al As String, ByVal Na As String, ByVal Descripcion As String, ByVal Saldo_Anterior As Decimal,
                               ByVal Debe As Decimal, ByVal Haber As Decimal, ByVal Saldo_Final As Decimal, ByVal Cuenta_Cero As String, ByVal Cargo_Contario As String, ByVal Abono_Contrario As String,
                               ByVal Sin_Cargos As String, ByVal Sin_Abonos As String)
        Dim Sql As String = "
                    INSERT INTO dbo.Balanzas
    	                (

    	                Id_Empresa,
    	                Anio,
    	                Mes,
    	                Cuenta,
    	                Alias,
                        Nat,
    	                Descripcion,
    	                Saldo_Anterior,
    	                Debe,
    	                Haber,
    	                Saldo_Final,
    	                Cuenta_Cero,
    	                Cargo_Contrario,
    	                Abono_Contrario,
    	                Sin_Cargos,
    	                Sin_Abonos
    	                )
                    VALUES 
    	                (

    	                " & Me.lstClientesMasivos.SelectItem & ",
    	                '" & Trim(Anio) & "',
    	                '" & Trim(Mes) & "',
    	                '" & Trim(Cuenta) & "',
    	                '" & Trim(Al) & "',
                        '" & Trim(Na) & "',
    	                '" & Trim(Descripcion) & "',
    	                " & Trim(Saldo_Anterior) & ",
    	                " & Trim(Debe) & ",
    	                " & Trim(Haber) & ",
    	                " & Trim(Saldo_Final) & ",
    	                '" & Trim(Cuenta_Cero) & "',
    	                '" & Trim(Cargo_Contario) & "',
    	                '" & Trim(Abono_Contrario) & "',
    	                '" & Trim(Sin_Cargos) & "',
    	                '" & Trim(Sin_Abonos) & "'
    	                )
                    "
        If Eventos.Comando_sql(Sql) = 0 Then

        End If

    End Sub

    Private Sub CmdCalcular_Click(sender As Object, e As EventArgs) Handles CmdCalcular.Click
        SegundoPlano.RunWorkerAsync()
        Control.CheckForIllegalCrossThreadCalls = False


    End Sub
    Private Sub Busca_Errores(ByVal Tabla As DataGridView)
        Me.TablaErrores.RowCount = 5
        Me.TablaErrores.Item(0, 0).Value = "Cuenta Cero"
        Me.TablaErrores.Item(0, 1).Value = "Cargo Contrario"
        Me.TablaErrores.Item(0, 2).Value = "Abono Contrario"
        Me.TablaErrores.Item(0, 3).Value = "Sin Cargos"
        Me.TablaErrores.Item(0, 4).Value = "Sin Abonos"
        If Tabla.Rows.Count > 0 Then

            Dim Cont As Integer = 0
            For i As Integer = 0 To Tabla.RowCount - 1
                If Tabla.Item(9, i).Value <> "" And Tabla.Item(9, i).Value IsNot Nothing Then
                    Cont += 1
                End If

            Next
            Me.TablaErrores.Item(1, 0).Value = Cont
            Cont = 0
            For i As Integer = 0 To Tabla.RowCount - 1
                If Tabla.Item(10, i).Value <> "" And Tabla.Item(10, i).Value IsNot Nothing Then
                    Cont += 1
                End If

            Next
            Me.TablaErrores.Item(1, 1).Value = Cont
            Cont = 0
            For i As Integer = 0 To Tabla.RowCount - 1
                If Tabla.Item(11, i).Value <> "" And Tabla.Item(11, i).Value IsNot Nothing Then
                    Cont += 1
                End If

            Next
            Me.TablaErrores.Item(1, 2).Value = Cont
            Cont = 0
            For i As Integer = 0 To Tabla.RowCount - 1
                If Tabla.Item(12, i).Value <> "" And Tabla.Item(12, i).Value IsNot Nothing Then
                    Cont += 1
                End If

            Next
            Me.TablaErrores.Item(1, 3).Value = Cont
            Cont = 0
            For i As Integer = 0 To Tabla.RowCount - 1
                If Tabla.Item(13, i).Value <> "" And Tabla.Item(13, i).Value IsNot Nothing Then
                    Cont += 1
                End If

            Next
            Me.TablaErrores.Item(1, 4).Value = Cont
        Else
            Me.TablaErrores.Rows.Clear()
        End If
    End Sub
    Private Sub Balanzas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Balanzas.SelectedIndexChanged
        Select Case Me.Balanzas.SelectedIndex
            Case 0
                Busca_Errores(Tablaenero)
            Case 1
                Busca_Errores(TablaFebrero)
            Case 2
                Busca_Errores(TablaMarzo)
            Case 3
                Busca_Errores(TablaAbril)
            Case 4
                Busca_Errores(TablaMayo)
            Case 5
                Busca_Errores(TablaJunio)
            Case 6
                Busca_Errores(TablaJulio)
            Case 7
                Busca_Errores(TablaAgosto)
            Case 8
                Busca_Errores(TablaSeptiembre)
            Case 9
                Busca_Errores(TablaOctubre)
            Case 10
                Busca_Errores(TablaNoviembre)
            Case 11
                Busca_Errores(TablaDiciembre)
            Case 12
                Busca_Errores(TablaAnual)
            Case 13

        End Select
    End Sub

    Private Sub CmdSalirF_Click(sender As Object, e As EventArgs) Handles CmdSalirF.Click
        Me.Close()
    End Sub

    Private Sub SegundoPlano_DoWork(sender As Object, e As DoWorkEventArgs) Handles SegundoPlano.DoWork
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        My.Forms.Inicio.txtServerDB.Text = serV
        Limpiar3()
        If Me.lstClientesMasivos.SelectText <> "" Then
            If Me.Anio2.Text <> "" Then
                Buscar_Balanzas(Trim(Me.Anio2.Text.ToString()), Me.lstNivel2.SelectItem)
                Inserta_finales()
                Me.CmdQuitarMasivo.PerformClick()
                Me.CmdGuardar.PerformClick()
            End If
        Else
            RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If

    End Sub

    Private Sub SegundoPlano_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles SegundoPlano.RunWorkerCompleted
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If e.Cancelled Then
            RadMessageBox.Show("La ación ha sido cancelada.")
        ElseIf e.Error IsNot Nothing Then
            RadMessageBox.Show("Se ha producido un error durante la ejecución: " & e.Error.Message)

        End If
    End Sub
End Class
