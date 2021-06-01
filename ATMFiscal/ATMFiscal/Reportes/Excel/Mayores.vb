Imports System.IO
Imports Telerik.WinControls
Public Class Mayores
    Private Sub Mayores_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar()
        Limpiar()
    End Sub
    Private Sub Cargar()
        Me.LstCliente.Cargar("SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social FROM     Empresa  ")
        Me.LstCliente.SelectItem = 1

        Me.LstNiveles.Cargar("SELECT 1, 'Nivel1' UNION SELECT 2, 'Nivel2' UNION SELECT 3, 'Nivel3' UNION SELECT 4, 'Nivel4' ")

        Me.LstNiveles.Combo.SelectedIndex = 0
        For i = DateTime.Now.Year To DateTime.Now.Year - 10 Step -1
            If i >= 2010 Then
                Me.ComboAñoB.Items.Add(Str(i))
            End If
        Next
        Me.Panel.Text = Str(DateTime.Now.Year)
        Me.Lstctainicial.Cargar("SELECT cuenta, Rtrim(Descripcion) + '/' +  Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4  from Catalogo_de_Cuentas where Id_Empresa = " & Me.LstCliente.SelectItem & "   order by nivel1,nivel2,nivel3,nivel4")
        Me.Lstctainicial.Combo.SelectedIndex = 0
        Me.Lstctafinal.Cargar("SELECT cuenta, Rtrim(Descripcion) + '/' + Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4   from Catalogo_de_Cuentas where Id_Empresa = " & Me.LstCliente.SelectItem & "   order by nivel1,nivel2,nivel3,nivel4")
        Me.Lstctafinal.Combo.SelectedIndex = Me.Lstctafinal.Combo.Items.Count - 1
    End Sub

    Private Sub CmdExp_Click(sender As Object, e As EventArgs) Handles CmdExp.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.Tabla.RowCount > 0 Then
            If Me.Tabla.Columns.Count > 256 Then
                RadMessageBox.Show("El rango de fechas sobrepasa las columnas de una hoja de excel, disminuye el rango...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                Exit Sub
            End If
            Dim excel As Microsoft.Office.Interop.Excel.Application = Eventos.NuevoExcelMeses("vacio", False)
            For col As Integer = 1 To Me.Tabla.Columns.Count - 1
                Eventos.EscribeExcel(excel, 1, col, Me.Tabla.Columns(col).HeaderText)
            Next
            For i As Integer = 0 To Me.Tabla.RowCount - 1
                For j As Integer = 1 To Me.Tabla.Columns.Count - 1
                    Eventos.EscribeExcel(excel, i + 1, j, Valor(Me.Tabla.Item(j, i).Value))
                Next
            Next
            Eventos.Mostrar_Excel(excel)
        Else
            RadMessageBox.Show("No hay datos para exportar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
        End If
    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        Limpiar()
    End Sub
    Private Sub Limpiar()
        Me.Tabla.Rows.Clear()
    End Sub

    Private Sub Buscar_cuentas(ByVal Nivel As String)
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Eventos.Cuentas(Me.Panel.Text.Trim(), Me.LstCliente.SelectItem)
        Dim Sa As Decimal = 0
        Dim Where As String = ""
        Dim Ctas As String = "Nivel1 >='" & Me.Lstctainicial.SelectItem.ToString().Substring(0, 4) & "' AND nivel1<='" & Me.Lstctafinal.SelectItem.ToString().Substring(0, 4) & "'"

        Select Case Nivel
            Case 1
                Where = " And  A.Nivel1 >0 and A.Nivel2='0000' " & " AND A.Nivel1 IN(SELECT Nivel1  FROM (SELECT DISTINCT SUBSTRING(convert(VARCHAR,detalle_polizas.Cuenta), 0, 5) AS Nivel1 FROM Detalle_Polizas  INNER JOIN Polizas ON Polizas.Id_Empresa =" & Me.LstCliente.SelectItem & " AND Polizas.ID_anio = " & Trim(Me.ComboAñoB.SelectedItem.ToString()) & " ) AS T WHERE " & Ctas & ")"
            Case 2
                Where = " And  A.Nivel1 > 0 And  A.Nivel2 >='0000' and A.Nivel3='0000' " & " AND A.Nivel1 IN(SELECT Nivel1  FROM (SELECT DISTINCT SUBSTRING(convert(VARCHAR,detalle_polizas.Cuenta), 0, 5) AS Nivel1 FROM Detalle_Polizas  INNER JOIN Polizas ON Polizas.Id_Empresa =" & Me.LstCliente.SelectItem & " AND Polizas.ID_anio = " & Trim(Me.ComboAñoB.SelectedItem.ToString) & ") AS T WHERE " & Ctas & ")"
            Case 3
                Where = " And  A.Nivel1 > 0 And  A.Nivel2  >='0000' And  A.Nivel3  >='0000' and A.Nivel4='0000' " & " AND A.Nivel1 IN(SELECT Nivel1  FROM (SELECT DISTINCT SUBSTRING(convert(VARCHAR,detalle_polizas.Cuenta), 0, 5) AS Nivel1 FROM Detalle_Polizas  INNER JOIN Polizas ON Polizas.Id_Empresa =" & Me.LstCliente.SelectItem & " AND Polizas.ID_anio = " & Trim(Me.ComboAñoB.SelectedItem.ToString) & ") AS T WHERE " & Ctas & ")"
            Case 4
                Where = " And  A.Nivel1 > 0 And  A.Nivel2  >='0000' And  A.Nivel3  >='0000' and A.Nivel4 >='0000' " & " AND A.Nivel1 IN(SELECT Nivel1  FROM (SELECT DISTINCT SUBSTRING(convert(VARCHAR,detalle_polizas.Cuenta), 0, 5) AS Nivel1 FROM Detalle_Polizas  INNER JOIN Polizas ON Polizas.Id_Empresa =" & Me.LstCliente.SelectItem & " AND Polizas.ID_anio = " & Trim(Me.ComboAñoB.SelectedItem.ToString) & ") AS T WHERE " & Ctas & ")"
        End Select


        Dim Datos As String = "SELECT A.Cuenta ,A.ID_cuenta,A.Descripcion, A.Naturaleza"
        Datos &= " FROM   Empresa AS C "
        Datos &= " LEFT OUTER JOIN (Select  Cuenta, "
        Datos &= " CASE WHEN nivel4 >'0000' THEN nivel1 + '-' + nivel2 + '-' + nivel3 + '-' + nivel4 "
        Datos &= " WHEN nivel3 >'0000' THEN nivel1 + '-' + nivel2 + '-' + nivel3 "
        Datos &= " WHEN nivel2 >'0000' THEN nivel1 + '-' + nivel2"
        Datos &= " WHEN  nivel2 ='0000' then nivel1 END  AS ID_cuenta, "
        Datos &= " Rtrim(Descripcion) as Descripcion ,Naturaleza,Id_Empresa ,Nivel1 , Nivel2 , Nivel3 , Nivel4 "
        Datos &= " from catalogo_de_cuentas where catalogo_de_cuentas.Cuenta IN (SELECT Cuenta FROM Cuentas_Con_Saldo WHERE Anio = " & Trim(Me.ComboAñoB.SelectedItem.ToString) & " AND Id_Empresa = " & Me.LstCliente.SelectItem & "  )) AS A ON A.Id_Empresa = C.Id_Empresa"
        Datos &= " WHERE C.Id_Empresa = " & Me.LstCliente.SelectItem & " " & Where & " order by A.Cuenta"

        Dim sql As String = "Select  cuenta, CASE WHEN nivel4 >'0000' THEN nivel1 + '-' + nivel2 + '-' + nivel3 + '-' + nivel4 WHEN nivel3 >'0000' THEN nivel1 + '-' + nivel2 + '-' + nivel3 WHEN nivel2 >'0000' THEN nivel1 + '-' + nivel2 WHEN  nivel2 ='0000' then nivel1 END  AS ID_cuenta,   Rtrim(Descripcion) as Descripcion ,Naturaleza
	                                from catalogo_de_cuentas where Id_Empresa= " & Me.LstCliente.SelectItem & " " & Where & " order by cuenta"

        Dim ds As DataSet = Eventos.Obtener_DS(Datos)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.RowCount = ds.Tables(0).Rows.Count
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.Tabla.RowCount - 1
            Me.Cursor = Cursors.AppStarting
            Dim posicion As Integer = 0
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Me.Tabla.Item(0, posicion).Value = ds.Tables(0).Rows(i)(0)
                Me.Tabla.Item(1, posicion).Value = ds.Tables(0).Rows(i)(1)
                Me.Tabla.Item(2, posicion).Value = ds.Tables(0).Rows(i)(2)
                Me.Tabla.Item(3, posicion).Value = ds.Tables(0).Rows(i)(3)
                ' calcular los saldos iniciales
                Me.Tabla.Item(6, posicion).Value = "Saldo Inicial de la Cuenta"
                ' Eventos.Calcula_Saldos_Iniciales(Me.Tabla.Item(1, posicion).Value.ToString, "01/01/" & Trim(Me.ComboAñoB.SelectedItem), Me.LstCliente.SelectItem)
                ' Dim acumulado As Decimal = Calcula_Saldos_InicialesP(dscuentas.Tables(0).Rows(i)("cuenta"), Me.DtInicio.Value.ToString.Substring(6, 4) - 1, Me.lstCliente.SelectItem, "  ( Polizas.Fecha_captura <" & Eventos.Sql_hoy(Me.DtInicio.Value.ToString.Substring(0, 10)) & " )")
                Me.Tabla.Item(7, posicion).Value = Eventos.Calcula_Saldos_Iniciales(Me.Tabla.Item(0, posicion).Value.ToString.PadRight(16, "0"), Trim(Me.ComboAñoB.SelectedItem.ToString) - 1, Me.LstCliente.SelectItem)
                ' Se carga el Saldo inicial de la Cuenta
                Sa = Sa + Me.Tabla.Item(7, posicion).Value
                'Busacar debe y haber  de cada mes
                Dim mes As String = ""
                Where = ""

                Me.Tabla.RowCount = Me.Tabla.RowCount + 12
                Select Case Len(Trim(Me.Tabla.Item(1, posicion).Value.Replace("-", "")))
                    Case 4
                        Where = "   Nivel1 = '" & Trim(Me.Tabla.Item(1, posicion).Value) & "' and Nivel2 >= '0000' "
                    Case 8
                        Where = "  Nivel1 = '" & Trim(Me.Tabla.Item(1, posicion).Value.ToString.Substring(0, 4)) & "' and Nivel2   = '" & Trim(Me.Tabla.Item(1, posicion).Value.Replace("-", "").ToString.Substring(4, 4)) & "' and Nivel3 >= '0000' "
                    Case 12
                        Where = "  Nivel1 = '" & Trim(Me.Tabla.Item(1, posicion).Value.ToString.Substring(0, 4)) & "' and Nivel2   = '" & Trim(Me.Tabla.Item(1, posicion).Value.Replace("-", "").ToString.Substring(4, 4)) & "' and Nivel3='" & Trim(Me.Tabla.Item(1, posicion).Value.Replace("-", "").ToString.Substring(8, 4)) & "' and Nivel4 >= '0000' "
                    Case 16
                        Where = "   Nivel1 = '" & Trim(Me.Tabla.Item(1, posicion).Value.ToString.Substring(0, 4)) & "' and Nivel2   = '" & Trim(Me.Tabla.Item(1, posicion).Value.Replace("-", "").ToString.Substring(4, 4)) & "' and Nivel3='" & Trim(Me.Tabla.Item(1, posicion).Value.Replace("-", "").ToString.Substring(8, 4)) & "' and Nivel4 ='" & Trim(Me.Tabla.Item(1, posicion).Value.Replace("-", "").ToString.Substring(12, 4)) & "'  "
                End Select
                For a As Integer = 1 To 12
                    posicion = posicion + 1
                    mes = a
                    If Len(mes) < 2 Then
                        mes = "0" & mes
                    End If
                    Me.Tabla.Item(Cta.Index, posicion).Value = ds.Tables(0).Rows(i)(0)
                    Me.Tabla.Item(M.Index, posicion).Value = Eventos.MesEnletra(mes)

                    Dim sql2 As String = "SELECT CASE WHEN Catalogo_de_Cuentas.Naturaleza= 'D' then   sum(Detalle_Polizas.Cargo)   - sum(Detalle_Polizas.Abono ) 
	                               When  Catalogo_de_Cuentas.Naturaleza= 'A' THEN   sum(Detalle_Polizas.Cargo ) - sum(Detalle_Polizas.Abono) END AS Saldo  ,
	                              sum(Detalle_Polizas.Cargo) as Cargos,sum(Detalle_Polizas.Abono ) as Abonos
	                                FROM Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
	                               INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
	                               INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
	                               INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa 
	                               WHERE   " & Where & " And polizas.ID_anio = " & Me.ComboAñoB.SelectedItem.ToString & " And Polizas.ID_mes = " & mes & " and Polizas.Id_Empresa= " & Me.LstCliente.SelectItem & "
	                               GROUP BY Polizas.ID_anio,Polizas.ID_mes,Catalogo_de_Cuentas.Naturaleza   "
                    Dim ds2 As DataSet = Eventos.Obtener_DS(sql2)
                    If ds2.Tables(0).Rows.Count > 0 Then


                        Me.Tabla.Item(Deb.Index, posicion).Value = ds2.Tables(0).Rows(0)(1)
                        Me.Tabla.Item(Hab.Index, posicion).Value = ds2.Tables(0).Rows(0)(2)
                        Sa = Sa + IIf(IsDBNull(ds2.Tables(0).Rows(0)(0)) = True, 0, ds2.Tables(0).Rows(0)(0))
                        Me.Tabla.Item(Saldo.Index, posicion).Value = Sa

                    Else
                        Me.Tabla.Item(Deb.Index, posicion).Value = 0
                        Me.Tabla.Item(Hab.Index, posicion).Value = 0
                        Me.Tabla.Item(Saldo.Index, posicion).Value = Sa
                    End If
                    ' Se acumula el saldo Final de la Cuenta
                    '   Sa = Sa + ds2.Tables(0).Rows(0)(0)
                Next
                posicion = posicion + 1
                'se reinicia el saldo de las cuentas
                Sa = 0
                frm.Barra.Value = i

            Next
            Colorear()
            frm.Close()
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow
        Else
            Me.Tabla.RowCount = 0
        End If

    End Sub

    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdImportar.Click
        Limpiar()
        Buscar_cuentas(Me.LstNiveles.SelectItem)
    End Sub
    Private Sub Colorear()
        Dim contador As Integer = 0
        For Each Fila As DataGridViewRow In Tabla.Rows
            If Fila.Cells(2).Value <> Nothing Then
                Fila.DefaultCellStyle.BackColor = Color.PaleGreen
                contador = contador + 1
            End If
        Next

    End Sub



    Private Sub CmdPdf_Click(sender As Object, e As EventArgs) Handles CmdPdf.Click
        MayorPDF()
    End Sub
    Private Sub MayorPDF()
        Dim DT As New DataTable
        With DT
            .Columns.Add("IdCuenta")
            .Columns.Add("Cuenta")
            .Columns.Add("Descripcion")
            .Columns.Add("Naturaleza")
            .Columns.Add("Mes")
            .Columns.Add("Debe", Type.GetType("System.Decimal"))
            .Columns.Add("Haber", Type.GetType("System.Decimal"))
            .Columns.Add("Saldo", Type.GetType("System.Decimal"))
            .Columns.Add("Inicial", Type.GetType("System.Decimal"))
        End With
        Dim Cu, Nt, Des As String
        Dim Sl As Decimal = 0
        For Each dr As DataGridViewRow In Me.Tabla.Rows
            If IsNumeric(dr.Cells(Cta.Index).Value) And dr.Cells(Cuent.Index).Value IsNot Nothing Then
                DT.Rows.Add(dr.Cells(Cta.Index).Value, dr.Cells(Cuent.Index).Value, dr.Cells(Desc.Index).Value, dr.Cells(Naturaleza.Index).Value, dr.Cells(M.Index).Value, Nothing, Nothing, dr.Cells(Saldo.Index).Value * 1, dr.Cells(Saldo.Index).Value * 1)
                Cu = dr.Cells(Cuent.Index).Value.ToString()
                Nt = dr.Cells(Naturaleza.Index).Value
                Des = dr.Cells(Desc.Index).Value
                Sl = dr.Cells(Saldo.Index).Value * 1
            ElseIf dr.Cells(Cuent.Index).Value Is Nothing Then
                DT.Rows.Add(dr.Cells(Cta.Index).Value, Cu, Des, Nt, dr.Cells(M.Index).Value, dr.Cells(Deb.Index).Value * 1, dr.Cells(Hab.Index).Value * 1, dr.Cells(Saldo.Index).Value * 1, Sl)
            End If
        Next

        Try
            Dim Cr As New ReporteMayorPDF
            Cr.Database.Tables("Mayor").SetDataSource(DT)
            Dim Reporte As New ReporteBalanzaGeneral
            Reporte.CrvBalanza.ReportSource = Cr
            Dim Anio As New CrystalDecisions.Shared.ParameterDiscreteValue()
            Dim Rptinv As New CrystalDecisions.Shared.ParameterValues()
            Anio.Value = Me.Panel.Text.Trim()
            Rptinv.Add(Anio)
            Cr.DataDefinition.ParameterFields("Año").ApplyCurrentValues(Rptinv)
            Dim RFC As String = Eventos.ObtenerValorDB("Empresa", "RFC", " Id_Empresa = " & Me.LstCliente.SelectItem & "", True)
            Dim Cliente As New CrystalDecisions.Shared.ParameterDiscreteValue()
            Dim Cl As New CrystalDecisions.Shared.ParameterValues()
            Cliente.Value = Me.LstCliente.SelectText & vbCr & RFC
            Cl.Add(Cliente)
            Cr.DataDefinition.ParameterFields("Cliente").ApplyCurrentValues(Cl)
            Dim Clt As DataSet = Eventos.Obtener_DS("select * from Empresa where Id_Empresa = " & Me.LstCliente.SelectItem & " ")

            Cliente.Value = Clt.Tables(0).Rows(0)("Direccion")
            Cl.Add(Cliente)
            Cr.DataDefinition.ParameterFields("Direccion").ApplyCurrentValues(Cl)

            Dim Archivo As String = "C:\Program Files\Contable\SetupProyectoContable\Mayores\PDF\Mayores" & RFC & ".pdf"
            If Directory.Exists("C:\Program Files\Contable\SetupProyectoContable\Mayores\PDF") = False Then
                ' si no existe la carpeta se crea
                Directory.CreateDirectory("C:\Program Files\Contable\SetupProyectoContable\Mayores\PDF")
            End If
            If Directory.Exists(Archivo) Then
                My.Computer.FileSystem.DeleteFile(Archivo)
            End If
            Cr.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Archivo)
            Process.Start(Archivo)
        Catch ex As Exception

        End Try
    End Sub


End Class