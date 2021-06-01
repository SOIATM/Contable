Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.ComponentModel
Imports System.Math
Imports Telerik.WinControls
Public Class DIOT
    Dim anio = Str(DateTime.Now.Year)
    Dim m = Now.Date.Month.ToString
    Dim activo As Boolean

    Private Sub DIOT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        activo = True
        Cargar_Listas()
        Eventos.DiseñoTabla(Me.Tabla)
        Eventos.DiseñoTabla(Me.TablaErrores)
        Dim i As Integer
        For i = DateTime.Now.Year To DateTime.Now.Year - 5 Step -1
            If i >= 2004 Then
                Me.LstAnio.Items.Add(Str(i))
            End If
        Next
        Me.LstAnio.Text = anio

        If Len(m) < 2 Then
            m = "0" & m
        End If
        Me.ComboMes.Text = m
        activo = False

    End Sub
    Private Sub Cargar_Listas()
        Me.lstCliente.Cargar("SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social FROM     Empresa  ")
        Me.lstCliente.SelectItem = 1


        Me.LstTipoTercero.Cargar("SELECT  '04' , 'Proveedor Nacional' union SELECT  '05' , 'Proveedor Extranjero' union SELECT  '15' , 'Proveedor Global' ")
        Me.LstTipoTercero.SelectText = "Proveedor Nacional"


    End Sub

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If Me.lstCliente.SelectText <> "" Then
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            If RadMessageBox.Show("Se actualizara la informacion de la Empresa  " & Me.lstCliente.SelectText & " para el año " & anio & " esto es correcto?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim Sql As String = "Delete From DIOT  WHERE  Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio  =" & Me.LstAnio.Text.Trim() & " and Mes = '" & Me.ComboMes.Text.Trim() & "' "
                If Eventos.Comando_sql(Sql) > 0 Then
                    Guarda_Ctas()
                End If
            Else
                Exit Sub
            End If
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Debes seleccionar una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
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
            sql = " INSERT INTO dbo.DIOT
	                (	Id_Empresa,	Anio,	Mes,	Tipo_Tercero,	Tipo_Operacion,	RFC,	Num_Id_Fiscal,	Nom_Extranjero,	Pais_Recidencia,
	                Nacionalidad,	Actividades_Pagadas_16,	Actividades_Pagadas_15,	Iva_Pagado_No_Acreditable_15,	Actividades_Pagadas_11,	Actividades_Pagadas_10,
	                Actividades_Pagadas_8,	Iva_Pagado_No_Acreditable_11,	Iva_Pagado_No_Acreditable_8,	Actividades_Pagadas_Import_16,	Actividades_Pagadas_Import_15,
	                Actividades_Pagadas_Import_11,	Iva_Pagado_No_Acreditable_Import_11,	Actividades_Pagadas_Import_Ex,	Actividades_Pagadas_0,
	                Actividades_Pagadas_Ex,	IVA_Retenido,	IVA_Devoluciones
	                )
                VALUES  " & Me.lstCliente.SelectItem & "' , " & Me.LstAnio.Text.Trim() & ",'" & Me.ComboMes.Text.Trim() & "','" & Me.Tabla.Item(Tipo_Tercero.Index, i).Value.trim() & "',
              '" & Trim(Me.Tabla.Item(Tipo_Operacion.Index, i).Value) & "', '" & Trim(Me.Tabla.Item(RFC.Index, i).Value) & "',
              '" & IIf(IsDBNull(Me.Tabla.Item(Num_Id_Fiscal.Index, i).Value) = True, "", Me.Tabla.Item(Num_Id_Fiscal.Index, i).Value) & "',
              '" & IIf(IsDBNull(Me.Tabla.Item(Nom_Extranjero.Index, i).Value) = True, "", Me.Tabla.Item(Nom_Extranjero.Index, i).Value) & "',
              '" & IIf(IsDBNull(Me.Tabla.Item(Pais_Recidencia.Index, i).Value) = True, "", Me.Tabla.Item(Pais_Recidencia.Index, i).Value) & "',
              '" & IIf(IsDBNull(Me.Tabla.Item(Nacionalidad.Index, i).Value) = True, "", Me.Tabla.Item(Nacionalidad.Index, i).Value) & "',
              " & IIf(IsDBNull(Me.Tabla.Item(Actividades_Pagadas_16.Index, i).Value) = True, 0, Me.Tabla.Item(Actividades_Pagadas_16.Index, i).Value) & ",
              " & IIf(IsDBNull(Me.Tabla.Item(Actividades_Pagadas_15.Index, i).Value) = True, 0, Me.Tabla.Item(Actividades_Pagadas_15.Index, i).Value) & ",
              " & IIf(IsDBNull(Me.Tabla.Item(Iva_Pagado_No_Acreditable_15.Index, i).Value) = True, 0, Me.Tabla.Item(Iva_Pagado_No_Acreditable_15.Index, i).Value) & ",
              " & IIf(IsDBNull(Me.Tabla.Item(Actividades_Pagadas_11.Index, i).Value) = True, 0, Me.Tabla.Item(Actividades_Pagadas_11.Index, i).Value) & ",
              " & IIf(IsDBNull(Me.Tabla.Item(Actividades_Pagadas_10.Index, i).Value) = True, 0, Me.Tabla.Item(Actividades_Pagadas_10.Index, i).Value) & ",
              " & IIf(IsDBNull(Me.Tabla.Item(Actividades_Pagadas_8.Index, i).Value) = True, 0, Me.Tabla.Item(Actividades_Pagadas_8.Index, i).Value) & ",
              " & IIf(IsDBNull(Me.Tabla.Item(Iva_Pagado_No_Acreditable_11.Index, i).Value) = True, 0, Me.Tabla.Item(Iva_Pagado_No_Acreditable_11.Index, i).Value) & ",
              " & IIf(IsDBNull(Me.Tabla.Item(Iva_Pagado_No_Acreditable_8.Index, i).Value) = True, 0, Me.Tabla.Item(Iva_Pagado_No_Acreditable_8.Index, i).Value) & ",
              " & IIf(IsDBNull(Me.Tabla.Item(Actividades_Pagadas_Import_16.Index, i).Value) = True, 0, Me.Tabla.Item(Actividades_Pagadas_Import_16.Index, i).Value) & ",
              " & IIf(IsDBNull(Me.Tabla.Item(Actividades_Pagadas_Import_15.Index, i).Value) = True, 0, Me.Tabla.Item(Actividades_Pagadas_Import_15.Index, i).Value) & ",
              " & IIf(IsDBNull(Me.Tabla.Item(Actividades_Pagadas_Import_11.Index, i).Value) = True, 0, Me.Tabla.Item(Actividades_Pagadas_Import_11.Index, i).Value) & ",
              " & IIf(IsDBNull(Me.Tabla.Item(Iva_Pagado_No_Acreditable_Import_11.Index, i).Value) = True, 0, Me.Tabla.Item(Iva_Pagado_No_Acreditable_Import_11.Index, i).Value) & ",
              " & IIf(IsDBNull(Me.Tabla.Item(Actividades_Pagadas_Import_Ex.Index, i).Value) = True, 0, Me.Tabla.Item(Actividades_Pagadas_Import_Ex.Index, i).Value) & ",
              " & IIf(IsDBNull(Me.Tabla.Item(Actividades_Pagadas_0.Index, i).Value) = True, 0, Me.Tabla.Item(Actividades_Pagadas_0.Index, i).Value) & ",
              " & IIf(IsDBNull(Me.Tabla.Item(Actividades_Pagadas_Ex.Index, i).Value) = True, 0, Me.Tabla.Item(Actividades_Pagadas_Ex.Index, i).Value) & ",
              " & IIf(IsDBNull(Me.Tabla.Item(IVA_Retenido.Index, i).Value) = True, 0, Me.Tabla.Item(IVA_Retenido.Index, i).Value) & ",
              " & IIf(IsDBNull(Me.Tabla.Item(IVA_Devoluciones.Index, i).Value) = True, 0, Me.Tabla.Item(IVA_Devoluciones.Index, i).Value) & ")"
            If Eventos.Comando_sql(sql) = 0 Then
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("No se guardo la informacion error en " & Trim(Me.Tabla.Item(RFC.Index, i).Value) & " verifique la Informacion ", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                sql = "Delete From DIOT where Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio  =" & Trim(Me.LstAnio.Text) & " and Mes ='" & Me.ComboMes.Text.Trim() & "' "
                If Eventos.Comando_sql(sql) = 0 Then

                End If
            End If
            frm.Barra.Value = i
        Next
        frm.Close()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub CmdCalcular_Click(sender As Object, e As EventArgs) Handles CmdCalcular.Click

        Dim sql As String = "  SELECT 	Id_Diot,	Id_Empresa,	Anio,	Mes,	Tipo_Tercero,	Tipo_Operacion,	RFC,	Num_Id_Fiscal,	Nom_Extranjero,
	Pais_Recidencia,	Nacionalidad,	Actividades_Pagadas_16,	Actividades_Pagadas_15,	Iva_Pagado_No_Acreditable_15,	Actividades_Pagadas_11,
	Actividades_Pagadas_10,	Actividades_Pagadas_8,	Iva_Pagado_No_Acreditable_11,	Iva_Pagado_No_Acreditable_8,	Actividades_Pagadas_Import_16,
	Actividades_Pagadas_Import_15,	Actividades_Pagadas_Import_11,	Iva_Pagado_No_Acreditable_Import_11,	Actividades_Pagadas_Import_Ex,	Actividades_Pagadas_0,
	Actividades_Pagadas_Ex,	IVA_Retenido,	IVA_Devoluciones FROM dbo.DIOT
                               WHERE  Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio  =" & Me.LstAnio.Text.Trim() & " and Mes = '" & Me.ComboMes.Text.Trim() & "' ORDER BY Id_Diot  "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then

            If MessageBox.Show("Ya existe un Calculo de DIOT para la Empresa  " & Me.lstCliente.SelectText & " para el año " & Me.LstAnio.Text.Trim() & " 
            y del mes " & Me.ComboMes.Text.Trim() & " deseas Reemplasarla?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Calcula_Diot() ' Calcular Diot
                'Guarda_Ctas()
            Else
                Me.Tabla.RowCount = ds.Tables(0).Rows.Count
                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = Me.Tabla.RowCount - 1
                Me.Cursor = Cursors.AppStarting
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Me.Tabla.Item(Tipo_Tercero.Index, i).Value = ds.Tables(0).Rows(i)("Tipo_Tercero")
                    Me.Tabla.Item(Tipo_Operacion.Index, i).Value = ds.Tables(0).Rows(i)("Tipo_Operacion")
                    Me.Tabla.Item(RFC.Index, i).Value = ds.Tables(0).Rows(i)("RFC")
                    Me.Tabla.Item(Num_Id_Fiscal.Index, i).Value = ds.Tables(0).Rows(i)("Num_Id_Fiscal")
                    Me.Tabla.Item(Nom_Extranjero.Index, i).Value = ds.Tables(0).Rows(i)("Nom_Extranjero")
                    Me.Tabla.Item(Pais_Recidencia.Index, i).Value = ds.Tables(0).Rows(i)("Pais_Recidencia")
                    Me.Tabla.Item(Nacionalidad.Index, i).Value = ds.Tables(0).Rows(i)("Nacionalidad")
                    Me.Tabla.Item(Actividades_Pagadas_16.Index, i).Value = ds.Tables(0).Rows(i)("Actividades_Pagadas_16")
                    Me.Tabla.Item(Actividades_Pagadas_15.Index, i).Value = ds.Tables(0).Rows(i)("Actividades_Pagadas_15")
                    Me.Tabla.Item(Iva_Pagado_No_Acreditable_15.Index, i).Value = ds.Tables(0).Rows(i)("Iva_Pagado_No_Acreditable_15")
                    Me.Tabla.Item(Actividades_Pagadas_11.Index, i).Value = ds.Tables(0).Rows(i)("Actividades_Pagadas_11")
                    Me.Tabla.Item(Actividades_Pagadas_10.Index, i).Value = ds.Tables(0).Rows(i)("Actividades_Pagadas_10")
                    Me.Tabla.Item(Actividades_Pagadas_8.Index, i).Value = ds.Tables(0).Rows(i)("Actividades_Pagadas_8")
                    Me.Tabla.Item(Iva_Pagado_No_Acreditable_11.Index, i).Value = ds.Tables(0).Rows(i)("Iva_Pagado_No_Acreditable_11")
                    Me.Tabla.Item(Iva_Pagado_No_Acreditable_8.Index, i).Value = ds.Tables(0).Rows(i)("Iva_Pagado_No_Acreditable_8")
                    Me.Tabla.Item(Actividades_Pagadas_Import_16.Index, i).Value = ds.Tables(0).Rows(i)("Actividades_Pagadas_Import_16")
                    Me.Tabla.Item(Actividades_Pagadas_Import_15.Index, i).Value = ds.Tables(0).Rows(i)("Actividades_Pagadas_Import_15") 'Este es el IVA de las actividades pagadas a al 15-16
                    Me.Tabla.Item(Actividades_Pagadas_Import_11.Index, i).Value = ds.Tables(0).Rows(i)("Actividades_Pagadas_Import_11")
                    Me.Tabla.Item(Iva_Pagado_No_Acreditable_Import_11.Index, i).Value = ds.Tables(0).Rows(i)("Iva_Pagado_No_Acreditable_Import_11")
                    Me.Tabla.Item(Actividades_Pagadas_Import_Ex.Index, i).Value = ds.Tables(0).Rows(i)("Actividades_Pagadas_Import_Ex")
                    Me.Tabla.Item(Actividades_Pagadas_0.Index, i).Value = ds.Tables(0).Rows(i)("Actividades_Pagadas_0")
                    Me.Tabla.Item(Actividades_Pagadas_Ex.Index, i).Value = ds.Tables(0).Rows(i)("Actividades_Pagadas_Ex")
                    Me.Tabla.Item(IVA_Retenido.Index, i).Value = ds.Tables(0).Rows(i)("IVA_Retenido")
                    Me.Tabla.Item(IVA_Devoluciones.Index, i).Value = ds.Tables(0).Rows(i)("IVA_Devoluciones")

                    frm.Barra.Value = i
                Next
                frm.Close()
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Me.Cursor = Cursors.Arrow



            End If


        Else

            Calcula_Diot()
            BuscarIVAS()
        End If
        Me.LstTipoTercero_Enters()
    End Sub
    Public Class Ctas_Diot
        Public Property Cuenta As String
        Public Property Nivel As String
        Public Property Tipo As String
        Public Property Valor As String
        Public Property Tasa As String
    End Class
    Private Class Rfc_Diot
        Public Property RFC As String
        Public Property Saldo As Integer
        Public Property Tipo As String
        Public Property Tasa As String
        Public Property Valor As String
    End Class
    Private Class Rfc_CTAS
        Public Property Cuenta As String
        Public Property Saldo As Integer

    End Class
    Private Sub Pintar(ByVal I As Integer, ByVal C As Integer)

        If Me.Tabla.Item(C, I).Value < 0 Then
            Me.Tabla.Item(C, I).Style.ForeColor = Color.Red
        End If
    End Sub
    Private Sub Calcula_Diot()
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.Clear()
        End If
        Dim Filas As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS(" SELECT  Cuenta,	Nivel,	Tipo,	Valor,	Tasa FROM dbo.Cuentas_Diot where Id_Empresa =" & Me.lstCliente.SelectItem & " order by Cuenta")
        If ds.Tables(0).Rows.Count > 0 Then
            Dim Cuentas As New List(Of Ctas_Diot)
            Dim Rfcs As New List(Of Rfc_Diot)

            For j As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Cuentas.Add(New Ctas_Diot() With {.Cuenta = ds.Tables(0).Rows(j)("Cuenta").ToString.Substring(0, 19), .Nivel = ds.Tables(0).Rows(j)("Nivel").ToString,
                            .Tipo = ds.Tables(0).Rows(j)("Tipo").ToString, .Valor = ds.Tables(0).Rows(j)("Valor").ToString, .Tasa = ds.Tables(0).Rows(j)("Tasa").ToString})
            Next
            Dim where As String = ""
            Dim SQL As String = ""
            For Each Cuenta In Cuentas
                If Cuenta.Cuenta.ToString.Substring(10, 4) = "0000" And Cuenta.Cuenta.ToString.Substring(5, 4) = "0000" And Cuenta.Cuenta.ToString.Substring(0, 4) <> "0000" Then 'Nivel 1
                    where = " Nivel1  = '" & Cuenta.Cuenta.ToString.Substring(0, 4) & "' AND " & Cuenta.Nivel & " <> '0000'  "
                ElseIf Cuenta.Cuenta.ToString.Substring(10, 4) = "0000" And Cuenta.Cuenta.ToString.Substring(5, 4) <> "0000" And Cuenta.Cuenta.ToString.Substring(0, 4) <> "0000" Then 'Nivel 2
                    where = " Nivel1  = '" & Cuenta.Cuenta.ToString.Substring(0, 4) & "' AND Nivel2  = '" & Cuenta.Cuenta.ToString.Substring(5, 4) & "' AND " & Cuenta.Nivel & " <> '0000'  "
                ElseIf Cuenta.Cuenta.ToString.Substring(10, 4) <> "0000" And Cuenta.Cuenta.ToString.Substring(5, 4) <> "0000" And Cuenta.Cuenta.ToString.Substring(0, 4) <> "0000" Then 'Nivel 2
                    where = " Nivel1  = '" & Cuenta.Cuenta.ToString.Substring(0, 4) & "' AND Nivel2  = '" & Cuenta.Cuenta.ToString.Substring(5, 4) & "' AND Nivel3  = '" & Cuenta.Cuenta.ToString.Substring(10, 4) & "' AND " & Cuenta.Nivel & " <> '0000'  "
                End If

                SQL = "SELECT Catalogo_de_Cuentas.RFC , Catalogo_de_Cuentas.Cuenta,Sum(Detalle_Polizas." & Cuenta.Tipo & ") AS Saldo
                        FROM Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                        INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                        INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                        INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                        WHERE Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " AND Detalle_Polizas.Cuenta IN (SELECT Cuenta FROM Catalogo_de_Cuentas 
                        WHERE Catalogo_de_Cuentas.Id_Empresa = " & Me.lstCliente.SelectItem & " AND   " & where & " AND Catalogo_de_Cuentas.RFC <> 'NULL'
                        )
                        AND Polizas.ID_anio= " & Me.LstAnio.Text.Trim() & " AND Polizas.ID_mes= '" & Me.ComboMes.Text.Trim() & "' GROUP BY Catalogo_de_Cuentas.Cuenta ,Catalogo_de_Cuentas.RFC"

                ds.Clear()
                ds = Eventos.Obtener_DS(SQL)
                Dim Igual As Boolean = False
                If ds.Tables(0).Rows.Count > 0 Then

                    If Rfcs.Count > 0 Then

                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                            For Each it In Rfcs


                                If ds.Tables(0).Rows(i)("RFC").ToString.Trim() = it.RFC.Trim() Then
                                    Dim valor As Rfc_Diot
                                    valor = Rfcs.Find(Function(S) it.Tasa = Cuenta.Tasa)
                                    Try
                                        If valor.Tasa.ToString() = Cuenta.Tasa.ToString() Then
                                            it.Saldo = it.Saldo + IIf(Cuenta.Valor = "N", ds.Tables(0).Rows(i)("Saldo") * -1, ds.Tables(0).Rows(i)("Saldo"))
                                            Igual = True
                                            Filas -= 1
                                        Else
                                            Igual = False
                                            Filas -= 1
                                        End If
                                    Catch ex As Exception
                                        Filas -= 1
                                        Igual = False
                                    End Try
                                    Exit For
                                Else
                                    Igual = False
                                End If
                            Next
                            If Igual = False Then
                                Filas += 1
                                Rfcs.Add(New Rfc_Diot() With {.RFC = ds.Tables(0).Rows(i)("RFC").ToString.Trim(), .Saldo = IIf(Cuenta.Valor = "N", ds.Tables(0).Rows(i)("Saldo") * -1, ds.Tables(0).Rows(i)("Saldo")), .Tasa = Cuenta.Tasa, .Tipo = Cuenta.Tipo})
                            End If
                        Next
                    Else

                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                            If Rfcs.Count > 0 Then
                                For Each it In Rfcs


                                    If ds.Tables(0).Rows(i)("RFC").ToString.Trim() = it.RFC.Trim() Then
                                        Dim valor As Rfc_Diot
                                        valor = Rfcs.Find(Function(S) it.Tasa = Cuenta.Tasa)
                                        Try
                                            If valor.Tasa.ToString() = Cuenta.Tasa.ToString() Then
                                                it.Saldo = it.Saldo + IIf(Cuenta.Valor = "N", ds.Tables(0).Rows(i)("Saldo") * -1, ds.Tables(0).Rows(i)("Saldo"))
                                                Igual = True
                                                Filas -= 1
                                            Else
                                                Igual = False
                                                Filas -= 1
                                            End If
                                        Catch ex As Exception
                                            Filas -= 1
                                            Igual = False
                                        End Try
                                        Exit For
                                    Else
                                        Igual = False
                                    End If
                                Next
                                If Igual = False Then
                                    Filas += 1
                                    Rfcs.Add(New Rfc_Diot() With {.RFC = ds.Tables(0).Rows(i)("RFC").ToString.Trim(), .Saldo = IIf(Cuenta.Valor = "N", ds.Tables(0).Rows(i)("Saldo") * -1, ds.Tables(0).Rows(i)("Saldo")), .Tasa = Cuenta.Tasa, .Tipo = Cuenta.Tipo})
                                End If
                            Else
                                Rfcs.Add(New Rfc_Diot() With {.RFC = ds.Tables(0).Rows(i)("RFC").ToString.Trim(), .Saldo = IIf(Cuenta.Valor = "N", ds.Tables(0).Rows(i)("Saldo") * -1, ds.Tables(0).Rows(i)("Saldo")), .Tasa = Cuenta.Tasa, .Tipo = Cuenta.Tipo})
                                Filas += 1
                            End If






                        Next
                    End If

                End If

            Next
            If Rfcs.Count > 0 Then


                Me.Tabla.RowCount = Filas
                Dim Fi = 0
                Dim Lista As New List(Of Rfc_Diot)

                For j As Integer = 0 To Rfcs.Count - 1
                    Dim valor As Rfc_Diot
                    valor = Lista.Find(Function(S) S.RFC = Rfcs(j).RFC.Trim())
                    Try
                        If valor.RFC Is Nothing Then
                            Lista.Add(New Rfc_Diot() With {.RFC = Rfcs(j).RFC.Trim(), .Saldo = 0, .Tasa = "", .Tipo = ""})
                        Else

                        End If
                    Catch ex As Exception
                        Lista.Add(New Rfc_Diot() With {.RFC = Rfcs(j).RFC.Trim(), .Saldo = 0, .Tasa = "", .Tipo = ""})
                    End Try

                Next
                Me.Tabla.RowCount = Lista.Count


                For i As Integer = 0 To Lista.Count - 1
                    Me.Tabla.Item(RFC.Index, i).Value = Lista(i).RFC.Trim()
                Next
                For j As Integer = 0 To Rfcs.Count - 1
                    For i As Integer = 0 To Me.Tabla.Rows.Count - 1
                        If Me.Tabla.Item(RFC.Index, i).Value = Rfcs(j).RFC.Trim() Then
                            Try
                                Select Case Rfcs(j).Tasa
                                    Case "16%"
                                        Me.Tabla.Item(Actividades_Pagadas_16.Index, i).Value = Rfcs(j).Saldo
                                        Pintar(i, Actividades_Pagadas_16.Index)
                                        Me.Tabla.Item(IVA_Retenido.Index, i).Value = Calcula_Ivas_Retenidos(Rfcs(j).RFC, 0.16, i)
                                        Pintar(i, IVA_Retenido.Index)
                                    Case "Cero"
                                        Me.Tabla.Item(Actividades_Pagadas_0.Index, i).Value = Rfcs(j).Saldo
                                        Pintar(i, Actividades_Pagadas_0.Index)
                                    Case "8%"
                                        Me.Tabla.Item(IVA_Retenido.Index, i).Value = Calcula_Ivas_Retenidos(Rfcs(j).RFC, 0.08, i)
                                        Pintar(i, IVA_Retenido.Index)
                                    Case "Exenta"
                                        Me.Tabla.Item(Actividades_Pagadas_Ex.Index, i).Value = Rfcs(j).Saldo
                                        Pintar(i, Actividades_Pagadas_Ex.Index)
                                End Select
                                Exit For
                            Catch ex As Exception

                            End Try
                        End If
                    Next
                Next





            End If

        End If



    End Sub


    Private Function Calcula_Ivas_Retenidos2(ByVal RFC As String, ByVal Tasa As Decimal, ByVal i As Integer)
        ' buscar cuenta del rfc y aplica formula
        Dim Valor As Integer = 0
        Dim Cuentas As New List(Of Rfc_CTAS)
        Dim importe As Integer = 0
        Dim DS As DataSet = Eventos.Obtener_DS("SELECT Cuenta ,Valor FROM Cuentas_Impuestos WHERE   Id_Empresa = " & Me.lstCliente.SelectItem & " ")
        If DS.Tables(0).Rows.Count > 0 Then
            Dim sql As String = ""
            For j As Integer = 0 To DS.Tables(0).Rows.Count - 1


                Dim cta As String = DS.Tables(0).Rows(0)("Cuenta").ToString.Substring(0, 19).Replace("-", "")
                Dim Ctas As DataSet = Eventos.Obtener_DS("SELECT Catalogo_de_Cuentas.RFC,Catalogo_de_Cuentas.Cuenta FROM Catalogo_de_Cuentas WHERE Id_Empresa = " & Me.lstCliente.SelectItem & " AND Nivel1 = '" & cta.Substring(0, 4) & "' AND Nivel2 = '" & cta.Substring(4, 4) & "'AND Nivel3 ='" & cta.Substring(8, 4) & "' AND Nivel4>0 AND (RFC ='" & RFC.Trim() & "' )")
                If Ctas.Tables(0).Rows.Count > 0 Then

                    For s As Integer = 0 To Ctas.Tables(0).Rows.Count - 1

                        sql = "SELECT Catalogo_de_Cuentas.RFC , Catalogo_de_Cuentas.Cuenta,Sum(Detalle_Polizas.Cargo) AS Saldo
                                FROM Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta 
                                AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                WHERE Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & "  AND Detalle_Polizas.Cuenta IN (SELECT Cuenta FROM Catalogo_de_Cuentas 
                                WHERE Catalogo_de_Cuentas.Id_Empresa = " & Me.lstCliente.SelectItem & " AND  Catalogo_de_Cuentas.Cuenta = '" & Ctas.Tables(0).Rows(s)("Cuenta").ToString & "' AND Catalogo_de_Cuentas.RFC <> 'NULL'
                                )
                                AND Polizas.ID_anio= " & Me.LstAnio.Text.Trim() & " AND Polizas.ID_mes= '" & Me.ComboMes.Text.Trim() & "' GROUP BY Catalogo_de_Cuentas.Cuenta ,Catalogo_de_Cuentas.RFC"
                        Dim saldo As DataSet = Eventos.Obtener_DS(sql)

                        If saldo.Tables(0).Rows.Count > 0 Then
                            Try


                                Valor = Valor + (saldo.Tables(0).Rows(0)("Saldo") * DS.Tables(0).Rows(j)("Valor"))

                                Select Case saldo.Tables(0).Rows(0)("Cuenta").ToString.Substring(0, 12)
                                    Case "601000200045"

                                        Me.Tabla.Item(Tipo_Operacion.Index, i).Value = "06"
                                    Case "601000200072"

                                        Me.Tabla.Item(Tipo_Operacion.Index, i).Value = "85"
                                    Case "601000200034"

                                        Me.Tabla.Item(Tipo_Operacion.Index, i).Value = "03"
                                    Case "601000200035"

                                        Me.Tabla.Item(Tipo_Operacion.Index, i).Value = "03"

                                End Select
                            Catch ex As Exception

                            End Try
                        Else
                            Me.Tabla.Item(Tipo_Operacion.Index, i).Value = "85"
                        End If
                    Next
                Else
                    Me.Tabla.Item(Tipo_Operacion.Index, i).Value = "85"
                End If


            Next
        Else
            Me.Tabla.Item(Tipo_Operacion.Index, i).Value = "85"

        End If



        Return Valor
    End Function





    Private Function Calcula_Ivas_Retenidos(ByVal RFC As String, ByVal Tasa As Decimal, ByVal i As Integer)
        ' buscar cuenta del rfc y aplica formula
        Dim Valor As Integer = 0
        Dim Cuentas As New List(Of Rfc_CTAS)
        Dim importe As Integer = 0
        Dim DS As DataSet = Eventos.Obtener_DS("SELECT Catalogo_de_Cuentas.RFC,Catalogo_de_Cuentas.Cuenta FROM Catalogo_de_Cuentas WHERE Id_Empresa = " & Me.lstCliente.SelectItem & " AND Nivel1 = '6010' AND Nivel2 = '0020' AND Nivel3 IN ('0045','0072','0035','0034') AND Nivel4>0 AND (RFC ='" & RFC & "' )")
        If DS.Tables(0).Rows.Count > 0 Then
            Dim sql As String = ""
            For j As Integer = 0 To DS.Tables(0).Rows.Count - 1

                sql = "SELECT Catalogo_de_Cuentas.RFC , Catalogo_de_Cuentas.Cuenta,Sum(Detalle_Polizas.Cargo) AS Saldo
                        FROM Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                        INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                        INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta 
                        AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                        WHERE  Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & "  AND Detalle_Polizas.Cuenta IN (SELECT Cuenta FROM Catalogo_de_Cuentas 
                        WHERE Catalogo_de_Cuentas.Id_Empresa = " & Me.lstCliente.SelectItem & " AND  Catalogo_de_Cuentas.Cuenta = '" & DS.Tables(0).Rows(j)("Cuenta").ToString & "' AND Catalogo_de_Cuentas.RFC <> 'NULL'
                        )
                        AND Polizas.ID_anio= " & Me.LstAnio.Text.Trim() & " AND Polizas.ID_mes= '" & Me.ComboMes.Text.Trim() & "' GROUP BY Catalogo_de_Cuentas.Cuenta ,Catalogo_de_Cuentas.RFC"
                Dim saldo As DataSet = Eventos.Obtener_DS(sql)

                If saldo.Tables(0).Rows.Count > 0 Then
                    Select Case DS.Tables(0).Rows(j)("Cuenta").ToString.Substring(0, 12)
                        Case "601000200045"
                            Valor = Valor + ((saldo.Tables(0).Rows(0)("Saldo") * Tasa) / 3) * 2
                            Me.Tabla.Item(Tipo_Operacion.Index, i).Value = "06"
                        Case "601000200072"
                            Valor = Valor + saldo.Tables(0).Rows(0)("Saldo") * 0.04
                            Me.Tabla.Item(Tipo_Operacion.Index, i).Value = "85"
                        Case "601000200034"
                            Valor = Valor + ((saldo.Tables(0).Rows(0)("Saldo") * Tasa) / 3) * 2
                            Me.Tabla.Item(Tipo_Operacion.Index, i).Value = "03"
                        Case "601000200035"
                            Valor = Valor + ((saldo.Tables(0).Rows(0)("Saldo") * Tasa) / 3) * 2
                            Me.Tabla.Item(Tipo_Operacion.Index, i).Value = "03"
                    End Select
                End If
            Next
        Else
            Me.Tabla.Item(Tipo_Operacion.Index, i).Value = "85"

        End If



        Return Valor
    End Function

    Private Sub LstTipoTercero_Enters() Handles LstTipoTercero.Enters
        If activo = False Then
            If Me.Tabla.Rows.Count > 0 Then
                For i As Integer = 0 To Me.Tabla.Rows.Count - 1
                    Me.Tabla.Item(Tipo_Tercero.Index, i).Value = Me.LstTipoTercero.SelectItem
                Next
            End If
        End If
    End Sub

    Private Sub CmdCuentasDiot_Click(sender As Object, e As EventArgs) Handles CmdCuentasDiot.Click
        Eventos.Abrir_form(Cuentas_IVA_AC)
    End Sub

    Private Sub CmdCtasIVADiot_Click(sender As Object, e As EventArgs) Handles CmdCtasIVADiot.Click
        Eventos.Abrir_form(Cuentas_Diot)
    End Sub

    Private Sub BuscarIVAS()
        Dim Ds As DataSet = Eventos.Obtener_DS("SELECT  Id_ctas_IVA_AC, Cuenta, Id_Empresa ,Abono FROM dbo.Cuentas_IVA_AC where Id_Empresa =" & Me.lstCliente.SelectItem & "")
        If Ds.Tables(0).Rows.Count > 0 Then
            Me.TablaErrores.RowCount = 1
            Dim sql As String = ""
            Dim su As Decimal = 0
            Dim D16 As Decimal = 0
            Dim D8 As Decimal = 0
            Dim tipo As String = ""
            For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1

                If Eventos.Bool2(Ds.Tables(0).Rows(i)("Abono")) = 0 Then
                    tipo = "Detalle_Polizas.Cargo"
                Else
                    tipo = "Detalle_Polizas.Abono *-1 "
                End If
                sql = "Select  Catalogo_de_Cuentas.Cuenta,Sum(" & tipo & ") As Saldo
                        FROM Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                        INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                        INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta 
                        AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                        WHERE Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " AND Detalle_Polizas.Cuenta IN (SELECT Cuenta FROM Catalogo_de_Cuentas 
                        WHERE Catalogo_de_Cuentas.Id_Empresa = " & Me.lstCliente.SelectItem & " AND  
                        Catalogo_de_Cuentas.Cuenta = '" & Ds.Tables(0).Rows(i)("Cuenta").ToString.Substring(0, 19).Replace("-", "").Trim() & "'  
                        )
                        AND Polizas.ID_anio= " & Me.LstAnio.Text.Trim() & " AND Polizas.ID_mes= '" & Me.ComboMes.Text.Trim() & "' GROUP BY Catalogo_de_Cuentas.Cuenta "
                Dim saldo As DataSet = Eventos.Obtener_DS(sql)
                If saldo.Tables(0).Rows.Count > 0 Then
                    su += saldo.Tables(0).Rows(0)("Saldo")
                End If



            Next
            Me.TablaErrores.Item(IvaAc.Index, 0).Value = su
            For i As Integer = 0 To Me.Tabla.Rows.Count - 1
                D16 = D16 + Me.Tabla.Item(Actividades_Pagadas_16.Index, i).Value
                D8 = D8 + Me.Tabla.Item(Actividades_Pagadas_8.Index, i).Value
            Next
            Me.TablaErrores.Item(Suma.Index, 0).Value = D16
            Me.TablaErrores.Item(Ocho.Index, 0).Value = D8
            Me.TablaErrores.Item(Dif.Index, 0).Value = ((D8 + D16) * 0.16) - su
        End If


    End Sub

    Private Sub CmdGenerar_Click(sender As Object, e As EventArgs) Handles CmdGenerar.Click
        If Me.Tabla.Rows.Count > 0 Then
            Genera_TXT()
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No se ha cargado ningun Registro...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub

    Private Sub CmdAbrir_Click(sender As Object, e As EventArgs) Handles CmdAbrir.Click
        Eventos.Abrir_Capeta("C:\Program Files\Contable\SetupProyectoContable\DIOT")
    End Sub

    Private Sub Genera_TXT()
        Dim valor As String = IIf(Me.ChekCeros.Checked = True, 0, "")
        Dim fila As String = Nothing
        Dim strStreamW As Stream = Nothing
        Dim strStreamWriter As StreamWriter = Nothing
        Dim texto As String = Nothing
        ' Donde guardamos los paths (la direccion de mi sukiri) de los archivos que vamos a estar utilizando ..
        Dim PathArchivo As String
        Dim nuevo_archivo As String
        Dim Ordinal As String

        Dim Anio As String = ""
        Dim Mes As String = ""
        Dim Tercero As String = ""
        Dim Operacion As String = ""
        Dim RF As String = ""
        Dim No_IDFisacal As String = ""
        Dim Nom_Extra As String = ""
        Dim PasiReci As String = ""
        Dim Nacion As String = ""
        Dim Act1615 As String = ""
        Dim Act15 As String = ""
        Dim MotoIva1516 As String = ""
        Dim Act1011 As String = ""
        Dim Act10 As String = ""
        Dim Act08 As String = ""
        Dim ActFro As String = ""
        Dim MontoIVA1011 As String = ""
        Dim MontoIVA811 As String = ""
        Dim Imp1516 As String = ""
        Dim IvaImp1516 As String = ""
        Dim IMP1011 As String = ""
        Dim IvaImp1011 As String = ""


        Dim Acividades11 As String = ""


        Dim Act0 As String = ""
        Dim ActE As String = ""
        Dim IVARet As String = ""
        Dim IvaDev As String = ""

        Dim rfcNombre As String = Eventos.ObtenerValorDB("Empresa", "Reg_fed_causantes", " Id_Empresa= " & Me.lstCliente.SelectItem & "", 1)
        Dim mess As String = IIf(Me.ComboMes.Text = "*", "13", Me.ComboMes.Text)
        Dim parte As String = rfcNombre & " " & Me.LstAnio.Text & " " & mess  'Format(Today.Date, "yyyyMMdd")
        Try
            If Directory.Exists("C:\Program Files\Contable\SetupProyectoContable\DIOT") = False Then ' si no existe la carpeta se crea
                Directory.CreateDirectory("C:\Program Files\Contable\SetupProyectoContable\DIOT")
            End If
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            PathArchivo = "C:\Program Files\Contable\SetupProyectoContable\DIOT\" & parte & "-1.txt" ' Se determina el nombre del archivo para la tranferencia concatenando la fecha actual
            ' asignar el valor de la cuenta

            If File.Exists(PathArchivo) Then
                ' Dim ultimoXML As String = (From item As String In IO.Directory.GetFiles("C:\Program Files\TUSA-ATM\SetupProyectoTUSAATMV2\Tranferencias", "*.text", SearchOption.AllDirectories) Order By item Descending Select item).FirstOrDefault
                Dim ultimo_nombre As String = File.GetLastWriteTime("C:\Program Files\Contable\SetupProyectoContable\DIOT")

                For Each item As String In Directory.GetFiles("C:\Program Files\Contable\SetupProyectoContable\DIOT", "*.txt", False)
                    Dim fecha As String = File.GetLastWriteTime(item)
                    '   If ultimo_nombre <> fecha Then
                    nuevo_archivo = 1 + Val(Path.GetFileName(item).ToString.Substring(9, 2))
                    PathArchivo = "C:\Program Files\Contable\SetupProyectoContable\DIOT\" & parte & "-" & nuevo_archivo & ".txt" ' Se determina nuevo nombre del archivo
                    strStreamW = File.Create(PathArchivo)
                    strStreamWriter = New StreamWriter(strStreamW, System.Text.Encoding.Default) ' tipo de codificacion para escritura del archivo
                    'Linea pricipal 

                    Dim contador As Integer = 0
                    For i As Integer = 0 To Me.Tabla.Rows.Count - 1



                        Tercero = IIf(IsNothing(Me.Tabla.Item(Tipo_Tercero.Index, i).Value), "04", Me.Tabla.Item(Tipo_Tercero.Index, i).Value).ToString.PadLeft(2, "0")
                        Operacion = IIf(IsNothing(Me.Tabla.Item(Tipo_Operacion.Index, i).Value), "85", Me.Tabla.Item(Tipo_Operacion.Index, i).Value).ToString
                        RF = IIf(IsNothing(Me.Tabla.Item(RFC.Index, i).Value), "", Me.Tabla.Item(RFC.Index, i).Value).ToString
                        No_IDFisacal = IIf(IsNothing(Me.Tabla.Item(Num_Id_Fiscal.Index, i).Value), "", Me.Tabla.Item(Num_Id_Fiscal.Index, i).Value).ToString
                        Nom_Extra = IIf(IsNothing(Me.Tabla.Item(Nom_Extranjero.Index, i).Value), "", Me.Tabla.Item(Nom_Extranjero.Index, i).Value).ToString
                        PasiReci = IIf(IsNothing(Me.Tabla.Item(Pais_Recidencia.Index, i).Value), "", Me.Tabla.Item(Pais_Recidencia.Index, i).Value).ToString
                        Nacion = IIf(IsNothing(Me.Tabla.Item(Nacionalidad.Index, i).Value), "", Me.Tabla.Item(Nacionalidad.Index, i).Value).ToString
                        Act1615 = IIf(IsNothing(Me.Tabla.Item(Actividades_Pagadas_16.Index, i).Value) Or Me.Tabla.Item(Actividades_Pagadas_16.Index, i).Value = 0, valor, Me.Tabla.Item(Actividades_Pagadas_16.Index, i).Value)
                        Act15 = IIf(IsNothing(Me.Tabla.Item(Actividades_Pagadas_15.Index, i).Value) Or Me.Tabla.Item(Actividades_Pagadas_15.Index, i).Value = 0, valor, Val(Me.Tabla.Item(Actividades_Pagadas_15.Index, i).Value))
                        MotoIva1516 = IIf(IsNothing(Me.Tabla.Item(Iva_Pagado_No_Acreditable_15.Index, i).Value) Or Me.Tabla.Item(Iva_Pagado_No_Acreditable_15.Index, i).Value = 0, valor, Val(Me.Tabla.Item(Iva_Pagado_No_Acreditable_15.Index, i).Value))
                        Act1011 = IIf(IsNothing(Me.Tabla.Item(Actividades_Pagadas_Import_11.Index, i).Value) Or Me.Tabla.Item(Actividades_Pagadas_Import_11.Index, i).Value = 0, valor, Me.Tabla.Item(Actividades_Pagadas_Import_11.Index, i).Value)

                        Acividades11 = IIf(IsNothing(Me.Tabla.Item(Actividades_Pagadas_11.Index, i).Value) Or Me.Tabla.Item(Actividades_Pagadas_11.Index, i).Value = 0, valor, Me.Tabla.Item(Actividades_Pagadas_11.Index, i).Value)



                        Act10 = valor
                        ActFro = IIf(IsNothing(Me.Tabla.Item(Actividades_Pagadas_8.Index, i).Value) Or Me.Tabla.Item(Actividades_Pagadas_8.Index, i).Value = 0, valor, Me.Tabla.Item(Actividades_Pagadas_8.Index, i).Value)
                        MontoIVA1011 = IIf(IsNothing(Me.Tabla.Item(Iva_Pagado_No_Acreditable_11.Index, i).Value) Or Me.Tabla.Item(Iva_Pagado_No_Acreditable_11.Index, i).Value = 0, valor, Me.Tabla.Item(Iva_Pagado_No_Acreditable_11.Index, i).Value)
                        MontoIVA811 = IIf(IsNothing(Me.Tabla.Item(Iva_Pagado_No_Acreditable_8.Index, i).Value) Or Me.Tabla.Item(Iva_Pagado_No_Acreditable_8.Index, i).Value = 0, valor, Me.Tabla.Item(Iva_Pagado_No_Acreditable_8.Index, i).Value)
                        Imp1516 = IIf(IsNothing(Me.Tabla.Item(Actividades_Pagadas_Import_15.Index, i).Value) Or Me.Tabla.Item(Actividades_Pagadas_Import_15.Index, i).Value = 0, valor, Me.Tabla.Item(Actividades_Pagadas_Import_15.Index, i).Value)
                        IvaImp1516 = valor
                        IMP1011 = IIf(IsNothing(Me.Tabla.Item(Actividades_Pagadas_Import_11.Index, i).Value) Or Me.Tabla.Item(Actividades_Pagadas_Import_11.Index, i).Value = 0, valor, Me.Tabla.Item(Actividades_Pagadas_Import_11.Index, i).Value)
                        IvaImp1011 = IIf(IsNothing(Me.Tabla.Item(Iva_Pagado_No_Acreditable_Import_11.Index, i).Value) Or Me.Tabla.Item(Iva_Pagado_No_Acreditable_Import_11.Index, i).Value = 0, valor, Me.Tabla.Item(Iva_Pagado_No_Acreditable_Import_11.Index, i).Value)
                        Act0 = IIf(IsNothing(Me.Tabla.Item(Actividades_Pagadas_0.Index, i).Value) Or Me.Tabla.Item(Actividades_Pagadas_0.Index, i).Value = 0, valor, Me.Tabla.Item(Actividades_Pagadas_0.Index, i).Value)
                        ActE = IIf(IsNothing(Me.Tabla.Item(Actividades_Pagadas_Ex.Index, i).Value) Or Me.Tabla.Item(Actividades_Pagadas_Ex.Index, i).Value = 0, valor, Me.Tabla.Item(Actividades_Pagadas_Ex.Index, i).Value)
                        IVARet = IIf(IsNothing(Me.Tabla.Item(IVA_Retenido.Index, i).Value) Or Me.Tabla.Item(IVA_Retenido.Index, i).Value = 0, valor, Me.Tabla.Item(IVA_Retenido.Index, i).Value)
                        IvaDev = IIf(IsNothing(Me.Tabla.Item(IVA_Devoluciones.Index, i).Value) Or Me.Tabla.Item(IVA_Devoluciones.Index, i).Value = 0, valor, Me.Tabla.Item(IVA_Devoluciones.Index, i).Value)
                        Dim cadena As String = ""
                        cadena = Tercero & "|" & Operacion & "|" & RF & "|" & No_IDFisacal & "|" & Nom_Extra & "|" & PasiReci & "|" & Nacion & "|" & Act1615 & "|" & Act15 & "|" & MotoIva1516 & "|" & Act1011 & "|" & Act10 & "|" & ActFro & "|" & MontoIVA1011 & "|" & MontoIVA811 & "|" & Imp1516 & "|" & IvaImp1516 & "|" & Acividades11 & "|" & IMP1011 & "|" & IvaImp1011 & "|" & Act0 & "|" & ActE & "|" & IVARet & "|" & IvaDev & "|"
                        strStreamWriter.WriteLine(cadena)
                    Next
                    strStreamWriter.Flush()
                    strStreamWriter.Close() ' cerramos

                Next
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("Proceso terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Else
                nuevo_archivo = 1
                ' si no existe se crea ys e eliminan todos los anteriores
                For Each item As String In Directory.GetFiles("C:\Program Files\Contable\SetupProyectoContable\Polizas", "*.txt", False)
                    My.Computer.FileSystem.DeleteFile(item)
                Next
                strStreamW = File.Create(PathArchivo)
                strStreamWriter = New StreamWriter(strStreamW, System.Text.Encoding.Default) ' tipo de codificacion para escritura del archivo
                'Linea pricipal 
                Ordinal = nuevo_archivo
                Ordinal = Format(Int(Ordinal), "0000")

                Dim contador As Integer = 0
                For i As Integer = 0 To Me.Tabla.Rows.Count - 1
                    Tercero = IIf(IsNothing(Me.Tabla.Item(Tipo_Tercero.Index, i).Value), "04", Me.Tabla.Item(Tipo_Tercero.Index, i).Value).ToString.PadLeft(2, "0")
                    Operacion = IIf(IsNothing(Me.Tabla.Item(Tipo_Operacion.Index, i).Value), "85", Me.Tabla.Item(Tipo_Operacion.Index, i).Value).ToString
                    RF = IIf(IsNothing(Me.Tabla.Item(RFC.Index, i).Value), "", Me.Tabla.Item(RFC.Index, i).Value).ToString
                    No_IDFisacal = IIf(IsNothing(Me.Tabla.Item(Num_Id_Fiscal.Index, i).Value), "", Me.Tabla.Item(Num_Id_Fiscal.Index, i).Value).ToString
                    Nom_Extra = IIf(IsNothing(Me.Tabla.Item(Nom_Extranjero.Index, i).Value), "", Me.Tabla.Item(Nom_Extranjero.Index, i).Value).ToString
                    PasiReci = IIf(IsNothing(Me.Tabla.Item(Pais_Recidencia.Index, i).Value), "", Me.Tabla.Item(Pais_Recidencia.Index, i).Value).ToString
                    Nacion = IIf(IsNothing(Me.Tabla.Item(Nacionalidad.Index, i).Value), "", Me.Tabla.Item(Nacionalidad.Index, i).Value).ToString
                    Act1615 = IIf(IsNothing(Me.Tabla.Item(Actividades_Pagadas_16.Index, i).Value) Or Me.Tabla.Item(Actividades_Pagadas_16.Index, i).Value = 0, valor, Me.Tabla.Item(Actividades_Pagadas_16.Index, i).Value)
                    Act15 = IIf(IsNothing(Me.Tabla.Item(Actividades_Pagadas_15.Index, i).Value) Or Me.Tabla.Item(Actividades_Pagadas_15.Index, i).Value = 0, valor, Val(Me.Tabla.Item(Actividades_Pagadas_15.Index, i).Value))
                    MotoIva1516 = IIf(IsNothing(Me.Tabla.Item(Iva_Pagado_No_Acreditable_15.Index, i).Value) Or Me.Tabla.Item(Iva_Pagado_No_Acreditable_15.Index, i).Value = 0, valor, Val(Me.Tabla.Item(Iva_Pagado_No_Acreditable_15.Index, i).Value))
                    Act1011 = IIf(IsNothing(Me.Tabla.Item(Actividades_Pagadas_Import_11.Index, i).Value) Or Me.Tabla.Item(Actividades_Pagadas_Import_11.Index, i).Value = 0, valor, Me.Tabla.Item(Actividades_Pagadas_Import_11.Index, i).Value)
                    Acividades11 = IIf(IsNothing(Me.Tabla.Item(Actividades_Pagadas_11.Index, i).Value) Or Me.Tabla.Item(Actividades_Pagadas_11.Index, i).Value = 0, valor, Me.Tabla.Item(Actividades_Pagadas_11.Index, i).Value)
                    Act10 = valor
                    ActFro = IIf(IsNothing(Me.Tabla.Item(Actividades_Pagadas_8.Index, i).Value) Or Me.Tabla.Item(Actividades_Pagadas_8.Index, i).Value = 0, valor, Me.Tabla.Item(Actividades_Pagadas_8.Index, i).Value)
                    MontoIVA1011 = IIf(IsNothing(Me.Tabla.Item(Iva_Pagado_No_Acreditable_11.Index, i).Value) Or Me.Tabla.Item(Iva_Pagado_No_Acreditable_11.Index, i).Value = 0, valor, Me.Tabla.Item(Iva_Pagado_No_Acreditable_11.Index, i).Value)
                    MontoIVA811 = IIf(IsNothing(Me.Tabla.Item(Iva_Pagado_No_Acreditable_8.Index, i).Value) Or Me.Tabla.Item(Iva_Pagado_No_Acreditable_8.Index, i).Value = 0, valor, Me.Tabla.Item(Iva_Pagado_No_Acreditable_8.Index, i).Value)
                    Imp1516 = IIf(IsNothing(Me.Tabla.Item(Actividades_Pagadas_Import_15.Index, i).Value) Or Me.Tabla.Item(Actividades_Pagadas_Import_15.Index, i).Value = 0, valor, Me.Tabla.Item(Actividades_Pagadas_Import_15.Index, i).Value)
                    IvaImp1516 = valor
                    IMP1011 = IIf(IsNothing(Me.Tabla.Item(Actividades_Pagadas_Import_11.Index, i).Value) Or Me.Tabla.Item(Actividades_Pagadas_Import_11.Index, i).Value = 0, valor, Me.Tabla.Item(Actividades_Pagadas_Import_11.Index, i).Value)
                    IvaImp1011 = IIf(IsNothing(Me.Tabla.Item(Iva_Pagado_No_Acreditable_Import_11.Index, i).Value) Or Me.Tabla.Item(Iva_Pagado_No_Acreditable_Import_11.Index, i).Value = 0, valor, Me.Tabla.Item(Iva_Pagado_No_Acreditable_Import_11.Index, i).Value)
                    Act0 = IIf(IsNothing(Me.Tabla.Item(Actividades_Pagadas_0.Index, i).Value) Or Me.Tabla.Item(Actividades_Pagadas_0.Index, i).Value = 0, valor, Me.Tabla.Item(Actividades_Pagadas_0.Index, i).Value)
                    ActE = IIf(IsNothing(Me.Tabla.Item(Actividades_Pagadas_Ex.Index, i).Value) Or Me.Tabla.Item(Actividades_Pagadas_Ex.Index, i).Value = 0, valor, Me.Tabla.Item(Actividades_Pagadas_Ex.Index, i).Value)
                    IVARet = IIf(IsNothing(Me.Tabla.Item(IVA_Retenido.Index, i).Value) Or Me.Tabla.Item(IVA_Retenido.Index, i).Value = 0, valor, Me.Tabla.Item(IVA_Retenido.Index, i).Value)
                    IvaDev = IIf(IsNothing(Me.Tabla.Item(IVA_Devoluciones.Index, i).Value) Or Me.Tabla.Item(IVA_Devoluciones.Index, i).Value = 0, valor, Me.Tabla.Item(IVA_Devoluciones.Index, i).Value)
                    Dim cadena As String = ""
                    'cadena = Tercero & "|" & Operacion & "|" & RF & "|" & No_IDFisacal & "|" & Nom_Extra & "|" & PasiReci & "|" & Nacion & "|" & Act1615 & "|" & Act15 & "|" & MotoIva1516 & "|" & Act1011 & "|" & Act10 & "|" & ActFro & "|" & MontoIVA1011 & "|" & MontoIVA811 & "|" & Imp1516 & "|" & IvaImp1516 & "|" & IMP1011 & "|" & IvaImp1011 & "|" & Act0 & "|" & ActE & "|" & IVARet & "|" & IvaDev
                    cadena = Tercero & "|" & Operacion & "|" & RF & "|" & No_IDFisacal & "|" & Nom_Extra & "|" & PasiReci & "|" & Nacion & "|" & Act1615 & "|" & Act15 & "|" & MotoIva1516 & "|" & Act1011 & "|" & Act10 & "|" & ActFro & "|" & MontoIVA1011 & "|" & MontoIVA811 & "|" & Imp1516 & "|" & IvaImp1516 & "|" & Acividades11 & "|" & IMP1011 & "|" & IvaImp1011 & "|" & Act0 & "|" & ActE & "|" & IVARet & "|" & IvaDev & "|"
                    strStreamWriter.WriteLine(cadena)
                Next

                strStreamWriter.Flush()
                strStreamWriter.Close() ' cerramos
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("Proceso terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            End If
        Catch ex As Exception
            MsgBox("Error al Guardar la informacion en el archivo. " & ex.ToString, MsgBoxStyle.Critical, Application.ProductName)
            strStreamWriter.Close() ' cerramos
        End Try
    End Sub

    Private Sub CmdImpuestos_Click(sender As Object, e As EventArgs) Handles CmdImpuestos.Click
        Eventos.Abrir_form(Cuentas_Impuestos)
    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub
End Class