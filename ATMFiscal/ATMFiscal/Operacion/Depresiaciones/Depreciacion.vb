Imports Telerik.WinControls
Public Class Depreciacion
    Dim Negrita_verde As New DataGridViewCellStyle
    Dim Negrita_morado As New DataGridViewCellStyle
    Dim anio = Str(DateTime.Now.Year)
    Dim m = Now.Date.Month.ToString
    Private Sub CmdListar_Click(sender As Object, e As EventArgs) Handles CmdListar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.lstCliente.SelectText <> "" Then

            If Trim(Me.LstAnio.Text) <> "" Then
                If Me.TablaImportar.Rows.Count > 0 Then
                    Me.TablaImportar.Rows.Clear()
                    Crear_Filas(Trim(Me.LstAnio.Text), Me.lstCliente.SelectItem)
                Else
                    Crear_Filas(Trim(Me.LstAnio.Text), Me.lstCliente.SelectItem)
                End If
                ResumenAnual()
                DesgloseMensual()
                Calculaporcentajes(9999)
            Else
                RadMessageBox.Show("Debes seleccionar un Año para consultar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            End If
        Else
            RadMessageBox.Show("Debes seleccionar una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If
    End Sub
    Private Sub Crear_Filas(ByVal Anio As Integer, ByVal Cliente As Integer)
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Negrita_verde.Font = New Font(Me.TablaImportar.Font, FontStyle.Bold)
        Negrita_verde.BackColor = Color.LawnGreen
        Negrita_morado.Font = New Font(Me.TablaImportar.Font, FontStyle.Bold)
        Negrita_morado.BackColor = Color.Khaki
        Dim Tpos As New List(Of Tipos)
        Dim Tipos_S As New List(Of Tipo_Suma)
        Dim Titulo As DataGridViewRow
        Dim sql As String = "  SELECT Id_Depreciacion	,	Cuenta,	Folio,	Cta_Depreciacion,	Resultado_Dep_1,	Porc_Dep_1,	Resultado_Dep_2,	Porc_Dep_2,	Resultado_Dep_3,
		        Porc_Dep_3,	Resultado_Dep_4,	Porc_Dep_4,	Fecha_Uso,	Fecha_Adquisicion,	Descripcion,	MOI,	Tasa,	Dep_Acumulada_Anteriro,
		        Meses_Depreciacion,	Dep_Ejercicio,	Dep_Acumulada_Actual,	Monto_Por_Depreciar,	Fecha_Baja,	Dep_Contable_Fiscal,	INPC_Mes,
		        INPC_Ultima_M,	INPC_Mes_Adq,Factor_Actualizacion,	Dep_Actualizada,	Tipo,	Anio,	Id_Empresa,Sumas FROM dbo.Depreciaciones
	            WHERE  Id_Empresa = " & Cliente & " and Anio  =" & Anio & "  ORDER BY Id_Depreciacion  "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If RadMessageBox.Show("Ya existe Informacion de la Empresa " & Me.lstCliente.SelectText & " para el año " & Anio & " deseas reemplazarla?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim Del As String = "Delete From Depreciaciones where Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio  =" & Trim(Me.LstAnio.Text) & " "
                If Eventos.Comando_sql(Del) > 0 Then
                    Calcular()
                    Guarda_Ctas()
                End If

            Else
                Dim Cuentas As DataSet = Eventos.Obtener_DS("  SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias   From Catalogo_de_Cuentas Where  Id_Empresa = " & Me.lstCliente.SelectItem & " AND Nivel1 IN (" & Ctas(Me.lstCliente.SelectItem) & " ) order by Alias ")
                If Cuentas.Tables(0).Rows.Count > 0 Then
                    If Me.Cta.Items.Count = 0 Then
                        For i As Integer = 0 To Cuentas.Tables(0).Rows.Count - 1
                            Me.Cta.Items.Add(Trim(Cuentas.Tables(0).Rows(i)("Alias")))
                        Next
                    Else
                        Me.Cta.Items.Clear()
                        For i As Integer = 0 To Cuentas.Tables(0).Rows.Count - 1
                            Me.Cta.Items.Add(Trim(Cuentas.Tables(0).Rows(i)("Alias")))

                        Next
                    End If

                End If

                Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count
                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = Me.TablaImportar.RowCount - 1
                Me.Cursor = Cursors.AppStarting
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(i)

                    Try
                        If ds.Tables(0).Rows(i)("Cuenta") <> "" Then
                            Fila.Cells(Cta.Index).Value = Me.Cta.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("Cuenta")), Me.Cta))
                        End If
                    Catch ex As Exception

                    End Try

                    Me.TablaImportar.Item(Cta_Dep.Index, i).Value = ds.Tables(0).Rows(i)("Cta_Depreciacion")


                    Me.TablaImportar.Item(Cta_Dep_1.Index, i).Value = ds.Tables(0).Rows(i)("Resultado_Dep_1")
                    Me.TablaImportar.Item(Cta_Dep_2.Index, i).Value = ds.Tables(0).Rows(i)("Resultado_Dep_2")
                    Me.TablaImportar.Item(Cta_Dep_3.Index, i).Value = ds.Tables(0).Rows(i)("Resultado_Dep_3")
                    Me.TablaImportar.Item(Cta_Dep_4.Index, i).Value = ds.Tables(0).Rows(i)("Resultado_Dep_4")


                    Me.TablaImportar.Item(Pdep1.Index, i).Value = ds.Tables(0).Rows(i)("Porc_Dep_1")
                    Me.TablaImportar.Item(Pdep2.Index, i).Value = ds.Tables(0).Rows(i)("Porc_Dep_2")
                    Me.TablaImportar.Item(Pdep3.Index, i).Value = ds.Tables(0).Rows(i)("Porc_Dep_3")
                    Me.TablaImportar.Item(Pdep4.Index, i).Value = ds.Tables(0).Rows(i)("Porc_Dep_4")
                    Dim fecha As Object = ds.Tables(0).Rows(i)("Fecha_Uso").ToString
                    If fecha = "" Then
                        Me.TablaImportar.Item(Fechauso.Index, i).Value = Nothing
                    Else
                        Me.TablaImportar.Item(Fechauso.Index, i).Value = fecha.ToString.Substring(0, 10)
                    End If
                    fecha = ds.Tables(0).Rows(i)("Fecha_Adquisicion").ToString
                    If fecha = "" Then
                        Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value = Nothing
                    Else
                        Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value = fecha.ToString.Substring(0, 10)
                    End If
                    'Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("Fecha_Adquisicion")) = True, "", ds.Tables(0).Rows(i)("Fecha_Adquisicion").ToString.Substring(0, 10))
                    Me.TablaImportar.Item(Descrip.Index, i).Value = ds.Tables(0).Rows(i)("Descripcion")
                    Me.TablaImportar.Item(MOI.Index, i).Value = ds.Tables(0).Rows(i)("MOI")
                    Me.TablaImportar.Item(Tasa.Index, i).Value = ds.Tables(0).Rows(i)("Tasa")
                    Me.TablaImportar.Item(Dep.Index, i).Value = ds.Tables(0).Rows(i)("Dep_Acumulada_Anteriro")
                    Me.TablaImportar.Item(MesesDep.Index, i).Value = ds.Tables(0).Rows(i)("Meses_Depreciacion")
                    Me.TablaImportar.Item(DepEjercicio.Index, i).Value = ds.Tables(0).Rows(i)("Dep_Ejercicio")
                    Me.TablaImportar.Item(DepAcumuladaA.Index, i).Value = ds.Tables(0).Rows(i)("Dep_Acumulada_Actual")
                    Me.TablaImportar.Item(MontoFDep.Index, i).Value = ds.Tables(0).Rows(i)("Monto_Por_Depreciar")

                    fecha = ds.Tables(0).Rows(i)("Fecha_Baja").ToString
                    If fecha = "" Then
                        Me.TablaImportar.Item(Fecha_Baja.Index, i).Value = Nothing
                    Else
                        Me.TablaImportar.Item(Fecha_Baja.Index, i).Value = fecha.ToString.Substring(0, 10)
                    End If
                    'Me.TablaImportar.Item(Fecha_Baja.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("Fecha_Baja")) = True, "", ds.Tables(0).Rows(i)("Fecha_Baja").ToString.Substring(0, 10))
                    Me.TablaImportar.Item(DepContableFiscal.Index, i).Value = ds.Tables(0).Rows(i)("Dep_Contable_Fiscal")

                    fecha = ds.Tables(0).Rows(i)("INPC_Mes").ToString
                    If fecha = "" Then
                        Me.TablaImportar.Item(INPCMes.Index, i).Value = Nothing
                    Else
                        Me.TablaImportar.Item(INPCMes.Index, i).Value = fecha.ToString.Substring(0, 10)
                    End If
                    ' Me.TablaImportar.Item(INPCMes.Index, i).Value = ds.Tables(0).Rows(i)("INPC_Mes")
                    Me.TablaImportar.Item(INPCUM.Index, i).Value = ds.Tables(0).Rows(i)("INPC_Ultima_M")
                    Me.TablaImportar.Item(INPCMesA.Index, i).Value = ds.Tables(0).Rows(i)("INPC_Mes_Adq")
                    Me.TablaImportar.Item(DepActualizada.Index, i).Value = ds.Tables(0).Rows(i)("Dep_Actualizada")
                    Me.TablaImportar.Item(Ti.Index, i).Value = ds.Tables(0).Rows(i)("Tipo")
                    Me.TablaImportar.Item(Sumas.Index, i).Value = ds.Tables(0).Rows(i)("Sumas")
                    Me.TablaImportar.Item(FactorAct.Index, i).Value = ds.Tables(0).Rows(i)("Factor_Actualizacion")
                    If (Me.TablaImportar.Item(Descrip.Index, i).Value <> Nothing Or Me.TablaImportar.Item(Descrip.Index, i).Value.ToString <> "") And Me.TablaImportar.Item(Cta.Index, i).Value Is Nothing And Me.TablaImportar.Item(Ti.Index, i).Value < 40 Then
                        Titulo = Me.TablaImportar.Rows(i)
                        Titulo.DefaultCellStyle = Negrita_verde
                    ElseIf (Me.TablaImportar.Item(Descrip.Index, i).Value <> Nothing Or Me.TablaImportar.Item(Descrip.Index, i).Value.ToString <> "") And Me.TablaImportar.Item(Cta.Index, i).Value Is Nothing And Me.TablaImportar.Item(Ti.Index, i).Value >= 40 Then
                        Titulo = Me.TablaImportar.Rows(i)
                        Titulo.DefaultCellStyle = Negrita_morado
                    End If

                    frm.Barra.Value = i
                Next
                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Me.Cursor = Cursors.Arrow

            End If
        Else
            If RadMessageBox.Show("No existe plantilla de la Empresa " & Me.lstCliente.SelectText & " para el año " & Anio & " deseas crearla?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Calcular()
                Guarda_Ctas()
            Else
                Exit Sub
            End If
        End If


    End Sub
    Private Function Ctas(ByVal Cliente As Integer)
        Dim C() As String
        Dim Datos As String = ""
        Dim sql As String = " Select  Ctas_MOI.Cta_Madre  from Ctas_MOI Where   Id_Empresa = " & Cliente & "   "
        Dim DS_Cuentas As DataSet = Eventos.Obtener_DS(sql)
        If DS_Cuentas.Tables(0).Rows.Count > 0 Then
            For a As Integer = 0 To DS_Cuentas.Tables(0).Rows.Count - 1
                If C Is Nothing Then
                    ReDim Preserve C(0)
                    C(UBound(C)) = DS_Cuentas.Tables(0).Rows(a)(0)
                Else
                    ReDim Preserve C(UBound(C) + 1)
                    C(UBound(C)) = DS_Cuentas.Tables(0).Rows(a)(0)
                End If

            Next
            Datos = String.Join(",", C)
        End If

        'C = String.Join(",", C)
        Return Datos
    End Function



    Private Function Ctas_Dep(ByVal Cliente As Integer, ByVal Clave As String)
        Dim C As String = ""
        Dim where As String = ""
        Dim Cuentas As New List(Of Cuentas_MOI)
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT CtasDep.Cta_Madre  FROM CtasDep WHERE Id_Clave = " & Clave & " AND Id_Empresa = " & Cliente & "")
        If ds.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1


                Dim cuenta As String = ds.Tables(0).Rows(0)(0).ToString.Replace("-", "")
                Select Case Len(cuenta)
                    Case 4
                        where = " Nivel1 = " & cuenta & " and nivel2 >= 0000 "
                    Case 8
                        where = " Nivel1 = " & cuenta.ToString.Substring(0, 4) & " and nivel2 = " & cuenta.ToString.Substring(4, 4) & " and Nivel3 >= 0000 "
                    Case 12
                        where = " Nivel1 = " & cuenta.ToString.Substring(0, 4) & " and nivel2 = " & cuenta.ToString.Substring(4, 4) & " and nivel3 = " & cuenta.ToString.Substring(8, 4) & " and Nivel4 >= 0000 "
                    Case 16
                        where = " Nivel1 = " & cuenta.ToString.Substring(0, 4) & " and nivel2 = " & cuenta.ToString.Substring(4, 4) & " and nivel3 = " & cuenta.ToString.Substring(8, 4) & " and Nivel4 >= " & cuenta.ToString.Substring(12, 4) & " "
                End Select

                Dim sql As String = " SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias  FROM Catalogo_de_Cuentas WHERE Id_Empresa = " & Cliente & " AND   " & where & " "
                Dim DS_Cuentas As DataSet = Eventos.Obtener_DS(sql)
                If DS_Cuentas.Tables(0).Rows.Count > 0 Then
                    For a As Integer = 0 To DS_Cuentas.Tables(0).Rows.Count - 1
                        Cuentas.Add(New Cuentas_MOI() With {.Cuenta = Trim(DS_Cuentas.Tables(0).Rows(a)(0)).ToString})
                    Next
                End If

            Next
        End If
        Return Cuentas
    End Function

    Private Sub Calcular()
        Negrita_verde.Font = New Font(Me.TablaImportar.Font, FontStyle.Bold)
        Negrita_verde.BackColor = Color.LawnGreen
        Negrita_morado.Font = New Font(Me.TablaImportar.Font, FontStyle.Bold)
        Negrita_morado.BackColor = Color.Khaki
        Dim Tpos As New List(Of Tipos)
        Dim Tipos_S As New List(Of Tipo_Suma)
        Dim Titulo As DataGridViewRow
        Dim Posicion As Integer = 0
        ' Dim Cuentas As DataSet = Eventos.Obtener_DS("  SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias   From Catalogo_de_Cuentas Where Clasificacion ='AFI' AND Id_Empresa = " & Me.lstCliente.SelectItem & " AND (RFc <>'NULL' AND RFC <> '' AND RFC IS NOT NULL ) order by Alias ")
        Dim Cuentas As DataSet = Eventos.Obtener_DS("  SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias   From Catalogo_de_Cuentas Where  Id_Empresa = " & Me.lstCliente.SelectItem & " AND Nivel1 IN (" & Ctas(Me.lstCliente.SelectItem) & " ) order by Alias ")
        If Cuentas.Tables(0).Rows.Count > 0 Then
            If Me.Cta.Items.Count = 0 Then
                For i As Integer = 0 To Cuentas.Tables(0).Rows.Count - 1
                    Me.Cta.Items.Add(Trim(Cuentas.Tables(0).Rows(i)("Alias")))

                Next
            Else
                Me.Cta.Items.Clear()
                For i As Integer = 0 To Cuentas.Tables(0).Rows.Count - 1
                    Me.Cta.Items.Add(Trim(Cuentas.Tables(0).Rows(i)("Alias")))

                Next
            End If
        End If
        Dim ds_Tipos As DataSet = Eventos.Obtener_DS("SELECT DISTINCT   Clave_Activos.Id_Clave FROM     Ctas_Activo_Cliente 
                        INNER JOIN Catalogo_de_Cuentas ON Ctas_Activo_Cliente.Id_cat_Cuentas = Catalogo_de_Cuentas.Id_cat_Cuentas 
                        INNER JOIN Tipo_Activos ON Tipo_Activos.Id_Tipo = Ctas_Activo_Cliente.Id_Tipo 
                        INNER JOIN Clave_Activos ON Tipo_Activos.Id_Clave = Clave_Activos.Id_Clave WHERE Ctas_Activo_Cliente.Id_Empresa =" & Me.lstCliente.SelectItem & "")
        For p As Integer = 0 To ds_Tipos.Tables(0).Rows.Count - 1
            Tipos_S.Add(New Tipo_Suma() With {.Tipo = ds_Tipos.Tables(0).Rows(p)("Id_Clave")})
        Next
        For Each T In Tipos_S ' Se buscan los tipos
            Dim Sql As String = "SELECT DISTINCT  Tipo_Activos.Id_Tipo,Tipo_Activos.Descripcion AS Ti
                                FROM     Ctas_Activo_Cliente 
                                INNER JOIN Catalogo_de_Cuentas ON Ctas_Activo_Cliente.Id_cat_Cuentas = Catalogo_de_Cuentas.Id_cat_Cuentas 
                                INNER JOIN Tipo_Activos ON Tipo_Activos.Id_Tipo = Ctas_Activo_Cliente.Id_Tipo 
                                INNER JOIN Clave_Activos ON Tipo_Activos.Id_Clave = Clave_Activos.Id_Clave WHERE Ctas_Activo_Cliente.Id_Empresa =" & Me.lstCliente.SelectItem & " and  Clave_Activos.Id_Clave = " & T.Tipo & ""
            Dim ds As DataSet = Eventos.Obtener_DS(Sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + ds.Tables(0).Rows.Count

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Titulo = Me.TablaImportar.Rows(Posicion)
                    Me.TablaImportar.Item(Descrip.Index, Posicion).Value = ds.Tables(0).Rows(i)("TI")
                    Me.TablaImportar.Item(Ti.Index, Posicion).Value = ds.Tables(0).Rows(i)("Id_Tipo")
                    Me.TablaImportar.Item(Sumas.Index, Posicion).Value = T.Tipo
                    Titulo.DefaultCellStyle = Negrita_verde
                    Posicion += 1

                    Sql = "  SELECT Catalogo_de_Cuentas.Nivel1 + '-' + Catalogo_de_Cuentas.Nivel2 + '-' + Catalogo_de_Cuentas.Nivel3 + '-' + Catalogo_de_Cuentas.Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion AS Alias,
                        Tipo_Activos.Tasa, Clave_Activos.Id_Clave,Clave_Activos.Descripcion,Tipo_Activos.Id_Tipo,Tipo_Activos.Descripcion AS Ti,Ctas_Activo_Cliente.Id_Res_dep1,Ctas_Activo_Cliente.Id_Res_dep2,Ctas_Activo_Cliente.Id_Res_dep3,Ctas_Activo_Cliente.Id_Res_dep4,Ctas_Activo_Cliente.Id_Cta_depAct
                        FROM     Ctas_Activo_Cliente 
                        INNER JOIN Catalogo_de_Cuentas ON Ctas_Activo_Cliente.Id_cat_Cuentas = Catalogo_de_Cuentas.Id_cat_Cuentas 
                        INNER JOIN Tipo_Activos ON Tipo_Activos.Id_Tipo = Ctas_Activo_Cliente.Id_Tipo 
                        INNER JOIN Clave_Activos ON Tipo_Activos.Id_Clave = Clave_Activos.Id_Clave WHERE Ctas_Activo_Cliente.Id_Empresa =" & Me.lstCliente.SelectItem & " and  Tipo_Activos.Id_Tipo = " & ds.Tables(0).Rows(i)("Id_Tipo") & ""
                    Dim ds2 As DataSet = Eventos.Obtener_DS(Sql)
                    If ds2.Tables(0).Rows.Count > 0 Then
                        Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + ds2.Tables(0).Rows.Count
                        For j As Integer = 0 To ds2.Tables(0).Rows.Count - 1

                            Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(Posicion)
                            Try
                                If ds2.Tables(0).Rows(j)("Alias") <> "" Then
                                    Fila.Cells(Cta.Index).Value = Me.Cta.Items(Obtener_Index(Trim(ds2.Tables(0).Rows(j)("Alias")), Me.Cta))
                                    Me.TablaImportar.Item(Dep.Index, Posicion).Value = Dep_Anterior(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, Fila.Cells(Cta.Index).Value.trim())
                                End If
                            Catch ex As Exception

                            End Try
                            Me.TablaImportar.Item(Tasa.Index, Posicion).Value = ds2.Tables(0).Rows(j)("Tasa")
                            Me.TablaImportar.Item(Ti.Index, Posicion).Value = ds2.Tables(0).Rows(j)("Id_Tipo")
                            Me.TablaImportar.Item(Sumas.Index, Posicion).Value = ds2.Tables(0).Rows(j)("Id_Clave")
                            Me.TablaImportar.Item(Cta_Dep.Index, Posicion).Value = ds2.Tables(0).Rows(j)("Id_Cta_depAct")

                            Me.TablaImportar.Item(Cta_Dep_1.Index, Posicion).Value = ds2.Tables(0).Rows(j)("Id_Res_dep1")
                            Me.TablaImportar.Item(Cta_Dep_2.Index, Posicion).Value = ds2.Tables(0).Rows(j)("Id_Res_dep2")
                            Me.TablaImportar.Item(Cta_Dep_3.Index, Posicion).Value = ds2.Tables(0).Rows(j)("Id_Res_dep3")
                            Me.TablaImportar.Item(Cta_Dep_4.Index, Posicion).Value = ds2.Tables(0).Rows(j)("Id_Res_dep4")
                            Posicion += 1
                        Next

                    End If

                Next

            End If
            Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + 3

            ' Se insertan Los resumenes
            Titulo = Me.TablaImportar.Rows(Posicion)
            Me.TablaImportar.Item(Descrip.Index, Posicion).Value = "Saldo Segun Auditoria"
            Me.TablaImportar.Item(Ti.Index, Posicion).Value = 40
            Me.TablaImportar.Item(Sumas.Index, Posicion).Value = T.Tipo
            Titulo.DefaultCellStyle = Negrita_morado
            Posicion += 1
            Me.TablaImportar.Item(Descrip.Index, Posicion).Value = "Saldo Segun Compañia"
            Me.TablaImportar.Item(Ti.Index, Posicion).Value = 41
            Me.TablaImportar.Item(Sumas.Index, Posicion).Value = T.Tipo
            Titulo = Me.TablaImportar.Rows(Posicion)
            Titulo.DefaultCellStyle = Negrita_morado
            Posicion += 1
            Me.TablaImportar.Item(Descrip.Index, Posicion).Value = "Diferencia"
            Me.TablaImportar.Item(Ti.Index, Posicion).Value = 42
            Me.TablaImportar.Item(Sumas.Index, Posicion).Value = T.Tipo
            Titulo = Me.TablaImportar.Rows(Posicion)
            Titulo.DefaultCellStyle = Negrita_morado
            Posicion += 1

        Next

    End Sub
    Private Sub CalculamesDep(ByVal i As Integer)
        Dim MI As Decimal = 0
        Dim MF As Decimal = 0
        Dim F As String = ""
        Dim dia As String = ""
        Try
            If Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString.Substring(6, 4) > Me.LstAnio.Text.Trim() Then
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("La fecha de Adquisicion es Mayor al Año procesado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Else

                If (Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value <> Nothing And Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString <> "") Then
                    If Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString.Substring(6, 4) = Me.LstAnio.Text.Trim() Then

                        If (Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value <> Nothing And Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString <> "") And (Me.TablaImportar.Item(Fecha_Baja.Index, i).Value Is Nothing Or IsDBNull(Me.TablaImportar.Item(Fecha_Baja.Index, i).Value) = True) Then

                            MI = 12 - (Val(Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString.Substring(3, 2)))
                            MF = 12 - Val(Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString.Substring(3, 2))
                            MF = MF + 1
                            MF = Math.Truncate(MF / 2)
                            MF = (MF - 1) + Val(Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString.Substring(3, 2))
                            dia = UltimoDiaDelMes(Me.LstAnio.Text, MF)
                            'F = Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString.Substring(0, 2) & "/" & IIf(Len(MF.ToString) = 1, "0" & MF, MF) & "/" & Me.LstAnio.Text.Trim()
                            F = dia
                        Else
                            If Me.TablaImportar.Item(Fecha_Baja.Index, i).Value.ToString.Substring(6, 4) = Me.LstAnio.Text.Trim() Then

                                MI = Val(Me.TablaImportar.Item(Fecha_Baja.Index, i).Value.ToString.Substring(3, 2))
                                MI = MI - (Val(Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString.Substring(3, 2)) + 1)

                                MF = Val(Me.TablaImportar.Item(Fecha_Baja.Index, i).Value.ToString.Substring(3, 2)) - Val(Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString.Substring(3, 2))
                                MF = MF + 1
                                MF = Math.Truncate(MF / 2)
                                MF = (MF - 1) + Val(Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString.Substring(3, 2))

                                dia = UltimoDiaDelMes(Me.LstAnio.Text, MF)

                                'F = Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString.Substring(0, 2) & "/" & IIf(Len(MF.ToString) = 1, "0" & MF, MF) & "/" & Me.LstAnio.Text.Trim()
                                F = dia
                            Else
                                RadMessageBox.SetThemeName("MaterialBlueGrey")
                                RadMessageBox.Show("El año de la fecha de baja no puede ser mayor al año procesado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                            End If
                        End If

                    ElseIf Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString.Substring(6, 4) < Me.LstAnio.Text.Trim() Then
                        If (Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value <> Nothing And Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString <> "") And (Me.TablaImportar.Item(Fecha_Baja.Index, i).Value Is Nothing Or IsDBNull(Me.TablaImportar.Item(Fecha_Baja.Index, i).Value) = True) Then
                            MI = 12
                            F = "30/06/" & Me.LstAnio.Text.Trim()
                            MF = "06"
                        Else
                            If Me.TablaImportar.Item(Fecha_Baja.Index, i).Value.ToString.Substring(6, 4) = Me.LstAnio.Text.Trim() Then

                                MI = Val(Me.TablaImportar.Item(Fecha_Baja.Index, i).Value.ToString.Substring(3, 2)) - 1
                                MF = Val(Me.TablaImportar.Item(Fecha_Baja.Index, i).Value.ToString.Substring(3, 2))
                                MF = Math.Truncate(MF / 2)
                                dia = UltimoDiaDelMes(Me.LstAnio.Text, MF)
                                'F = Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString.Substring(0, 2) & "/" & IIf(Len(MF.ToString) = 1, "0" & MF, MF) & "/" & Me.LstAnio.Text.Trim()
                                F = dia
                            Else
                                RadMessageBox.SetThemeName("MaterialBlueGrey")
                                RadMessageBox.Show("El año de la fecha de baja no puede ser mayor al año procesado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                            End If
                        End If
                    End If
                End If
            End If

            Me.TablaImportar.Item(MesesDep.Index, i).Value = MI
            Me.TablaImportar.Item(INPCMes.Index, i).Value = F
            Try
                Me.TablaImportar.Item(INPCUM.Index, i).Value = Eventos.ObtenerValorDB("INPC", "Importe", " datepart(month,fecha)=" & IIf(Len(MF.ToString) = 1, "0" & MF, MF) & " AND datepart(year,fecha)=" & Me.LstAnio.Text.Trim() & "", True)
            Catch ex As Exception
                Me.TablaImportar.Item(INPCUM.Index, i).Value = 0
            End Try
            Try
                Me.TablaImportar.Item(INPCMesA.Index, i).Value = Eventos.ObtenerValorDB("INPC", "Importe", " datepart(month,fecha)=" & Me.TablaImportar.Item(Fechauso.Index, i).Value.ToString.Substring(3, 2) & " AND datepart(year,fecha)=" & Me.TablaImportar.Item(Fechauso.Index, i).Value.ToString.Substring(6, 4) & "", True)
            Catch ex As Exception
                Me.TablaImportar.Item(INPCMesA.Index, i).Value = 0
            End Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub CalculaDepContableEjer(ByVal i As Integer)
        Dim Importe As Decimal = 0
        Dim M As Decimal = 0
        Try
            If (Me.TablaImportar.Item(MesesDep.Index, i).Value.ToString <> "" And Me.TablaImportar.Item(MesesDep.Index, i).Value <> Nothing And IsDBNull(Me.TablaImportar.Item(MesesDep.Index, i).Value) = False Or Me.TablaImportar.Item(MesesDep.Index, i).Value = 0) And (Me.TablaImportar.Item(MOI.Index, i).Value.ToString <> "" And Me.TablaImportar.Item(MOI.Index, i).Value <> Nothing) Then

                Dim Arreglo As Object = MaximoDepreciar(i, Me.TablaImportar.Item(Ti.Index, i).Value)

                If Arreglo(0) = True Then
                    If Convert.ToDecimal(Me.TablaImportar.Item(MOI.Index, Me.TablaImportar.CurrentRow.Index).Value) > Convert.ToDecimal(Arreglo(1)) Then


                        M = Arreglo(1)
                    End If
                End If



                M = Math.Round(((M * Me.TablaImportar.Item(Tasa.Index, i).Value / 12) * (Me.TablaImportar.Item(MesesDep.Index, i).Value)), 2)
                Importe = Math.Round(((Me.TablaImportar.Item(MOI.Index, i).Value * Me.TablaImportar.Item(Tasa.Index, i).Value / 12) * (Me.TablaImportar.Item(MesesDep.Index, i).Value)), 2)
                Me.TablaImportar.Item(DepEjercicio.Index, i).Value = Importe
                ' No se puede poner depreciacion acunmulada anterior si el activo se adquirio el mismo año 

                Me.TablaImportar.Item(DepAcumuladaA.Index, i).Value = IIf(Me.TablaImportar.Item(Dep.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(Dep.Index, i).Value) + Importe
                Me.TablaImportar.Item(MontoFDep.Index, i).Value = Me.TablaImportar.Item(MOI.Index, i).Value - Me.TablaImportar.Item(DepAcumuladaA.Index, i).Value
                'Me.TablaImportar.Item(DepContableFiscal.Index, i).Value = Importe
                If M = 0 Then
                    M = Importe
                End If
                Me.TablaImportar.Item(DepContableFiscal.Index, i).Value = M
                If Me.TablaImportar.Item(DepContableFiscal.Index, i).Value > 0 Then
                    Dim ImpInpc As Decimal = Me.TablaImportar.Item(INPCUM.Index, i).Value / Me.TablaImportar.Item(INPCMesA.Index, i).Value
                    Me.TablaImportar.Item(FactorAct.Index, i).Value = Math.Truncate((IIf(ImpInpc < 1, 1, ImpInpc)) * 10000) / 10000
                    Me.TablaImportar.Item(DepActualizada.Index, i).Value = Math.Truncate((Me.TablaImportar.Item(DepContableFiscal.Index, i).Value * Me.TablaImportar.Item(FactorAct.Index, i).Value) * 100) / 100
                Else
                    Me.TablaImportar.Item(FactorAct.Index, i).Value = 0
                    Me.TablaImportar.Item(DepActualizada.Index, i).Value = 0
                End If

            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Function Dep_Anterior(ByVal Anio As Integer, ByVal Cliente As Integer, ByVal Cuenta As String)
        Dim Importe As Decimal = 0
        Dim ds As DataSet = Eventos.Obtener_DS("Select sum(Depreciaciones.Dep_Acumulada_Actual) As Suma FROM Depreciaciones WHERE Anio < " & Anio & " And Id_Empresa = " & Cliente & " And Cuenta = '" & Cuenta & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            Importe = IIf(IsDBNull(ds.Tables(0).Rows(0)("Suma")) = True, 0, ds.Tables(0).Rows(0)("Suma"))
        End If
        Return Importe
    End Function
    Public Class Ctas_Tipo
        Public Property Cuenta As String
        Public Property Tipo As String
        Public Property Saldo As Decimal
    End Class
    Private Class Tipo_Suma
        Public Property Tipo As String

    End Class
    Private Class Cuentas_MOI
        Public Property Cuenta As String

    End Class
    Private Class Tipos
        Public Property Tipo As String
        Public Property Clave As Integer
        Public Property Descripcion As String

    End Class



    Private Function Calcuala_Saldo(ByVal Cta As String, ByVal Anio As Integer)
        Dim Saldo As Decimal = 0
        Return Saldo
    End Function

    Private Function Obtener_Index(ByVal valor As String, ByVal Col As DataGridViewComboBoxColumn)
        Dim Indice As Integer = -1
        Select Case Len(valor)
            Case 4
                valor = valor & "-0000-0000-0000"
            Case 9
                valor = valor & "-0000-0000"
            Case 14
                valor = valor & "-0000"
            Case 20
                valor = valor
        End Select
        For i As Integer = 0 To Col.Items.Count - 1
            If valor = Trim(Col.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next

        Return Indice
    End Function

    Private Sub Depreciacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_Listas()
        Dim i As Integer
        For i = DateTime.Now.Year To DateTime.Now.Year - 5 Step -1
            If i >= 2004 Then
                Me.LstAnio.Items.Add(Str(i))
            End If
        Next
        Me.LstAnio.Text = anio

        Me.ComboMes.Text = "01"
        If Len(m) < 2 Then
            m = "0" & m
        End If
        Me.ComboMes2.Text = m
        For Each Colum In Me.TablaImportar.Columns
            Eventos.AltoFilas(Colum.index, Me.TablaImportar)
            Colum.SortMode = DataGridViewColumnSortMode.NotSortable
        Next

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
        Eventos.DiseñoTabla(Me.TablaImportar)
        Eventos.DiseñoTabla(Me.TablaMensual)
        Eventos.DiseñoTabla(Me.TablaResumen)


    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub
    Function UltimoDiaDelMes(ByVal A As Integer, ByVal M As Integer) As Date
        UltimoDiaDelMes = DateSerial(A, M + 1, 0)
    End Function
    Private Sub TablaImportar_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles TablaImportar.CellEndEdit
        Dim i As Integer = Me.TablaImportar.CurrentRow.Index
        Dim MI As Decimal = 0
        Dim MF As Decimal = 0
        Dim F As String = ""
        Dim dia As String = ""
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Try
            If IsNumeric(Me.TablaImportar.Item(e.ColumnIndex, e.RowIndex).Value) Then
                Me.TablaImportar.Item(e.ColumnIndex, e.RowIndex).Value *= 1
            End If
            If Me.TablaImportar.Item(FechaAdquisicion.Index, Me.TablaImportar.CurrentRow.Index).Value.ToString.Substring(6, 4) = Me.LstAnio.Text.Trim() Then


                CalculamesDep(Me.TablaImportar.CurrentRow.Index)
                CalculaDepContableEjer(Me.TablaImportar.CurrentRow.Index)
                Dim Arreglo As Object = MaximoDepreciar(Me.TablaImportar.CurrentRow.Index, Me.TablaImportar.Item(Ti.Index, Me.TablaImportar.CurrentRow.Index).Value)

                If Arreglo(0) = True Then
                    If Convert.ToDecimal(Me.TablaImportar.Item(MOI.Index, Me.TablaImportar.CurrentRow.Index).Value) > Convert.ToDecimal(Arreglo(1)) Then

                        RadMessageBox.Show("El importe de MOI excede el monto maximo de Depreciacion...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)

                        'Me.TablaImportar.Item(MOI.Index, Me.TablaImportar.CurrentRow.Index).Value = Arreglo(1)
                    End If
                End If
                Calculaporcentajes(9999)

                If Convert.ToDecimal(Me.TablaImportar.Item(DepAcumuladaA.Index, Me.TablaImportar.CurrentRow.Index).Value) > Convert.ToDecimal(Me.TablaImportar.Item(MOI.Index, Me.TablaImportar.CurrentRow.Index).Value) Then
                    RadMessageBox.Show("No es posible asignar un monto por depreciar anterior ya que le año de uso es el mismo que se esta procesando...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                    Me.TablaImportar.Item(Dep.Index, Me.TablaImportar.CurrentRow.Index).Value = 0
                    CalculamesDep(Me.TablaImportar.CurrentRow.Index)
                    CalculaDepContableEjer(Me.TablaImportar.CurrentRow.Index)
                    Arreglo = MaximoDepreciar(Me.TablaImportar.CurrentRow.Index, Me.TablaImportar.Item(Ti.Index, Me.TablaImportar.CurrentRow.Index).Value)
                    If Arreglo(0) = True Then
                        If Convert.ToDecimal(Me.TablaImportar.Item(MOI.Index, Me.TablaImportar.CurrentRow.Index).Value) > Convert.ToDecimal(Arreglo(1)) Then
                            RadMessageBox.SetThemeName("MaterialBlueGrey")
                            RadMessageBox.Show("El importe de MOI excede el monto maximo de Depreciacion...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                            'Me.TablaImportar.Item(MOI.Index, Me.TablaImportar.CurrentRow.Index).Value = Arreglo(1)
                        End If
                    End If
                    Calculaporcentajes(9999)
                Else

                End If
            Else



                CalculamesDep(Me.TablaImportar.CurrentRow.Index)
                CalculaDepContableEjer(Me.TablaImportar.CurrentRow.Index)
                Dim Arreglo As Object = MaximoDepreciar(Me.TablaImportar.CurrentRow.Index, Me.TablaImportar.Item(Ti.Index, Me.TablaImportar.CurrentRow.Index).Value)

                If Arreglo(0) = True Then
                    If Convert.ToDecimal(Me.TablaImportar.Item(MOI.Index, Me.TablaImportar.CurrentRow.Index).Value) > Convert.ToDecimal(Arreglo(1)) Then

                        RadMessageBox.Show("El importe de MOI excede el monto maximo de Depreciacion...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)

                        'Me.TablaImportar.Item(MOI.Index, Me.TablaImportar.CurrentRow.Index).Value = Arreglo(1)
                    End If
                End If
                Calculaporcentajes(9999)




                If Convert.ToDecimal(Me.TablaImportar.Item(DepAcumuladaA.Index, Me.TablaImportar.CurrentRow.Index).Value) > Convert.ToDecimal(Me.TablaImportar.Item(MOI.Index, Me.TablaImportar.CurrentRow.Index).Value) Then
                    RadMessageBox.Show("El Importe de la Depreciacion Acumulada Actual  excede el monto del MOI...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)

                    Dim A As Decimal = Me.TablaImportar.Item(MOI.Index, Me.TablaImportar.CurrentRow.Index).Value - Me.TablaImportar.Item(Dep.Index, Me.TablaImportar.CurrentRow.Index).Value
                    Dim B As Decimal = (Me.TablaImportar.Item(MOI.Index, Me.TablaImportar.CurrentRow.Index).Value * Me.TablaImportar.Item(Tasa.Index, Me.TablaImportar.CurrentRow.Index).Value) / 12
                    Dim c As Integer = A / B
                    Me.TablaImportar.Item(MesesDep.Index, Me.TablaImportar.CurrentRow.Index).Value = c


                    If (Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value <> Nothing And Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString <> "") And (Me.TablaImportar.Item(Fecha_Baja.Index, i).Value Is Nothing Or IsDBNull(Me.TablaImportar.Item(Fecha_Baja.Index, i).Value) = True) Then
                        MF = IIf(Math.Truncate(c / 2) < 1, 1, Math.Truncate(c / 2))
                        dia = UltimoDiaDelMes(Me.LstAnio.Text, MF)
                        'F = Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString.Substring(0, 2) & "/" & IIf(Len(MF.ToString) = 1, "0" & MF, MF) & "/" & Me.LstAnio.Text.Trim()
                        F = dia
                    Else
                        If Me.TablaImportar.Item(Fecha_Baja.Index, i).Value.ToString.Substring(6, 4) = Me.LstAnio.Text.Trim() Then

                            MF = Val(Me.TablaImportar.Item(Fecha_Baja.Index, i).Value.ToString.Substring(3, 2)) - Val(Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString.Substring(3, 2))
                            MF = MF + 1
                            MF = Math.Truncate(MF / 2)
                            MF = (MF - 1) + Val(Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString.Substring(3, 2))
                            dia = UltimoDiaDelMes(Me.LstAnio.Text, MF)
                            'F = Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value.ToString.Substring(0, 2) & "/" & IIf(Len(MF.ToString) = 1, "0" & MF, MF) & "/" & Me.LstAnio.Text.Trim()
                            F = dia
                        Else
                            RadMessageBox.Show("El año de la fecha de baja no puede ser mayor al año procesado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                        End If
                    End If


                    Me.TablaImportar.Item(INPCMes.Index, i).Value = F
                    Try
                        Me.TablaImportar.Item(INPCUM.Index, i).Value = Eventos.ObtenerValorDB("INPC", "Importe", " datepart(month,fecha)=" & IIf(Len(MF.ToString) = 1, "0" & MF, MF) & " AND datepart(year,fecha)=" & Me.LstAnio.Text.Trim() & "", True)
                    Catch ex As Exception
                        Me.TablaImportar.Item(INPCUM.Index, i).Value = 0
                    End Try
                    Try
                        Me.TablaImportar.Item(INPCMesA.Index, i).Value = Eventos.ObtenerValorDB("INPC", "Importe", " datepart(month,fecha)=" & Me.TablaImportar.Item(Fechauso.Index, i).Value.ToString.Substring(3, 2) & " AND datepart(year,fecha)=" & Me.TablaImportar.Item(Fechauso.Index, i).Value.ToString.Substring(6, 4) & "", True)
                    Catch ex As Exception
                        Me.TablaImportar.Item(INPCMesA.Index, i).Value = 0
                    End Try
                    CalculaDepContableEjer(Me.TablaImportar.CurrentRow.Index)
                    Arreglo = MaximoDepreciar(Me.TablaImportar.CurrentRow.Index, Me.TablaImportar.Item(Ti.Index, Me.TablaImportar.CurrentRow.Index).Value)

                    If Arreglo(0) = True Then
                        If Convert.ToDecimal(Me.TablaImportar.Item(MOI.Index, Me.TablaImportar.CurrentRow.Index).Value) > Convert.ToDecimal(Arreglo(1)) Then

                            RadMessageBox.Show("El importe de MOI excede el monto maximo de Depreciacion...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)

                            'Me.TablaImportar.Item(MOI.Index, Me.TablaImportar.CurrentRow.Index).Value = Arreglo(1)
                        End If
                    End If
                    If Convert.ToDecimal(Me.TablaImportar.Item(DepAcumuladaA.Index, Me.TablaImportar.CurrentRow.Index).Value) > Convert.ToDecimal(Me.TablaImportar.Item(MOI.Index, Me.TablaImportar.CurrentRow.Index).Value) Then
                        RadMessageBox.Show("El Importe de la Depreciacion Acumulada Actual  excede el monto del MOI...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)

                    End If
                    Calculaporcentajes(9999)
                End If



            End If
        Catch ex As Exception

        End Try

    End Sub
    Public Function MaximoDepreciar(ByVal i As Integer, ByVal Tipo As Integer)
        Dim Arreglo() As String = {False, 0}
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Dep_Anual_Activos.Monto FROM Ctas_Activo_Cliente INNER JOIN Dep_Anual_Activos ON Dep_Anual_Activos.Id_Dep_Activos = Ctas_Activo_Cliente.Id_Dep_Activos WHERE Id_Tipo = " & Tipo & " and Anio = " & 2019 & "")
        If ds.Tables(0).Rows.Count > 0 Then
            Arreglo(0) = True
            Arreglo(1) = ds.Tables(0).Rows(0)("Monto")
        End If
        Return Arreglo
    End Function

    Private Sub CmdCalcular_Click(sender As Object, e As EventArgs) Handles CmdCalcular.Click
        Calcula_SaldoDepAcAntCIA()
        Calcula_SaldoDepAcActCIA()
        Calcula_SaldoDepEJER()
        Calcula_SaldosMOI()
        ResumenAnual()
        DesgloseMensual()
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs)
        If Me.TablaImportar.RowCount > 0 Then
            Me.TablaImportar.Rows.Insert(Me.TablaImportar.CurrentRow.Index + 1, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", Me.TablaImportar.Item(Ti.Index, Me.TablaImportar.CurrentRow.Index).Value.ToString)
        End If
    End Sub

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If Me.lstCliente.SelectText <> "" Then
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            If RadMessageBox.Show("Se actualizara la informacion de la Empresa  " & Me.lstCliente.SelectText & " para el año " & Me.LstAnio.Text.Trim() & " esto es correcto?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim Sql As String = "Delete From Depreciaciones where Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio  =" & Trim(Me.LstAnio.Text) & " "
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

    Private Sub Guarda_Ctas()
        Dim sql As String = ""
        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = Me.TablaImportar.RowCount - 1
        Me.Cursor = Cursors.AppStarting
        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1

            sql = "   INSERT INTO dbo.Depreciaciones (	 	Cuenta,	Folio,	Cta_Depreciacion,	Resultado_Dep_1,	Porc_Dep_1,	Resultado_Dep_2,	Porc_Dep_2,	Resultado_Dep_3,	Porc_Dep_3,	Resultado_Dep_4,	Porc_Dep_4,
	            Fecha_Uso,	Fecha_Adquisicion,	Descripcion,	MOI,	Tasa,	Dep_Acumulada_Anteriro,	Meses_Depreciacion,	Dep_Ejercicio,	Dep_Acumulada_Actual,	Monto_Por_Depreciar,	Fecha_Baja,
	            Dep_Contable_Fiscal,	INPC_Mes,	INPC_Ultima_M,	INPC_Mes_Adq,Factor_Actualizacion,	Dep_Actualizada,	Tipo,	Anio,	Id_Empresa,Sumas	) VALUES 	(	'" & IIf(IsDBNull(Me.TablaImportar.Item(Cta.Index, i).Value) = True Or Me.TablaImportar.Item(Cta.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(Cta.Index, i).Value) & "',
	            '" & IIf(IsDBNull(Me.TablaImportar.Item(Folio.Index, i).Value) = True Or Me.TablaImportar.Item(Folio.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(Folio.Index, i).Value) & "',
	            '" & IIf(IsDBNull(Me.TablaImportar.Item(Cta_Dep.Index, i).Value) = True Or Me.TablaImportar.Item(Cta_Dep.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(Cta_Dep.Index, i).Value) & "',
	            '" & IIf(IsDBNull(Me.TablaImportar.Item(Cta_Dep_1.Index, i).Value) = True Or Me.TablaImportar.Item(Cta_Dep_1.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(Cta_Dep_1.Index, i).Value) & "',
	            " & IIf(IsDBNull(Me.TablaImportar.Item(Pdep1.Index, i).Value) = True Or Me.TablaImportar.Item(Pdep1.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(Pdep1.Index, i).Value) & ",
	            '" & IIf(IsDBNull(Me.TablaImportar.Item(Cta_Dep_2.Index, i).Value) = True Or Me.TablaImportar.Item(Cta_Dep_2.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(Cta_Dep_2.Index, i).Value) & "',
	            " & IIf(IsDBNull(Me.TablaImportar.Item(Pdep2.Index, i).Value) = True Or Me.TablaImportar.Item(Pdep2.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(Pdep2.Index, i).Value) & ",
	            '" & IIf(IsDBNull(Me.TablaImportar.Item(Cta_Dep_3.Index, i).Value) = True Or Me.TablaImportar.Item(Cta_Dep_3.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(Cta_Dep_3.Index, i).Value) & "',
	            " & IIf(IsDBNull(Me.TablaImportar.Item(Pdep3.Index, i).Value) = True Or Me.TablaImportar.Item(Pdep3.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(Pdep3.Index, i).Value) & ",
	            '" & IIf(IsDBNull(Me.TablaImportar.Item(Cta_Dep_4.Index, i).Value) = True Or Me.TablaImportar.Item(Cta_Dep_4.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(Cta_Dep_4.Index, i).Value) & "',
	            " & IIf(IsDBNull(Me.TablaImportar.Item(Pdep4.Index, i).Value) = True Or Me.TablaImportar.Item(Pdep4.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(Pdep4.Index, i).Value) & ",
	            " & IIf(IsDBNull(Me.TablaImportar.Item(Fechauso.Index, i).Value) = True Or Me.TablaImportar.Item(Fechauso.Index, i).Value Is Nothing, "NULL", Eventos.Sql_hoy(Me.TablaImportar.Item(Fechauso.Index, i).Value)) & ",
	            " & IIf(IsDBNull(Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value) = True Or Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value Is Nothing, "NULL", Eventos.Sql_hoy(Me.TablaImportar.Item(FechaAdquisicion.Index, i).Value)) & ",
	            '" & IIf(IsDBNull(Me.TablaImportar.Item(Descrip.Index, i).Value) = True Or Me.TablaImportar.Item(Descrip.Index, i).Value Is Nothing, "", Me.TablaImportar.Item(Descrip.Index, i).Value) & "',
	            " & IIf(IsDBNull(Me.TablaImportar.Item(MOI.Index, i).Value) = True Or Me.TablaImportar.Item(MOI.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(MOI.Index, i).Value) & ",
	            " & IIf(IsDBNull(Me.TablaImportar.Item(Tasa.Index, i).Value) = True Or Me.TablaImportar.Item(Tasa.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(Tasa.Index, i).Value) & ",
	            " & IIf(IsDBNull(Me.TablaImportar.Item(Dep.Index, i).Value) = True Or Me.TablaImportar.Item(Dep.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(Dep.Index, i).Value) & ",
	            " & IIf(IsDBNull(Me.TablaImportar.Item(MesesDep.Index, i).Value) = True Or Me.TablaImportar.Item(MesesDep.Index, i).Value Is Nothing, "NULL", Me.TablaImportar.Item(MesesDep.Index, i).Value) & ",
	            " & IIf(IsDBNull(Me.TablaImportar.Item(DepEjercicio.Index, i).Value) = True Or Me.TablaImportar.Item(DepEjercicio.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(DepEjercicio.Index, i).Value) & ",
	            " & IIf(IsDBNull(Me.TablaImportar.Item(DepAcumuladaA.Index, i).Value) = True Or Me.TablaImportar.Item(DepAcumuladaA.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(DepAcumuladaA.Index, i).Value) & ",
	            " & IIf(IsDBNull(Me.TablaImportar.Item(MontoFDep.Index, i).Value) = True Or Me.TablaImportar.Item(MontoFDep.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(MontoFDep.Index, i).Value) & ",
	            " & IIf(IsDBNull(Me.TablaImportar.Item(Fecha_Baja.Index, i).Value) = True Or Me.TablaImportar.Item(Fecha_Baja.Index, i).Value Is Nothing, "NULL", Eventos.Sql_hoy(Me.TablaImportar.Item(Fecha_Baja.Index, i).Value)) & ",
	            " & IIf(IsDBNull(Me.TablaImportar.Item(DepContableFiscal.Index, i).Value) = True Or Me.TablaImportar.Item(DepContableFiscal.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(DepContableFiscal.Index, i).Value) & ",
	            " & IIf(IsDBNull(Me.TablaImportar.Item(INPCMes.Index, i).Value) = True Or Me.TablaImportar.Item(INPCMes.Index, i).Value Is Nothing, "NULL", Eventos.Sql_hoy(Me.TablaImportar.Item(INPCMes.Index, i).Value)) & " ,
	            " & IIf(IsDBNull(Me.TablaImportar.Item(INPCUM.Index, i).Value) = True Or Me.TablaImportar.Item(INPCUM.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(INPCUM.Index, i).Value) & ",
	            " & IIf(IsDBNull(Me.TablaImportar.Item(INPCMesA.Index, i).Value) = True Or Me.TablaImportar.Item(INPCMesA.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(INPCMesA.Index, i).Value) & ",
                " & IIf(IsDBNull(Me.TablaImportar.Item(FactorAct.Index, i).Value) = True Or Me.TablaImportar.Item(FactorAct.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(FactorAct.Index, i).Value) & ",
	            " & IIf(IsDBNull(Me.TablaImportar.Item(DepActualizada.Index, i).Value) = True Or Me.TablaImportar.Item(DepActualizada.Index, i).Value Is Nothing, 0, Me.TablaImportar.Item(DepActualizada.Index, i).Value) & ",
	            " & IIf(IsDBNull(Me.TablaImportar.Item(Ti.Index, i).Value) = True, 0, Me.TablaImportar.Item(Ti.Index, i).Value) & " ,
	            " & Me.LstAnio.Text.Trim() & ",
	            " & Me.lstCliente.SelectItem & ",
                 " & IIf(IsDBNull(Me.TablaImportar.Item(Sumas.Index, i).Value) = True, 0, Me.TablaImportar.Item(Sumas.Index, i).Value) & " 
	            )"
            If Eventos.Comando_sql(sql) = 0 Then
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("No se guardo la informacion error en " & Trim(Me.TablaImportar.Item(Descrip.Index, i).Value) & " verifique la Informacion ", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                sql = "Delete From Depreciaciones where Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio  =" & Trim(Me.LstAnio.Text) & " "
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



    Private Sub TablaImportar_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles TablaImportar.CellDoubleClick
        Try


            If Me.TablaImportar.CurrentCell.ColumnIndex = Cta_Dep.Index Then
                If Me.TablaImportar.Item(Cta.Index, Me.TablaImportar.CurrentRow.Index).Value <> Nothing Then
                    Dim Cuentas As New List(Of Cuentas_MOI)
                    Cuentas = Ctas_Dep(Me.lstCliente.SelectItem, Me.TablaImportar.Item(Sumas.Index, Me.TablaImportar.CurrentRow.Index).Value)
                    If Cuentas.Count > 0 Then
                        Dim Combo As New DataGridViewComboBoxCell
                        For Each C In Cuentas
                            Combo.Items.Add(C.Cuenta)
                            Combo.FlatStyle = FlatStyle.Flat
                        Next
                        Me.TablaImportar.Item(Cta_Dep.Index, Me.TablaImportar.CurrentRow.Index) = Combo.Clone
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function CtasMoi(ByVal Cliente As Integer, ByVal Clave As Integer)
        Dim C() As String
        Dim Datos As String = ""
        Dim sql As String = " Select  Ctas_MOI.Cta_Madre  from Ctas_MOI Where  Id_Clave = " & Clave & " AND Id_Empresa = " & Cliente & "   "
        Dim DS_Cuentas As DataSet = Eventos.Obtener_DS(sql)
        If DS_Cuentas.Tables(0).Rows.Count > 0 Then
            For a As Integer = 0 To DS_Cuentas.Tables(0).Rows.Count - 1
                If C Is Nothing Then
                    ReDim Preserve C(0)
                    C(UBound(C)) = DS_Cuentas.Tables(0).Rows(a)(0)
                Else
                    ReDim Preserve C(UBound(C) + 1)
                    C(UBound(C)) = DS_Cuentas.Tables(0).Rows(a)(0)
                End If

            Next
            Datos = String.Join(",", C)
        End If


        Return Datos
    End Function
    Private Function CtasDepCia(ByVal Cliente As Integer, ByVal Clave As Integer)
        Dim C() As String
        Dim Datos As String = ""
        Dim sql As String = " Select DISTINCT  CtasDep.Cta_Madre  from CtasDep Where  CtasDep.Id_Clave = " & Clave & " AND CtasDep.Id_Empresa = " & Cliente & "   "
        Dim DS_Cuentas As DataSet = Eventos.Obtener_DS(sql)
        If DS_Cuentas.Tables(0).Rows.Count > 0 Then
            For a As Integer = 0 To DS_Cuentas.Tables(0).Rows.Count - 1
                If C Is Nothing Then
                    ReDim Preserve C(0)
                    C(UBound(C)) = DS_Cuentas.Tables(0).Rows(a)(0).ToString.Replace("-", "").Trim().PadRight(16, "0")
                Else
                    ReDim Preserve C(UBound(C) + 1)
                    C(UBound(C)) = DS_Cuentas.Tables(0).Rows(a)(0).ToString.Replace("-", "").Trim().PadRight(16, "0")
                End If

            Next
            Datos = String.Join(",", C)
        End If


        Return Datos
    End Function
    Private Function DepEjer(ByVal Cliente As Integer, ByVal Clave As Integer)
        Dim C() As String
        Dim Datos As String = ""
        Dim sql As String = " SELECT * FROM (
                            SELECT DISTINCT  SUBSTRING(Ctas_Activo_Cliente.Id_Res_dep1,1,19) AS cuenta,Tipo_Activos.Id_Clave,Ctas_Activo_Cliente.Id_Empresa FROM Ctas_Activo_Cliente 
                            INNER JOIN Tipo_Activos ON Tipo_Activos.Id_Tipo =Ctas_Activo_Cliente.Id_Tipo 
                            INNER JOIN Clave_Activos ON Clave_Activos.Id_Clave = Tipo_Activos.Id_Clave 
 
                            UNION 
                            SELECT DISTINCT SUBSTRING(Ctas_Activo_Cliente.Id_Res_dep2,1,19) AS cuenta,Tipo_Activos.Id_Clave,Ctas_Activo_Cliente.Id_Empresa FROM Ctas_Activo_Cliente 
                            INNER JOIN Tipo_Activos ON Tipo_Activos.Id_Tipo =Ctas_Activo_Cliente.Id_Tipo 
                            INNER JOIN Clave_Activos ON Clave_Activos.Id_Clave = Tipo_Activos.Id_Clave 
                            UNION
                            SELECT DISTINCT SUBSTRING(Ctas_Activo_Cliente.Id_Res_dep3,1,19) AS cuenta,Tipo_Activos.Id_Clave,Ctas_Activo_Cliente.Id_Empresa FROM Ctas_Activo_Cliente 
                            INNER JOIN Tipo_Activos ON Tipo_Activos.Id_Tipo =Ctas_Activo_Cliente.Id_Tipo 
                            INNER JOIN Clave_Activos ON Clave_Activos.Id_Clave = Tipo_Activos.Id_Clave 
                            UNION
                            SELECT DISTINCT SUBSTRING(Ctas_Activo_Cliente.Id_Res_dep4,1,19) AS cuenta,Tipo_Activos.Id_Clave,Ctas_Activo_Cliente.Id_Empresa FROM Ctas_Activo_Cliente 
                            INNER JOIN Tipo_Activos ON Tipo_Activos.Id_Tipo =Ctas_Activo_Cliente.Id_Tipo 
                            INNER JOIN Clave_Activos ON Clave_Activos.Id_Clave = Tipo_Activos.Id_Clave 
                            ) AS tabla WHERE (Cuenta <> '' And  Cuenta IS NOT NULL )  AND  Id_Clave = " & Clave & " AND  Id_Empresa = " & Cliente & "   "
        Dim DS_Cuentas As DataSet = Eventos.Obtener_DS(sql)
        If DS_Cuentas.Tables(0).Rows.Count > 0 Then
            For a As Integer = 0 To DS_Cuentas.Tables(0).Rows.Count - 1
                If C Is Nothing Then
                    ReDim Preserve C(0)
                    C(UBound(C)) = DS_Cuentas.Tables(0).Rows(a)(0).ToString.Replace("-", "").Trim().PadRight(16, "0")
                Else
                    ReDim Preserve C(UBound(C) + 1)
                    C(UBound(C)) = DS_Cuentas.Tables(0).Rows(a)(0).ToString.Replace("-", "").Trim().PadRight(16, "0")
                End If

            Next
            Datos = String.Join(",", C)
        End If


        Return Datos
    End Function
    Private Sub Calcula_SaldosMOI()
        Dim Saldo_Inicial As Decimal = 0
        Dim Saldo As Decimal = 0
        Dim Sql As String = ""
        If Me.TablaImportar.Rows.Count > 0 Then
            Dim where As String = ""
            For j As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                If Me.TablaImportar.Item(Ti.Index, j).Value = 40 Then
                    Dim Importe As Decimal = 0
                    Dim ImpEjer As Decimal = 0
                    Dim Depant As Decimal = 0
                    Dim Depact As Decimal = 0
                    Dim montodp As Decimal = 0
                    Dim DepCF As Decimal = 0
                    Dim DepA As Decimal = 0
                    For s As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                        If Me.TablaImportar.Item(Sumas.Index, j).Value = Me.TablaImportar.Item(Sumas.Index, s).Value And Me.TablaImportar.Item(Ti.Index, s).Value < 40 Then
                            Importe = Importe + Me.TablaImportar.Item(MOI.Index, s).Value
                            Depact = Depact + Me.TablaImportar.Item(DepAcumuladaA.Index, s).Value
                            Depant = Depant + Me.TablaImportar.Item(Dep.Index, s).Value
                            ImpEjer = ImpEjer + Me.TablaImportar.Item(DepEjercicio.Index, s).Value
                            montodp = montodp + Me.TablaImportar.Item(MontoFDep.Index, s).Value
                            DepCF = DepCF + Me.TablaImportar.Item(DepContableFiscal.Index, s).Value
                            DepA = DepA + Me.TablaImportar.Item(DepActualizada.Index, s).Value
                        End If
                    Next
                    Me.TablaImportar.Item(MOI.Index, j).Value = Importe
                    Me.TablaImportar.Item(DepAcumuladaA.Index, j).Value = Depact
                    Me.TablaImportar.Item(Dep.Index, j).Value = Depant
                    Me.TablaImportar.Item(DepEjercicio.Index, j).Value = ImpEjer
                    Me.TablaImportar.Item(MontoFDep.Index, j).Value = montodp
                    Me.TablaImportar.Item(DepContableFiscal.Index, j).Value = DepCF
                    Me.TablaImportar.Item(DepActualizada.Index, j).Value = DepA
                End If
                If Me.TablaImportar.Item(Ti.Index, j).Value = 41 Then
                    where = CtasMoi(Me.lstCliente.SelectItem, Me.TablaImportar.Item(Sumas.Index, j).Value)

                    Sql = "SELECT  CASE WHEN Naturaleza = 'D' THEN SUM(cargos - abonos) WHEN Naturaleza = 'A' THEN Sum(abonos - cargos) END AS Saldo   FROM (
                        SELECT Detalle_Polizas.Cuenta ,Catalogo_de_Cuentas.Naturaleza,sum(cargo ) AS Cargos,sum (abono )AS Abonos FROM Detalle_Polizas 
                        INNER JOIN Polizas ON Polizas.ID_poliza = Detalle_Polizas.ID_poliza
                        INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                        INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Cuenta = Detalle_Polizas.Cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa 
                        WHERE Polizas.Id_Empresa= " & Me.lstCliente.SelectItem & " AND  Catalogo_de_Cuentas.Nivel1 IN (" & where & ") AND   Polizas.Concepto = 'Poliza Cierre' AND  Polizas.Id_Anio =" & Convert.ToInt32(Me.LstAnio.Text.Trim()) - 1 & "
                        GROUP BY Detalle_Polizas.Cuenta ,Catalogo_de_Cuentas.Naturaleza
                        ) AS Tabla_Saldos GROUP BY NATURALEZA"
                    Try
                        Saldo_Inicial = Eventos.Obtener_DS(Sql).Tables(0).Rows(0)(0)
                    Catch ex As Exception
                        Saldo_Inicial = 0
                    End Try

                    'INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                    Sql = "SELECT sum (total) AS total FROM (SELECT SUM(Detalle_Polizas.Cargo - Detalle_Polizas.Abono ) AS Total, Detalle_Polizas.cuenta, Catalogo_de_Cuentas.Descripcion
                                     FROM Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                     
                                     INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                     INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                     WHERE (Detalle_Polizas.Cargo <> 0 or Detalle_Polizas.Abono <> 0) 
                                     GROUP BY Polizas.Id_Empresa, Catalogo_de_Cuentas.Id_Empresa,Detalle_Polizas.cuenta, Catalogo_de_Cuentas.Descripcion, Polizas.Id_Anio, Polizas.Id_Mes,Catalogo_de_Cuentas.Nivel1, Catalogo_de_Cuentas.Nivel2,Catalogo_de_Cuentas.Nivel3,Catalogo_de_Cuentas.Nivel4   
                                     HAVING Polizas.Id_Anio  =" & Me.LstAnio.Text.Trim() & "    AND Catalogo_de_Cuentas.Nivel1 IN (" & where & ") and Polizas.Id_Empresa =" & Me.lstCliente.SelectItem & "  )As tabla  "
                    Try
                        Saldo = Eventos.Obtener_DS(Sql).Tables(0).Rows(0)(0)
                    Catch ex As Exception
                        Saldo = 0
                    End Try
                    Saldo = Saldo_Inicial + Saldo
                    'MOI
                    Me.TablaImportar.Item(MOI.Index, j).Value = Saldo

                End If

                If Me.TablaImportar.Item(Ti.Index, j).Value = 42 Then
                    Dim dif As Decimal = 0
                    For s As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                        If (Me.TablaImportar.Item(Sumas.Index, j).Value = Me.TablaImportar.Item(Sumas.Index, s).Value) And Me.TablaImportar.Item(Ti.Index, s).Value = 40 Then
                            dif = Me.TablaImportar.Item(MOI.Index, s).Value
                            Me.TablaImportar.Item(MOI.Index, j).Value = dif
                        ElseIf (Me.TablaImportar.Item(Sumas.Index, j).Value = Me.TablaImportar.Item(Sumas.Index, s).Value) And Me.TablaImportar.Item(Ti.Index, s).Value = 41 Then
                            Me.TablaImportar.Item(MOI.Index, j).Value = dif - Me.TablaImportar.Item(MOI.Index, s).Value
                            dif = 0
                        End If
                    Next
                End If
            Next


        End If
        'Saldo_Inicial = Eventos.Calcula_Saldos_Iniciales(Cta.Substring(0, 16), Me.LstAnio.Text.Trim() - 1, Me.lstCliente.SelectItem)
    End Sub
    Private Sub Calcula_SaldoDepAcAntCIA()
        Dim Saldo_Inicial As Decimal = 0
        Dim Saldo As Decimal = 0
        Dim Sql As String = ""
        If Me.TablaImportar.Rows.Count > 0 Then
            Dim where As String = ""
            For j As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                If Me.TablaImportar.Item(Ti.Index, j).Value = 41 Then
                    where = CtasDepCia(Me.lstCliente.SelectItem, Me.TablaImportar.Item(Sumas.Index, j).Value)
                    If where <> "" Then


                        Sql = "SELECT  CASE WHEN Naturaleza = 'D' THEN SUM(cargos - abonos) WHEN Naturaleza = 'A' THEN Sum(abonos - cargos) END AS Saldo   FROM (
                        SELECT Detalle_Polizas.Cuenta ,Catalogo_de_Cuentas.Naturaleza,sum(cargo ) AS Cargos,sum (abono )AS Abonos FROM Detalle_Polizas 
                        INNER JOIN Polizas ON Polizas.ID_poliza = Detalle_Polizas.ID_poliza
                        INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                        INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Cuenta = Detalle_Polizas.Cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa 
                        WHERE Polizas.Id_Empresa= " & Me.lstCliente.SelectItem & " AND  Detalle_Polizas.Cuenta IN (" & where & ") AND   Polizas.Concepto = 'Poliza Cierre' AND  Polizas.Id_Anio =" & Convert.ToInt32(Me.LstAnio.Text.Trim()) - 1 & "
                        GROUP BY Detalle_Polizas.Cuenta ,Catalogo_de_Cuentas.Naturaleza
                        ) AS Tabla_Saldos GROUP BY NATURALEZA"
                        Try
                            Saldo_Inicial = Eventos.Obtener_DS(Sql).Tables(0).Rows(0)(0)
                        Catch ex As Exception
                            Saldo_Inicial = 0
                        End Try
                    End If
                    Me.TablaImportar.Item(Dep.Index, j).Value = Saldo_Inicial
                End If
            Next


        End If
    End Sub


    Private Sub Calcula_SaldoDepAcActCIA()
        Dim Saldo_Inicial As Decimal = 0
        Dim Saldo As Decimal = 0
        Dim Sql As String = ""
        If Me.TablaImportar.Rows.Count > 0 Then
            Dim where As String = ""
            For j As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                If Me.TablaImportar.Item(Ti.Index, j).Value = 41 Then
                    where = CtasDepCia(Me.lstCliente.SelectItem, Me.TablaImportar.Item(Sumas.Index, j).Value)
                    If where <> "" Then


                        Sql = "SELECT  CASE WHEN Naturaleza = 'D' THEN SUM(cargos - abonos) WHEN Naturaleza = 'A' THEN Sum(abonos - cargos) END AS Saldo   FROM (
                        SELECT Detalle_Polizas.Cuenta ,Catalogo_de_Cuentas.Naturaleza,sum(cargo ) AS Cargos,sum (abono )AS Abonos FROM Detalle_Polizas 
                        INNER JOIN Polizas ON Polizas.ID_poliza = Detalle_Polizas.ID_poliza
                        INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                        INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Cuenta = Detalle_Polizas.Cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa 
                        WHERE Polizas.Id_Empresa= " & Me.lstCliente.SelectItem & " AND  Detalle_Polizas.Cuenta IN (" & where & ") AND   Polizas.Concepto = 'Poliza Cierre' AND  Polizas.Id_Anio =" & Convert.ToInt32(Me.LstAnio.Text.Trim()) - 1 & "
                        GROUP BY Detalle_Polizas.Cuenta ,Catalogo_de_Cuentas.Naturaleza
                        ) AS Tabla_Saldos GROUP BY NATURALEZA"
                        Try
                            Saldo_Inicial = Eventos.Obtener_DS(Sql).Tables(0).Rows(0)(0)
                        Catch ex As Exception
                            Saldo_Inicial = 0
                        End Try

                        'INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                        Sql = "SELECT sum (total) AS total FROM (SELECT SUM(Detalle_Polizas.Cargo ) AS Total, Detalle_Polizas.cuenta, Catalogo_de_Cuentas.Descripcion
                                     FROM Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                     
                                     INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                     INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                     WHERE (Detalle_Polizas.Cargo <> 0 ) 
                                     GROUP BY Polizas.Id_Empresa, Catalogo_de_Cuentas.Id_Empresa,Detalle_Polizas.cuenta, Catalogo_de_Cuentas.Descripcion, Polizas.Id_Anio, Polizas.Id_Mes,Catalogo_de_Cuentas.Nivel1, Catalogo_de_Cuentas.Nivel2,Catalogo_de_Cuentas.Nivel3,Catalogo_de_Cuentas.Nivel4   
                                     HAVING Polizas.Id_Anio  =" & Me.LstAnio.Text.Trim() & "    AND Detalle_Polizas.Cuenta IN (" & where & ") and Polizas.Id_Empresa =" & Me.lstCliente.SelectItem & "  )As tabla  "
                        Try
                            Saldo = Eventos.Obtener_DS(Sql).Tables(0).Rows(0)(0)
                        Catch ex As Exception
                            Saldo = 0
                        End Try
                    End If
                    Saldo = Saldo_Inicial + Saldo
                    'DEP aCTUAL

                    Me.TablaImportar.Item(DepAcumuladaA.Index, j).Value = Saldo
                End If
            Next


        End If
    End Sub
    Private Sub Calcula_SaldoDepEJER()
        Dim Saldo_Inicial As Decimal = 0
        Dim Saldo As Decimal = 0
        Dim Sql As String = ""
        If Me.TablaImportar.Rows.Count > 0 Then
            Dim where As String = ""
            For j As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                If Me.TablaImportar.Item(Ti.Index, j).Value = 41 Then
                    where = DepEjer(Me.lstCliente.SelectItem, Me.TablaImportar.Item(Sumas.Index, j).Value)



                    'INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                    Sql = "SELECT sum (total) AS total FROM (SELECT SUM(Detalle_Polizas.Cargo ) AS Total, Detalle_Polizas.cuenta, Catalogo_de_Cuentas.Descripcion
                                     FROM Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                     
                                     INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                     INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                     WHERE (Detalle_Polizas.Cargo <> 0 ) 
                                     GROUP BY Polizas.Id_Empresa, Catalogo_de_Cuentas.Id_Empresa,Detalle_Polizas.cuenta, Catalogo_de_Cuentas.Descripcion, Polizas.Id_Anio, Polizas.Id_Mes,Catalogo_de_Cuentas.Nivel1, Catalogo_de_Cuentas.Nivel2,Catalogo_de_Cuentas.Nivel3,Catalogo_de_Cuentas.Nivel4   
                                     HAVING Polizas.Id_Anio  =" & Me.LstAnio.Text.Trim() & "    AND Detalle_Polizas.Cuenta IN (" & where & ") and Polizas.Id_Empresa =" & Me.lstCliente.SelectItem & "  )As tabla  "
                    Try
                        Saldo = Eventos.Obtener_DS(Sql).Tables(0).Rows(0)(0)
                    Catch ex As Exception
                        Saldo = 0
                    End Try
                    Saldo = Saldo
                    'DEP aCTUAL

                    Me.TablaImportar.Item(DepEjercicio.Index, j).Value = Saldo
                End If
            Next


        End If
    End Sub

    Private Sub ResumenAnual()
        Dim Titulos As New List(Of Titulos_Resumen)
        Dim Titulo As DataGridViewRow
        Dim Importe As Decimal = 0
        Dim ImpEjer As Decimal = 0
        Dim Depant As Decimal = 0
        Dim DepActl As Decimal = 0
        Dim Depact As Decimal = 0
        Dim montodp As Decimal = 0
        Dim DepCF As Decimal = 0
        Dim Posicion As Integer = 0
        For j As Integer = 0 To Me.TablaImportar.Rows.Count - 1
            Titulo = Me.TablaImportar.Rows(j)
            If Titulo.DefaultCellStyle.BackColor = Color.LawnGreen Then
                Titulos.Add(New Titulos_Resumen() With {.Titulo = Me.TablaImportar.Item(Descrip.Index, j).Value.ToString.Trim(), .Tipo = Me.TablaImportar.Item(Ti.Index, j).Value})
            End If
        Next
        Me.TablaResumen.RowCount = Titulos.Count + 2

        For Each Titul In Titulos
            Importe = 0
            ImpEjer = 0
            Depant = 0
            DepActl = 0
            Depact = 0
            montodp = 0
            DepCF = 0

            Me.TablaResumen.Item(DescripR.Index, Posicion).Value = Titul.Titulo
            For j As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                If Me.TablaImportar.Item(Ti.Index, j).Value = Titul.Tipo Then
                    Importe = Importe + Me.TablaImportar.Item(MOI.Index, j).Value
                    Depact = Depact + Me.TablaImportar.Item(DepAcumuladaA.Index, j).Value
                    Depant = Depant + Me.TablaImportar.Item(Dep.Index, j).Value
                    ImpEjer = ImpEjer + Me.TablaImportar.Item(DepEjercicio.Index, j).Value
                    montodp = montodp + Me.TablaImportar.Item(MontoFDep.Index, j).Value
                    DepCF = DepCF + Me.TablaImportar.Item(DepContableFiscal.Index, j).Value
                    DepActl = DepActl + Me.TablaImportar.Item(DepActualizada.Index, j).Value
                End If
            Next
            Me.TablaResumen.Item(MoiR.Index, Posicion).Value = Importe
            Me.TablaResumen.Item(DepAcActR.Index, Posicion).Value = Depact
            Me.TablaResumen.Item(DepEjercR.Index, Posicion).Value = ImpEjer
            Me.TablaResumen.Item(DepAcAntR.Index, Posicion).Value = Depant
            Me.TablaResumen.Item(DepActualizadaR.Index, Posicion).Value = DepActl
            Me.TablaResumen.Item(MontoR.Index, Posicion).Value = montodp
            Me.TablaResumen.Item(DepContaFiscalR.Index, Posicion).Value = DepCF
            Posicion += 1
        Next
        Dim Sal As Decimal = 0
        Dim dif As Decimal = 0
        Me.TablaResumen.Item(DescripR.Index, Posicion).Value = "Total"

        For j As Integer = 1 To Me.TablaResumen.Columns.Count - 1
            For i As Integer = 0 To Me.TablaResumen.Rows.Count - 3
                Sal += Me.TablaResumen.Item(j, i).Value
            Next
            Me.TablaResumen.Item(j, Posicion).Value = Sal
            Sal = 0
        Next
        Posicion += 1

        Me.TablaResumen.Item(DescripR.Index, Posicion).Value = "Diferencia"

        Importe = 0
        ImpEjer = 0
        Depant = 0
        DepActl = 0
        Depact = 0
        montodp = 0
        DepCF = 0
        For j As Integer = 0 To Me.TablaImportar.Rows.Count - 1
            If Me.TablaImportar.Item(Ti.Index, j).Value = 40 Then
                Importe = Importe + Me.TablaImportar.Item(MOI.Index, j).Value
                Depact = Depact + Me.TablaImportar.Item(DepAcumuladaA.Index, j).Value
                Depant = Depant + Me.TablaImportar.Item(Dep.Index, j).Value
                ImpEjer = ImpEjer + Me.TablaImportar.Item(DepEjercicio.Index, j).Value
                montodp = montodp + Me.TablaImportar.Item(MontoFDep.Index, j).Value
                DepCF = DepCF + Me.TablaImportar.Item(DepContableFiscal.Index, j).Value
                DepActl = DepActl + Me.TablaImportar.Item(DepActualizada.Index, j).Value
            End If
        Next
        Me.TablaResumen.Item(MoiR.Index, Posicion).Value = Importe - Me.TablaResumen.Item(MoiR.Index, Posicion - 1).Value
        Me.TablaResumen.Item(DepAcActR.Index, Posicion).Value = Depact - Me.TablaResumen.Item(DepAcActR.Index, Posicion - 1).Value
        Me.TablaResumen.Item(DepEjercR.Index, Posicion).Value = ImpEjer - Me.TablaResumen.Item(DepEjercR.Index, Posicion - 1).Value
        Me.TablaResumen.Item(DepAcAntR.Index, Posicion).Value = Depant - Me.TablaResumen.Item(DepAcAntR.Index, Posicion - 1).Value
        Me.TablaResumen.Item(DepActualizadaR.Index, Posicion).Value = DepActl - Me.TablaResumen.Item(DepActualizadaR.Index, Posicion - 1).Value
        Me.TablaResumen.Item(MontoR.Index, Posicion).Value = montodp - Me.TablaResumen.Item(MontoR.Index, Posicion - 1).Value
        Me.TablaResumen.Item(DepContaFiscalR.Index, Posicion).Value = DepCF - Me.TablaResumen.Item(DepContaFiscalR.Index, Posicion - 1).Value
        Posicion += 1
    End Sub

    Private Class Titulos_Resumen
        Public Property Titulo As String
        Public Property Tipo As Integer

    End Class

    Private Class Renglones_Mensual
        Public Property Descripcion As String
        Public Property Titulo As String
        Public Property Importe As Integer
        Public Property An As Integer
        Public Property Mes As Integer
        Public Property MesI As Integer
        Public Property MesF As Integer
        Public Property Baja As Integer = 0
    End Class
    Private Sub DesgloseMensual()
        If Me.TablaMensual.RowCount > 0 Then
            Me.TablaMensual.Rows.Clear()
        End If
        Negrita_verde.Font = New Font(Me.TablaImportar.Font, FontStyle.Bold)
        Negrita_verde.BackColor = Color.LawnGreen
        Dim Titulo As DataGridViewRow
        Dim Resumen As New List(Of Renglones_Mensual)
        Dim Lugar As Integer = 0
        For j As Integer = 0 To Me.TablaImportar.Rows.Count - 1
            Titulo = Me.TablaImportar.Rows(j)
            If Titulo.DefaultCellStyle.BackColor <> Color.Khaki Then
                If Titulo.DefaultCellStyle.BackColor = Color.LawnGreen Then
                    'Inserto Titulos
                    Resumen.Add(New Renglones_Mensual() With {.An = 0, .Mes = 0, .Titulo = IIf(Titulo.DefaultCellStyle.BackColor = Color.LawnGreen, "Si", "No").ToString.Trim(), .Descripcion = Me.TablaImportar.Item(Descrip.Index, j).Value, .Importe = Me.TablaImportar.Item(DepEjercicio.Index, j).Value})
                Else
                    'Insertar Registros
                    If Me.TablaImportar.Item(Fechauso.Index, j).Value IsNot Nothing Then
                        If Me.TablaImportar.Item(Fecha_Baja.Index, j).Value IsNot Nothing Then
                            If Me.TablaImportar.Item(Fechauso.Index, j).Value.ToString.Substring(6, 4).Trim() = Me.LstAnio.Text.Trim() Then
                                Resumen.Add(New Renglones_Mensual() With {.Baja = Me.TablaImportar.Item(Fechauso.Index, j).Value.ToString.Substring(3, 2).Trim(), .MesF = Me.TablaImportar.Item(MesesDep.Index, j).Value - 1, .MesI = Me.TablaImportar.Item(MesesDep.Index, j).Value + 1, .An = Me.TablaImportar.Item(FechaAdquisicion.Index, j).Value.ToString.Substring(6, 4).Trim(), .Mes = Me.TablaImportar.Item(MesesDep.Index, j).Value, .Titulo = IIf(Titulo.DefaultCellStyle.BackColor = Color.LawnGreen, "Si", "No").ToString.Trim(), .Descripcion = Me.TablaImportar.Item(Cta_Dep.Index, j).Value, .Importe = Me.TablaImportar.Item(DepEjercicio.Index, j).Value})

                            Else
                                Resumen.Add(New Renglones_Mensual() With {.Baja = Me.TablaImportar.Item(Fechauso.Index, j).Value.ToString.Substring(3, 2).Trim(), .MesF = Me.TablaImportar.Item(MesesDep.Index, j).Value - 1, .MesI = Me.TablaImportar.Item(MesesDep.Index, j).Value, .An = Me.TablaImportar.Item(FechaAdquisicion.Index, j).Value.ToString.Substring(6, 4).Trim(), .Mes = Me.TablaImportar.Item(MesesDep.Index, j).Value, .Titulo = IIf(Titulo.DefaultCellStyle.BackColor = Color.LawnGreen, "Si", "No").ToString.Trim(), .Descripcion = Me.TablaImportar.Item(Cta_Dep.Index, j).Value, .Importe = Me.TablaImportar.Item(DepEjercicio.Index, j).Value})

                            End If

                        Else
                            'If Me.TablaImportar.Item(Fechauso.Index, j).Value.ToString.Substring(6, 4).Trim() = Me.LstAnio.Text.Trim() Then
                            '    Resumen.Add(New Renglones_Mensual() With {.MesF = Me.TablaImportar.Item(MesesDep.Index, j).Value - 1, .MesI = Me.TablaImportar.Item(Fechauso.Index, j).Value.ToString.Substring(3, 2) + 1, .An = Me.TablaImportar.Item(FechaAdquisicion.Index, j).Value.ToString.Substring(6, 4).Trim(), .Mes = Me.TablaImportar.Item(MesesDep.Index, j).Value, .Titulo = IIf(Titulo.DefaultCellStyle.BackColor = Color.LawnGreen, "Si", "No").ToString.Trim(), .Descripcion = Me.TablaImportar.Item(Cta_Dep.Index, j).Value, .Importe = Me.TablaImportar.Item(DepEjercicio.Index, j).Value})
                            'Else
                            '    Resumen.Add(New Renglones_Mensual() With {.MesF = Me.TablaImportar.Item(MesesDep.Index, j).Value - 1, .MesI = Me.TablaImportar.Item(Fechauso.Index, j).Value.ToString.Substring(3, 2), .An = Me.TablaImportar.Item(FechaAdquisicion.Index, j).Value.ToString.Substring(6, 4).Trim(), .Mes = Me.TablaImportar.Item(MesesDep.Index, j).Value, .Titulo = IIf(Titulo.DefaultCellStyle.BackColor = Color.LawnGreen, "Si", "No").ToString.Trim(), .Descripcion = Me.TablaImportar.Item(Cta_Dep.Index, j).Value, .Importe = Me.TablaImportar.Item(DepEjercicio.Index, j).Value})

                            'End If
                            If Me.TablaImportar.Item(Fechauso.Index, j).Value.ToString.Substring(6, 4).Trim() = Me.LstAnio.Text.Trim() Then
                                Resumen.Add(New Renglones_Mensual() With {.MesF = Me.TablaImportar.Item(MesesDep.Index, j).Value - 1, .MesI = Me.TablaImportar.Item(Fechauso.Index, j).Value.ToString.Substring(3, 2) + 1, .An = Me.TablaImportar.Item(FechaAdquisicion.Index, j).Value.ToString.Substring(6, 4).Trim(), .Mes = Me.TablaImportar.Item(MesesDep.Index, j).Value, .Titulo = IIf(Titulo.DefaultCellStyle.BackColor = Color.LawnGreen, "Si", "No").ToString.Trim(), .Descripcion = Me.TablaImportar.Item(Cta_Dep.Index, j).Value, .Importe = Me.TablaImportar.Item(DepEjercicio.Index, j).Value})
                            Else
                                Resumen.Add(New Renglones_Mensual() With {.MesF = Me.ComboMes2.Text - 1, .MesI = Me.ComboMes.Text, .An = Me.LstAnio.Text, .Mes = Me.TablaImportar.Item(MesesDep.Index, j).Value, .Titulo = IIf(Titulo.DefaultCellStyle.BackColor = Color.LawnGreen, "Si", "No").ToString.Trim(), .Descripcion = Me.TablaImportar.Item(Cta_Dep.Index, j).Value, .Importe = Me.TablaImportar.Item(DepEjercicio.Index, j).Value})

                            End If

                        End If
                    End If
                End If

            End If
        Next
        Me.TablaMensual.RowCount = Resumen.Count
        For Each Item In Resumen

            Me.TablaMensual.Item(DescripcionMes.Index, Lugar).Value = Item.Descripcion
            Me.TablaMensual.Item(DepEj.Index, Lugar).Value = Item.Importe
            Titulo = Me.TablaMensual.Rows(Lugar)
            If Item.Titulo = "Si" Then
                Titulo.DefaultCellStyle = Negrita_verde
            End If
            Dim Inicio As Integer = 3
            Dim importe As Decimal = 0
            If Item.An = Me.LstAnio.Text.Trim() And Item.Baja = 0 Then

                For i As Integer = 3 To Me.TablaMensual.Columns.Count - 1
                    If Me.TablaMensual.Columns.Item(i).Name.ToString = Eventos.MesEnletra(IIf(Len(Item.MesI.ToString()) = 1, "0" & Item.MesI, Item.MesI)) Then
                        Inicio = i
                        Exit For
                    End If
                Next
                Try
                    importe = Item.Importe / (Item.Mes)
                Catch ex As Exception
                    importe = 0
                End Try

                For i As Integer = Inicio To Me.TablaMensual.Columns.Count - 1
                    Me.TablaMensual.Item(i, Lugar).Value = importe
                Next
                importe = 0
                For i As Integer = 3 To Me.TablaMensual.Columns.Count - 1
                    importe += Me.TablaMensual.Item(i, Lugar).Value
                Next
                Me.TablaMensual.Item(Diferencia.Index, Lugar).Value = Me.TablaMensual.Item(DepEj.Index, Lugar).Value - importe
            ElseIf Item.An < Me.LstAnio.Text.Trim() Then
                For i As Integer = 3 To Me.TablaMensual.Columns.Count - 1
                    If Me.TablaMensual.Columns.Item(i).Name.ToString = Eventos.MesEnletra(IIf(Len(Item.MesI.ToString()) = 1, "0" & Item.MesI, Item.MesI)) Then
                        Inicio = i
                        Exit For
                    End If
                Next
                Try
                    importe = Item.Importe / (Item.Mes)
                Catch ex As Exception
                    importe = 0
                End Try

                For i As Integer = Inicio To Me.TablaMensual.Columns.Count - 1
                    Me.TablaMensual.Item(i, Lugar).Value = importe
                Next
                importe = 0
                For i As Integer = 3 To Me.TablaMensual.Columns.Count - 1
                    importe += Me.TablaMensual.Item(i, Lugar).Value
                Next
                Me.TablaMensual.Item(Diferencia.Index, Lugar).Value = Me.TablaMensual.Item(DepEj.Index, Lugar).Value - importe
            End If



            Lugar += 1
        Next
    End Sub
    Private Function Calcula_poliza(ByVal Mes As String)
        Dim mess As String = IIf(Len(Mes) = 1, "0" & Mes, Mes)
        Dim poliza As String = Eventos.Num_poliza(Me.lstCliente.SelectItem, Checa_tipo("001", Me.lstCliente.SelectItem), Me.LstAnio.Text.Trim(), mess, Checa_tipo("001", Me.lstCliente.SelectItem))

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
    Private Sub CmdPoliza_Click(sender As Object, e As EventArgs) Handles CmdPoliza.Click


        For ia As Integer = Me.ComboMes.Text To Me.ComboMes2.Text

            Dim Fecha As Date = "01/" & IIf(Len(ia.ToString) = 1, "0" & ia, ia) & "/" & Me.LstAnio.Text.Trim()
            Dim Numpol As Integer = 0
            Dim item As Integer = 1
            Dim Cuenta As String = ""
            Dim Importe As Decimal = 0
            Dim poliza_Sistema As String = ""
            Dim Dia As String = Diasdelmes("01/" & IIf(Len(ia.ToString) = 1, "0" & ia, ia) & "/" & Me.LstAnio.Text.Trim())





            For j As Integer = 0 To Me.TablaMensual.Rows.Count - 1
                Importe += Val(Me.TablaMensual.Item(ia + 2, j).Value)
            Next

            If Importe > 0 Then
                poliza_Sistema = BuscarPoliza(IIf(Len(ia.ToString) = 1, "0" & ia, ia), Me.LstAnio.Text.Trim(), Checa_tipo("001", Me.lstCliente.SelectItem), "Depreciacion Contable " & IIf(Len(ia.ToString) = 1, "0" & ia, ia) & "/" & Me.LstAnio.Text.Trim()).trim()

                If poliza_Sistema <> "" Then
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    If RadMessageBox.Show("Ya existe una poliza de Depreciacion de Año " & Me.LstAnio.Text & "  y del mes " & Eventos.MesEnletra(IIf(Len(ia.ToString) = 1, "0" & ia, ia)) & " para " & Me.lstCliente.SelectText & " deseas Reemplazarla?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        If Comando_sql("DELETE FROM Detalle_Polizas WHERE ID_poliza = '" & poliza_Sistema & "'") > 0 Then

                            For j As Integer = 0 To Me.TablaMensual.Rows.Count - 1
                                If Val(Me.TablaMensual.Item(ia + 2, j).Value) > 0 Then

                                    For F As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                                        Dim Titulo As DataGridViewRow
                                        Titulo = Me.TablaImportar.Rows(F)
                                        If Titulo.DefaultCellStyle.BackColor <> Color.Khaki Then
                                            If Titulo.DefaultCellStyle.BackColor <> Color.LawnGreen Then
                                                Try

                                                    If Me.TablaImportar.Item(Cta_Dep.Index, F).Value.ToString.Substring(0, 19).Replace("-", "") = Me.TablaMensual.Item(DescripcionMes.Index, j).Value.ToString.Substring(0, 19).Replace("-", "") Then

                                                        Cuenta = Me.TablaMensual.Item(DescripcionMes.Index, j).Value.ToString.Substring(0, 19).Replace("-", "")
                                                        Crea_detalle_poliza(poliza_Sistema, item, 0, Me.TablaMensual.Item(ia + 2, j).Value, Cuenta, "")
                                                        item += 1
                                                        If Me.TablaImportar.Item(Cta_Dep_1.Index, F).Value.ToString <> "" Then
                                                            Cuenta = Me.TablaImportar.Item(Cta_Dep_1.Index, F).Value.ToString.Substring(0, 19).Replace("-", "")
                                                            Importe = Me.TablaMensual.Item(ia + 2, j).Value
                                                            Crea_detalle_poliza(poliza_Sistema, item, IIf(Me.TablaImportar.Item(Pdep1.Index, F).Value > 1, Importe * (Me.TablaImportar.Item(Pdep1.Index, F).Value / 100), Importe * Me.TablaImportar.Item(Pdep1.Index, F).Value), 0, Cuenta, "")
                                                            item += 1
                                                        End If
                                                        If Me.TablaImportar.Item(Cta_Dep_2.Index, F).Value.ToString <> "" Then
                                                            Cuenta = Me.TablaImportar.Item(Cta_Dep_2.Index, F).Value.ToString.Substring(0, 19).Replace("-", "")
                                                            Importe = Me.TablaMensual.Item(ia + 2, j).Value
                                                            Crea_detalle_poliza(poliza_Sistema, item, IIf(Me.TablaImportar.Item(Pdep2.Index, F).Value > 1, Importe * (Me.TablaImportar.Item(Pdep2.Index, F).Value / 100), Importe * Me.TablaImportar.Item(Pdep2.Index, F).Value), 0, Cuenta, "")
                                                            item += 1
                                                        End If
                                                        If Me.TablaImportar.Item(Cta_Dep_3.Index, F).Value.ToString <> "" Then
                                                            Cuenta = Me.TablaImportar.Item(Cta_Dep_3.Index, F).Value.ToString.Substring(0, 19).Replace("-", "")
                                                            Importe = Me.TablaMensual.Item(ia + 2, j).Value
                                                            Crea_detalle_poliza(poliza_Sistema, item, IIf(Me.TablaImportar.Item(Pdep3.Index, F).Value > 1, Importe * (Me.TablaImportar.Item(Pdep3.Index, F).Value / 100), Importe * Me.TablaImportar.Item(Pdep3.Index, F).Value), 0, Cuenta, "")
                                                            item += 1
                                                        End If
                                                        If Me.TablaImportar.Item(Cta_Dep_4.Index, F).Value.ToString <> "" Then
                                                            Cuenta = Me.TablaImportar.Item(Cta_Dep_4.Index, F).Value.ToString.Substring(0, 19).Replace("-", "")
                                                            Importe = Me.TablaMensual.Item(ia + 2, j).Value
                                                            Crea_detalle_poliza(poliza_Sistema, item, IIf(Me.TablaImportar.Item(Pdep4.Index, F).Value > 1, Importe * (Me.TablaImportar.Item(Pdep4.Index, F).Value / 100), Importe * Me.TablaImportar.Item(Pdep4.Index, F).Value), 0, Cuenta, "")
                                                            item += 1
                                                        End If
                                                    End If
                                                Catch ex As Exception

                                                End Try
                                            End If
                                        End If

                                    Next

                                End If
                            Next
                        End If
                    End If
                Else
                    poliza_Sistema = Calcula_poliza(ia)
                    Dim posi As Integer = InStr(1, poliza_Sistema, "-", CompareMethod.Binary)
                    Dim cuantos As Integer = Len(poliza_Sistema) - Len(poliza_Sistema.Substring(0, posi))
                    Dim consecutivo As Integer = Val(poliza_Sistema.Substring(posi, cuantos))
                    Dim ds As DataSet = Eventos.Obtener_DS("SELECt max (Num_Pol)+1 FROM Polizas WHERE Id_Empresa= " & Me.lstCliente.SelectItem & " AND id_Anio = '" & anio & "' AND id_Mes ='" & ia & "' and Id_Tipo_Pol_Sat = " & Checa_tipo("001", Me.lstCliente.SelectItem) & "")
                    If IsDBNull(ds.Tables(0).Rows(0)(0)) = True Then
                        Numpol = 1
                    Else
                        Numpol = ds.Tables(0).Rows(0)(0)
                    End If
                    If Creapoliza(poliza_Sistema, Me.LstAnio.Text.Trim(), IIf(Len(ia.ToString) = 1, "0" & ia, ia), Dia, consecutivo, Checa_tipo("001", Me.lstCliente.SelectItem), Fecha, "Depreciacion Contable " & IIf(Len(ia.ToString) = 1, "0" & ia, ia) & "/" & Me.LstAnio.Text.Trim(), "Dep", Numpol) = True Then
                        For j As Integer = 0 To Me.TablaMensual.Rows.Count - 1
                            If Val(Me.TablaMensual.Item(ia + 2, j).Value) > 0 Then


                                For F As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                                    Dim Titulo As DataGridViewRow
                                    Titulo = Me.TablaImportar.Rows(F)
                                    If Titulo.DefaultCellStyle.BackColor <> Color.Khaki Then
                                        If Titulo.DefaultCellStyle.BackColor <> Color.LawnGreen Then
                                            If Me.TablaImportar.Item(Cta_Dep.Index, F).Value.ToString.Substring(0, 19).Replace("-", "") = Me.TablaMensual.Item(DescripcionMes.Index, j).Value.ToString.Substring(0, 19).Replace("-", "") Then

                                                Cuenta = Me.TablaMensual.Item(DescripcionMes.Index, j).Value.ToString.Substring(0, 19).Replace("-", "")
                                                Crea_detalle_poliza(poliza_Sistema, item, 0, Me.TablaMensual.Item(ia + 2, j).Value, Cuenta, "")
                                                item += 1
                                                If Me.TablaImportar.Item(Cta_Dep_1.Index, F).Value.ToString <> "" Then
                                                    Cuenta = Me.TablaImportar.Item(Cta_Dep_1.Index, F).Value.ToString.Substring(0, 19).Replace("-", "")
                                                    Importe = Me.TablaMensual.Item(ia + 2, j).Value
                                                    Crea_detalle_poliza(poliza_Sistema, item, IIf(Me.TablaImportar.Item(Pdep1.Index, F).Value > 1, Importe * (Me.TablaImportar.Item(Pdep1.Index, F).Value / 100), Importe * Me.TablaImportar.Item(Pdep1.Index, F).Value), 0, Cuenta, "")
                                                    item += 1
                                                End If
                                                If Me.TablaImportar.Item(Cta_Dep_2.Index, F).Value.ToString <> "" Then
                                                    Cuenta = Me.TablaImportar.Item(Cta_Dep_2.Index, F).Value.ToString.Substring(0, 19).Replace("-", "")
                                                    Importe = Me.TablaMensual.Item(ia + 2, j).Value
                                                    Crea_detalle_poliza(poliza_Sistema, item, IIf(Me.TablaImportar.Item(Pdep2.Index, F).Value > 1, Importe * (Me.TablaImportar.Item(Pdep2.Index, F).Value / 100), Importe * Me.TablaImportar.Item(Pdep2.Index, F).Value), 0, Cuenta, "")
                                                    item += 1
                                                End If
                                                If Me.TablaImportar.Item(Cta_Dep_3.Index, F).Value.ToString <> "" Then
                                                    Cuenta = Me.TablaImportar.Item(Cta_Dep_3.Index, F).Value.ToString.Substring(0, 19).Replace("-", "")
                                                    Importe = Me.TablaMensual.Item(ia + 2, j).Value
                                                    Crea_detalle_poliza(poliza_Sistema, item, IIf(Me.TablaImportar.Item(Pdep3.Index, F).Value > 1, Importe * (Me.TablaImportar.Item(Pdep3.Index, F).Value / 100), Importe * Me.TablaImportar.Item(Pdep3.Index, F).Value), 0, Cuenta, "")
                                                    item += 1
                                                End If
                                                If Me.TablaImportar.Item(Cta_Dep_4.Index, F).Value.ToString <> "" Then
                                                    Cuenta = Me.TablaImportar.Item(Cta_Dep_4.Index, F).Value.ToString.Substring(0, 19).Replace("-", "")
                                                    Importe = Me.TablaMensual.Item(ia + 2, j).Value
                                                    Crea_detalle_poliza(poliza_Sistema, item, IIf(Me.TablaImportar.Item(Pdep4.Index, F).Value > 1, Importe * (Me.TablaImportar.Item(Pdep4.Index, F).Value / 100), Importe * Me.TablaImportar.Item(Pdep4.Index, F).Value), 0, Cuenta, "")
                                                    item += 1
                                                End If
                                            End If


                                        End If
                                    End If


                                Next

                            End If

                        Next
                    End If
                End If
            End If



        Next
        'Crear poliza
    End Sub



    Private Sub Calculaporcentajes(ByVal i As Integer)
        Dim Suma As Decimal = 0
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim Contador As Integer = 0
        If i = 9999 Then
            For P As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                If Val(Me.TablaImportar.Item(Cta.Index, P).Value) > 0 Then
                    Suma += IIf(Val(Me.TablaImportar.Item(Pdep1.Index, P).Value) > 1, Val(Me.TablaImportar.Item(Pdep1.Index, P).Value) / 100, Val(Me.TablaImportar.Item(Pdep1.Index, P).Value))
                    Suma += IIf(Val(Me.TablaImportar.Item(Pdep2.Index, P).Value) > 1, Val(Me.TablaImportar.Item(Pdep2.Index, P).Value) / 100, Val(Me.TablaImportar.Item(Pdep2.Index, P).Value))
                    Suma += IIf(Val(Me.TablaImportar.Item(Pdep3.Index, P).Value) > 1, Val(Me.TablaImportar.Item(Pdep3.Index, P).Value) / 100, Val(Me.TablaImportar.Item(Pdep3.Index, P).Value))
                    Suma += IIf(Val(Me.TablaImportar.Item(Pdep4.Index, P).Value) > 1, Val(Me.TablaImportar.Item(Pdep4.Index, P).Value) / 100, Val(Me.TablaImportar.Item(Pdep4.Index, P).Value))

                    If Suma > 1 Then
                        Contador += 1
                    ElseIf Suma < 1 Then
                        Contador += 1
                    End If
                End If

            Next
            If Contador >= 1 Then
                Me.CmdPoliza.Enabled = True
            Else
                Me.CmdPoliza.Enabled = False
            End If

        Else


            Suma += IIf(Val(Me.TablaImportar.Item(Pdep1.Index, i).Value) > 1, Val(Me.TablaImportar.Item(Pdep1.Index, i).Value) / 100, Val(Me.TablaImportar.Item(Pdep1.Index, i).Value))
            Suma += IIf(Val(Me.TablaImportar.Item(Pdep2.Index, i).Value) > 1, Val(Me.TablaImportar.Item(Pdep2.Index, i).Value) / 100, Val(Me.TablaImportar.Item(Pdep2.Index, i).Value))
            Suma += IIf(Val(Me.TablaImportar.Item(Pdep3.Index, i).Value) > 1, Val(Me.TablaImportar.Item(Pdep3.Index, i).Value) / 100, Val(Me.TablaImportar.Item(Pdep3.Index, i).Value))
            Suma += IIf(Val(Me.TablaImportar.Item(Pdep4.Index, i).Value) > 1, Val(Me.TablaImportar.Item(Pdep4.Index, i).Value) / 100, Val(Me.TablaImportar.Item(Pdep4.Index, i).Value))

            If Suma > 1 Then
                RadMessageBox.Show("Los porcentajes de Resultados sobrepasan el 100% ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)

                Me.CmdPoliza.Enabled = False
            ElseIf Suma < 1 Then
                RadMessageBox.Show("Los porcentajes de Resultados no cumplen con el 100% ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                Me.CmdPoliza.Enabled = False
            Else
                Me.CmdPoliza.Enabled = True
            End If
        End If
    End Sub
    Private Function BuscarPoliza(ByVal mes As String, ByVal Anio As Integer, ByVal tipo As Integer, ByVal Descripcion As String)
        Dim Hacer As String = ""
        Dim sql As String = " SELECT * FROM Polizas WHERE ID_anio = " & Anio & "  AND ID_mes = '" & mes & "' AND Concepto = '" & Descripcion & "' AND Id_Empresa =" & Me.lstCliente.SelectItem & "   And Id_Tipo_Pol_Sat = " & tipo & " "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Hacer = ds.Tables(0).Rows(0)(0)
        End If
        Return Hacer
    End Function
    Private Function Creapoliza(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String, ByVal dia As String,
                         ByVal consecutivo As Integer, ByVal tipo As Integer, ByVal fecha As String,
                         ByVal concepto As String, ByVal movimiento As String, ByVal num_pol As Integer)
        Dim hacer As Boolean
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Polizas "
        sql &= "("
        sql &= " 	ID_poliza ,      "
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
        sql &= "  '" & Eventos.Usuario(Inicio.LblUsuario.Text) & "', 1" '@usuario            
        sql &= " 	) "

        If Eventos.Comando_sql(sql) > 0 Then
            hacer = True
            Eventos.Insertar_usuariol("InsertarPolizCp", sql)
        Else
            hacer = False
        End If
        Return hacer
    End Function
    Private Sub Crea_detalle_poliza(ByVal id_poliza As String, ByVal item As Integer, ByVal cargo As Decimal,
                                       ByVal Abono As Decimal, ByVal cuenta As String, ByVal cheque As String)
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Detalle_Polizas "
        sql &= "(   "
        sql &= " ID_poliza ,      "
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
            Eventos.Insertar_usuariol("InsertarPolizD", sql)
        End If
    End Sub

    Private Sub CmdExportar_Click(sender As Object, e As EventArgs) Handles CmdExportar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")

        If Me.TablaImportar.RowCount > 0 Then
            Dim Posicion As Integer = 1
            RadMessageBox.Show("Este Proceso puede tardar dependiendo de la información a exportar, presione Aceptar y espere a que el proceso termine...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Dim frm As New DialogExpExcel
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.TablaImportar.RowCount - 1
            Dim m_Excel As Microsoft.Office.Interop.Excel.Application

            Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook



            m_Excel = New Microsoft.Office.Interop.Excel.Application
            m_Excel.Visible = False

            objLibroExcel = m_Excel.Workbooks.Add()
            m_Excel.Worksheets.Add()
            m_Excel.Workbooks(1).Worksheets(1).Name = "Activos Anual"
            m_Excel.Workbooks(1).Worksheets(2).Name = "Activos Mensual"


            Dim i As Integer, j As Integer
            For i = 0 To TablaImportar.Columns.Count - 1
                m_Excel.Workbooks(1).Worksheets(1).Cells(Posicion, i + 1) = Me.TablaImportar.Columns.Item(i).HeaderCell.Value
            Next
            Posicion = 2
            For i = 0 To TablaImportar.RowCount - 1
                frm.Barra.Value1 = i
                For j = 0 To TablaImportar.Columns.Count - 1
                    If IsNumeric(Me.TablaImportar.Item(j, i).Value) Then
                        m_Excel.Workbooks(1).Worksheets(1).Columns(j).NumberFormat = "$#,##0.00_);[Red]($#,##0.00)"
                    End If
                    m_Excel.Workbooks(1).Worksheets(1).Cells(Posicion, j + 1) = Me.TablaImportar.Item(j, i).Value
                Next
                Posicion += 1
            Next
            Posicion += 3
            For i = 0 To TablaResumen.Columns.Count - 1

                m_Excel.Workbooks(1).Worksheets(1).Cells(Posicion, i + 1) = Me.TablaResumen.Columns.Item(i).HeaderCell.Value
            Next
            Posicion += 1
            For i = 0 To TablaResumen.RowCount - 1
                frm.Barra.Value1 = i
                For j = 0 To TablaResumen.Columns.Count - 1
                    If IsNumeric(Me.TablaResumen.Item(j, i).Value) Then
                        m_Excel.Workbooks(1).Worksheets(1).Columns(j).NumberFormat = "$#,##0.00_);[Red]($#,##0.00)"
                    End If
                    m_Excel.Workbooks(1).Worksheets(1).Cells(Posicion, j + 1) = Me.TablaResumen.Item(j, i).Value
                Next
                Posicion += 1
            Next



            For i = 0 To TablaMensual.Columns.Count - 1
                m_Excel.Workbooks(1).Worksheets(2).Cells(1, i + 1) = Me.TablaMensual.Columns.Item(i).HeaderCell.Value
            Next
            For i = 0 To TablaMensual.RowCount - 1
                frm.Barra.Value1 = i
                For j = 0 To TablaMensual.Columns.Count - 1
                    If IsNumeric(Me.TablaMensual.Item(j, i).Value) Then
                        m_Excel.Workbooks(1).Worksheets(2).Columns(j).NumberFormat = "$#,##0.00_);[Red]($#,##0.00)"
                    End If
                    If Me.TablaMensual.Item(j, i).Value Is Nothing Then
                        m_Excel.Workbooks(1).Worksheets(2).Cells(i + 2, j + 1) = ""
                    ElseIf Me.TablaMensual.Item(j, i).Value.ToString() = "0" Then
                        m_Excel.Workbooks(1).Worksheets(2).Cells(i + 2, j + 1) = ""
                    Else
                        m_Excel.Workbooks(1).Worksheets(2).Cells(i + 2, j + 1) = Me.TablaMensual.Item(j, i).Value
                    End If


                Next
            Next
            frm.Close()
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            m_Excel.Visible = True
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No hay registros a exportar....", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub
End Class
