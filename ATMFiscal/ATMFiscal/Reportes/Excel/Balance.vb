Imports System.IO
Imports Telerik.WinControls
Public Class Balance
    Private Sub Balance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar()
    End Sub
    Private Sub Cargar()
        Eventos.DiseñoTabla(Me.Tabla)
        Eventos.DiseñoTabla(Me.Tabla2)
        Eventos.DiseñoTabla(Me.Tabla3)
        Me.LstCliente.Cargar("SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social FROM     Empresa  ")
        Me.LstCliente.SelectItem = 1

    End Sub
    Private Sub Limpiar()
        Me.Tabla.Rows.Clear()
        Me.Tabla2.Rows.Clear()
        Me.Tabla3.Rows.Clear()
    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        Limpiar()
    End Sub

    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdImportar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.LstCliente.SelectText <> "" Then
            Eventos.Cuentas(Me.DtInicio.Value.ToString.Substring(6, 4), Me.LstCliente.SelectItem)
            Limpiar()
            Buscar_Activos(Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10))
            Pasivos()
            Buscar_COrden(Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10))
            QuitacuentasceroBalance()
        Else
            RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub

    Private Sub Buscar_Activos(ByVal Fi As String, ByVal Ff As String)

        'Activo Circulante
        Dim Negritas As Font
        Negritas = New Font(Tabla.Font, FontStyle.Bold)

        Dim posicion As Integer = 0
        Dim saldo_fin As Decimal = 0
        Dim total As Decimal = 0
        Dim sql As String = "SELECT Catalogo_de_Cuentas.Nivel1, Catalogo_de_Cuentas.Descripcion FROM Catalogo_de_Cuentas WHERE Clasificacion ='ACI'AND Nivel1 >0 AND Nivel2 ='0000' and Id_Empresa = " & Me.LstCliente.SelectItem & " and catalogo_de_cuentas.Cuenta IN (SELECT Cuenta FROM Cuentas_Con_Saldo WHERE Anio = " & Me.DtInicio.Value.ToString.Substring(6, 4) & " AND Id_Empresa = " & Me.LstCliente.SelectItem & "  ) Order By Nivel1"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.RowCount = ds.Tables(0).Rows.Count + 2
            Me.Tabla.Item(Desc.Index, posicion).Value = "Activo Circulante"
            Me.Tabla.Rows(posicion).DefaultCellStyle.BackColor = Color.PaleGreen
            Me.Tabla.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                posicion = posicion + 1
                Me.Tabla.Item(CtaAc.Index, posicion).Value = ds.Tables(0).Rows(i)(0)
                Me.Tabla.Item(Desc.Index, posicion).Value = ds.Tables(0).Rows(i)(1)
                Me.Tabla.Item(Saldof.Index, posicion).Value = Calcula_saldo(Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10), ds.Tables(0).Rows(i)(0), False)
                If Me.Tabla.Item(Saldof.Index, posicion).Value < 0 Then
                    Me.Tabla.Rows(posicion).DefaultCellStyle.BackColor = Color.Red
                End If
                saldo_fin = saldo_fin + Me.Tabla.Item(Saldof.Index, posicion).Value
            Next

            posicion = posicion + 1
            Me.Tabla.Item(Desc.Index, posicion).Value = "Total Activo Circulante"
            Me.Tabla.Rows(posicion).DefaultCellStyle.BackColor = Color.RoyalBlue
            Me.Tabla.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
            Me.Tabla.Item(Saldof.Index, posicion).Value = saldo_fin
            Me.Tabla.Rows(posicion).Cells(Saldof.Index).Style.Font = Negritas

            total = total + saldo_fin
        End If

        'Activo Fijo
        Me.Tabla.Rows.Add()
        Try
            Me.Tabla.Rows(posicion + 1).DefaultCellStyle.BackColor = Color.FloralWhite
            Me.Tabla.Rows(posicion + 1).Cells(Desc.Index).Style.Font = Negritas
            posicion = posicion + 1
        Catch ex As Exception

        End Try
        sql = "SELECT Catalogo_de_Cuentas.Nivel1, Catalogo_de_Cuentas.Descripcion FROM Catalogo_de_Cuentas WHERE Clasificacion ='AFI' AND Nivel1 >0 AND Nivel2 ='0000' and Id_Empresa = " & Me.LstCliente.SelectItem & " and catalogo_de_cuentas.Cuenta IN (SELECT Cuenta FROM Cuentas_Con_Saldo WHERE Anio = " & Me.DtInicio.Value.ToString.Substring(6, 4) & " AND Id_Empresa = " & Me.LstCliente.SelectItem & "  )  Order By Nivel1"
        ds.Clear()
        ds = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.RowCount = Me.Tabla.RowCount + ds.Tables(0).Rows.Count + 2
            posicion = posicion + 1
            saldo_fin = 0
            Me.Tabla.Item(Desc.Index, posicion).Value = "Activo Fijo"
            Me.Tabla.Rows(posicion).DefaultCellStyle.BackColor = Color.PaleGreen
            Me.Tabla.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                posicion = posicion + 1
                Me.Tabla.Item(CtaAc.Index, posicion).Value = ds.Tables(0).Rows(i)(0)
                Me.Tabla.Item(Desc.Index, posicion).Value = ds.Tables(0).Rows(i)(1)
                Me.Tabla.Item(Saldof.Index, posicion).Value = Calcula_saldo(Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10), ds.Tables(0).Rows(i)(0), False)

                saldo_fin = saldo_fin + Me.Tabla.Item(Saldof.Index, posicion).Value
            Next

            posicion = posicion + 1
            Me.Tabla.Item(Desc.Index, posicion).Value = "Total Activo Fijo"
            Me.Tabla.Rows(posicion).DefaultCellStyle.BackColor = Color.RoyalBlue
            Me.Tabla.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
            Me.Tabla.Item(Saldof.Index, posicion).Value = saldo_fin
            Me.Tabla.Rows(posicion).Cells(Saldof.Index).Style.Font = Negritas
            total = total + saldo_fin
        End If

        Me.Tabla.Rows.Add()
        Try
            Me.Tabla.Rows(posicion + 1).DefaultCellStyle.BackColor = Color.FloralWhite
            Me.Tabla.Rows(posicion + 1).Cells(Desc.Index).Style.Font = Negritas
            posicion = posicion + 1
        Catch ex As Exception

        End Try

        'Activo Diferido

        sql = "SELECT Catalogo_de_Cuentas.Nivel1, Catalogo_de_Cuentas.Descripcion FROM Catalogo_de_Cuentas WHERE Clasificacion ='ADF' AND Nivel1 >0 AND Nivel2 ='0000' and Id_Empresa = " & Me.LstCliente.SelectItem & " and catalogo_de_cuentas.Cuenta IN (SELECT Cuenta FROM Cuentas_Con_Saldo WHERE Anio = " & Me.DtInicio.Value.ToString.Substring(6, 4) & " AND Id_Empresa = " & Me.LstCliente.SelectItem & "  ) Order By Nivel1"
        ds.Clear()
        ds = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.RowCount = Me.Tabla.RowCount + ds.Tables(0).Rows.Count + 2
            posicion = posicion + 1
            saldo_fin = 0
            Me.Tabla.Item(Desc.Index, posicion).Value = "Activo Diferido"
            Me.Tabla.Rows(posicion).DefaultCellStyle.BackColor = Color.PaleGreen
            Me.Tabla.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                posicion = posicion + 1
                Me.Tabla.Item(CtaAc.Index, posicion).Value = ds.Tables(0).Rows(i)(0)
                Me.Tabla.Item(Desc.Index, posicion).Value = ds.Tables(0).Rows(i)(1)
                Me.Tabla.Item(Saldof.Index, posicion).Value = Calcula_saldo(Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10), ds.Tables(0).Rows(i)(0), False)

                saldo_fin = saldo_fin + Me.Tabla.Item(Saldof.Index, posicion).Value
            Next

            posicion = posicion + 1
            Me.Tabla.Item(Desc.Index, posicion).Value = "Total Activo Diferido"
            Me.Tabla.Rows(posicion).DefaultCellStyle.BackColor = Color.RoyalBlue
            Me.Tabla.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
            Me.Tabla.Item(Saldof.Index, posicion).Value = saldo_fin
            Me.Tabla.Rows(posicion).Cells(Saldof.Index).Style.Font = Negritas
            total = total + saldo_fin
        End If

        Me.Tabla.Rows.Add()
        Try
            Me.Tabla.Rows(posicion + 1).DefaultCellStyle.BackColor = Color.FloralWhite
            Me.Tabla.Rows(posicion + 1).Cells(Desc.Index).Style.Font = Negritas
            posicion = posicion + 1
        Catch ex As Exception

        End Try



        posicion = posicion + 1
        'total = total + saldo_fin
        Me.Tabla.RowCount = Me.Tabla.RowCount + 1
        Me.Tabla.Item(Desc.Index, posicion).Value = "Total Activo "
        Me.Tabla.Rows(posicion).DefaultCellStyle.BackColor = Color.RoyalBlue
        Me.Tabla.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
        Me.Tabla.Item(Saldof.Index, posicion).Value = total
        Me.Tabla.Rows(posicion).Cells(Saldof.Index).Style.Font = Negritas
    End Sub
    Private Sub Pasivos()
        'Pasivo a Corto Plazo
        Dim Negritas As Font
        Negritas = New Font(Tabla2.Font, FontStyle.Bold)
        Dim posicion As Integer = 0
        Dim saldo_fin As Decimal = 0
        Dim total As Decimal = 0
        Dim sql As String = "SELECT Catalogo_de_Cuentas.Nivel1, Catalogo_de_Cuentas.Descripcion FROM Catalogo_de_Cuentas WHERE Clasificacion ='PCP' AND Nivel1 >0 AND Nivel2 ='0000' and Id_Empresa = " & Me.LstCliente.SelectItem & " and catalogo_de_cuentas.Cuenta IN (SELECT Cuenta FROM Cuentas_Con_Saldo WHERE Anio = " & Me.DtInicio.Value.ToString.Substring(6, 4) & " AND Id_Empresa = " & Me.LstCliente.SelectItem & "  )  Order By Nivel1 "

        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla2.RowCount = Me.Tabla2.RowCount + ds.Tables(0).Rows.Count + 2
            Me.Tabla2.Item(Desc.Index, posicion).Value = "Pasivo a Corto Plazo"
            Me.Tabla2.Rows(posicion).DefaultCellStyle.BackColor = Color.PaleGreen
            Me.Tabla2.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                posicion = posicion + 1
                Me.Tabla2.Item(CtaAc.Index, posicion).Value = ds.Tables(0).Rows(i)(0)
                Me.Tabla2.Item(Desc.Index, posicion).Value = ds.Tables(0).Rows(i)(1)
                Me.Tabla2.Item(Saldof.Index, posicion).Value = Calcula_saldo(Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10), ds.Tables(0).Rows(i)(0), True) * -1
                If Me.Tabla2.Item(Saldof.Index, posicion).Value > 0 Then
                    Me.Tabla2.Rows(posicion).DefaultCellStyle.BackColor = Color.Red
                End If
                Me.Tabla2.Item(Saldof.Index, posicion).Value = Me.Tabla2.Item(Saldof.Index, posicion).Value * -1
                saldo_fin = saldo_fin + Me.Tabla2.Item(Saldof.Index, posicion).Value
            Next
            posicion = posicion + 1
            Me.Tabla2.Item(Desc.Index, posicion).Value = "Total Pasivo a Corto Plazo"
            Me.Tabla2.Rows(posicion).DefaultCellStyle.BackColor = Color.RoyalBlue
            Me.Tabla2.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
            Me.Tabla2.Item(Saldof.Index, posicion).Value = saldo_fin
            Me.Tabla2.Rows(posicion).Cells(Saldof.Index).Style.Font = Negritas
            total = total + saldo_fin
        End If
        Me.Tabla2.Rows.Add()
        Try
            Me.Tabla2.Rows(posicion + 1).DefaultCellStyle.BackColor = Color.FloralWhite
            Me.Tabla2.Rows(posicion + 1).Cells(Desc.Index).Style.Font = Negritas
            posicion = posicion + 1
        Catch ex As Exception

        End Try

        'Pasivo a Largo Plazo

        sql = "SELECT Catalogo_de_Cuentas.Nivel1, Catalogo_de_Cuentas.Descripcion FROM Catalogo_de_Cuentas WHERE Clasificacion ='PLP' AND Nivel1 >0 AND Nivel2 ='0000' and Id_Empresa = " & Me.LstCliente.SelectItem & " and catalogo_de_cuentas.Cuenta IN (SELECT Cuenta FROM Cuentas_Con_Saldo WHERE Anio = " & Me.DtInicio.Value.ToString.Substring(6, 4) & " AND Id_Empresa = " & Me.LstCliente.SelectItem & "  )  Order By Nivel1 "
        ds.Clear()
        ds = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla2.RowCount = Me.Tabla2.RowCount + ds.Tables(0).Rows.Count + 2
            posicion = posicion + 1
            saldo_fin = 0
            Me.Tabla2.Item(Desc.Index, posicion).Value = "Pasivo a Largo Plazo"
            Me.Tabla2.Rows(posicion).DefaultCellStyle.BackColor = Color.PaleGreen
            Me.Tabla2.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                posicion = posicion + 1
                Me.Tabla2.Item(CtaAc.Index, posicion).Value = ds.Tables(0).Rows(i)(0)
                Me.Tabla2.Item(Desc.Index, posicion).Value = ds.Tables(0).Rows(i)(1)
                Me.Tabla2.Item(Saldof.Index, posicion).Value = Calcula_saldo(Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10), ds.Tables(0).Rows(i)(0), True) * -1

                If Me.Tabla2.Item(Saldof.Index, posicion).Value > 0 Then
                    Me.Tabla2.Rows(posicion).DefaultCellStyle.BackColor = Color.Red
                End If
                Me.Tabla2.Item(Saldof.Index, posicion).Value = Me.Tabla2.Item(Saldof.Index, posicion).Value * -1
                saldo_fin = saldo_fin + Me.Tabla2.Item(Saldof.Index, posicion).Value
            Next

            posicion = posicion + 1
            Me.Tabla2.Item(Desc.Index, posicion).Value = "Total Pasivo a Largo Plazo"
            Me.Tabla2.Rows(posicion).DefaultCellStyle.BackColor = Color.RoyalBlue
            Me.Tabla2.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
            Me.Tabla2.Item(Saldof.Index, posicion).Value = saldo_fin
            Me.Tabla2.Rows(posicion).Cells(Saldof.Index).Style.Font = Negritas
            total = total + saldo_fin
        End If
        posicion = posicion + 1
        'total = total + saldo_fin
        Me.Tabla2.RowCount = Me.Tabla2.RowCount + 1
        Me.Tabla2.Item(Desc.Index, posicion).Value = "Total Pasivo "
        Me.Tabla2.Rows(posicion).DefaultCellStyle.BackColor = Color.RoyalBlue
        Me.Tabla2.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
        Me.Tabla2.Item(Saldof.Index, posicion).Value = total
        Me.Tabla2.Rows(posicion).Cells(Saldof.Index).Style.Font = Negritas
        Me.Tabla2.Rows.Add()
        Try
            Me.Tabla2.Rows(posicion + 1).DefaultCellStyle.BackColor = Color.FloralWhite
            Me.Tabla2.Rows(posicion + 1).Cells(Desc.Index).Style.Font = Negritas
        Catch ex As Exception

        End Try

        posicion = posicion + 1
        'Capital contable

        sql = "SELECT Catalogo_de_Cuentas.Nivel1, Catalogo_de_Cuentas.Descripcion FROM Catalogo_de_Cuentas WHERE Clasificacion ='CCO' AND Nivel1 >0 AND Nivel2 ='0000' and Id_Empresa = " & Me.LstCliente.SelectItem & " and catalogo_de_cuentas.Cuenta IN (SELECT Cuenta FROM Cuentas_Con_Saldo WHERE Anio = " & Me.DtInicio.Value.ToString.Substring(6, 4) & " AND Id_Empresa = " & Me.LstCliente.SelectItem & "  )  Order By Nivel1"
        ds.Clear()
        ds = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla2.RowCount = Me.Tabla2.RowCount + ds.Tables(0).Rows.Count + 1
            posicion = posicion + 1
            saldo_fin = 0
            Me.Tabla2.Item(Desc.Index, posicion).Value = "Capital Contable"
            Me.Tabla2.Rows(posicion).DefaultCellStyle.BackColor = Color.PaleGreen
            Me.Tabla2.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                posicion = posicion + 1
                Me.Tabla2.Item(CtaAc.Index, posicion).Value = ds.Tables(0).Rows(i)(0)
                Me.Tabla2.Item(Desc.Index, posicion).Value = ds.Tables(0).Rows(i)(1)
                Me.Tabla2.Item(Saldof.Index, posicion).Value = Calcula_saldo(Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10), ds.Tables(0).Rows(i)(0), True)

                Me.Tabla2.Item(Saldof.Index, posicion).Value = Me.Tabla2.Item(Saldof.Index, posicion).Value
                saldo_fin = saldo_fin + Me.Tabla2.Item(Saldof.Index, posicion).Value
            Next

        End If

        Me.Tabla2.Rows.Add()
        Try
            Me.Tabla2.Rows(posicion + 1).DefaultCellStyle.BackColor = Color.FloralWhite
            Me.Tabla2.Rows(posicion + 1).Cells(Desc.Index).Style.Font = Negritas
            posicion = posicion + 1
            posicion = posicion + 1
        Catch ex As Exception

        End Try

        Me.Tabla2.RowCount = Me.Tabla2.RowCount + 1
        Me.Tabla2.Item(Desc.Index, posicion).Value = "Resultado del Ejercicio "
        Me.Tabla2.Rows(posicion).DefaultCellStyle.BackColor = Color.PaleGreen
        Me.Tabla2.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
        Me.Tabla2.Item(Saldof.Index, posicion).Value = Eventos.Utilidad_Bruta(Me.LstCliente.SelectItem, Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10))

        Me.Tabla2.Rows.Add()
        Try
            Me.Tabla2.Rows(posicion + 1).DefaultCellStyle.BackColor = Color.FloralWhite
            Me.Tabla2.Rows(posicion + 1).Cells(Desc.Index).Style.Font = Negritas
            posicion = posicion + 1
            posicion = posicion + 1
        Catch ex As Exception

        End Try


        Me.Tabla2.RowCount = Me.Tabla2.RowCount + 1
        Me.Tabla2.Item(Desc.Index, posicion).Value = "Total Capital Contable"
        Me.Tabla2.Rows(posicion).DefaultCellStyle.BackColor = Color.RoyalBlue
        Me.Tabla2.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
        Me.Tabla2.Item(Saldof.Index, posicion).Value = saldo_fin + Eventos.Utilidad_Bruta(Me.LstCliente.SelectItem, Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10))
        Me.Tabla2.Rows(posicion).Cells(Saldof.Index).Style.Font = Negritas
        Me.Tabla2.Rows.Add()
        Try
            Me.Tabla2.Rows(posicion + 1).DefaultCellStyle.BackColor = Color.FloralWhite
            Me.Tabla2.Rows(posicion + 1).Cells(Desc.Index).Style.Font = Negritas
            posicion = posicion + 1
            posicion = posicion + 1
        Catch ex As Exception

        End Try



        total = total + saldo_fin + Eventos.Utilidad_Bruta(Me.LstCliente.SelectItem, Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10))
        Me.Tabla2.RowCount = Me.Tabla2.RowCount + 1
        Me.Tabla2.Item(Desc.Index, posicion).Value = "Total Pasivo Mas Capital Contable"
        Me.Tabla2.Rows(posicion).DefaultCellStyle.BackColor = Color.RoyalBlue
        Me.Tabla2.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
        Me.Tabla2.Item(Saldof.Index, posicion).Value = total
        Me.Tabla2.Rows(posicion).Cells(Saldof.Index).Style.Font = Negritas

    End Sub
    Private Sub Buscar_COrden(ByVal Fi As String, ByVal Ff As String)

        'Cuentas de Orden
        Dim Negritas As Font
        Negritas = New Font(Tabla3.Font, FontStyle.Bold)

        Dim posicion As Integer = 0
        Dim saldo_fin As Decimal = 0
        Dim total As Decimal = 0
        Dim sql As String = "SELECT Catalogo_de_Cuentas.Nivel1, Catalogo_de_Cuentas.Descripcion,Catalogo_de_Cuentas.Naturaleza FROM Catalogo_de_Cuentas WHERE Clasificacion ='ORB'AND Nivel1 >0 AND Nivel2 ='0000' and Id_Empresa = " & Me.LstCliente.SelectItem & " and catalogo_de_cuentas.Cuenta IN (SELECT Cuenta FROM Cuentas_Con_Saldo WHERE Anio = " & Me.DtInicio.Value.ToString.Substring(6, 4) & " AND Id_Empresa = " & Me.LstCliente.SelectItem & "  )"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla3.RowCount = ds.Tables(0).Rows.Count + 2
            Me.Tabla3.Item(Desc.Index, posicion).Value = "Cuentas de Orden"
            Me.Tabla3.Rows(posicion).DefaultCellStyle.BackColor = Color.PaleGreen
            Me.Tabla3.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                posicion = posicion + 1
                Me.Tabla3.Item(CtaAc.Index, posicion).Value = ds.Tables(0).Rows(i)(0)
                Me.Tabla3.Item(Desc.Index, posicion).Value = ds.Tables(0).Rows(i)(1)
                If ds.Tables(0).Rows(i)(2).ToString().Trim() = "D" Then
                    Me.Tabla3.Item(Saldof.Index, posicion).Value = Calcula_saldo(Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10), ds.Tables(0).Rows(i)(0), False)
                Else
                    Me.Tabla3.Item(Saldof.Index, posicion).Value = Calcula_saldo(Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10), ds.Tables(0).Rows(i)(0), True) * -1
                End If

                saldo_fin = saldo_fin + Me.Tabla3.Item(Saldof.Index, posicion).Value
            Next
            posicion += 1
            total = total + saldo_fin
        End If
        Try
            Me.Tabla3.Item(Desc.Index, posicion).Value = "Total Cuentas Orden"
            Me.Tabla3.Rows(posicion).DefaultCellStyle.BackColor = Color.RoyalBlue
            Me.Tabla3.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
            Me.Tabla3.Item(Saldof.Index, posicion).Value = total
            If Me.Tabla3.Item(Saldof.Index, posicion).Value > 0 Then
                Me.Tabla3.Rows(posicion).DefaultCellStyle.BackColor = Color.Red
            End If
            Me.Tabla3.Rows(posicion).Cells(Saldof.Index).Style.Font = Negritas
        Catch ex As Exception

        End Try
    End Sub
    Private Function Calcula_saldo(ByVal Fi As String, ByVal FF As String, ByVal cuenta As String, ByVal Evaluar As Boolean)

        Dim saldo As Decimal

        Dim Sql As String = "SELECT sum(Saldo) AS S FROM ("
        Sql &= " SELECT   Case WHEN Naturaleza = 'D' THEN sum(cargo)-sum(abono) WHEN Naturaleza = 'A' THEN sum(abono)-sum(cargo) END  AS Saldo FROM Detalle_Polizas"
        Sql &= " INNER JOIN  Polizas ON Polizas.ID_poliza = Detalle_Polizas.ID_poliza"
        Sql &= " INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Cuenta = Detalle_Polizas.Cuenta"
        Sql &= " WHERE Catalogo_de_Cuentas.Id_Empresa = " & Me.LstCliente.SelectItem & "   and Detalle_Polizas.Cuenta  IN ( SELECT Cuenta  FROM Catalogo_de_Cuentas WHERE  Nivel1 ='" & cuenta & "' AND Id_Empresa = " & Me.LstCliente.SelectItem & " ) AND "
        Sql &= " Polizas.Id_Empresa = " & Me.LstCliente.SelectItem & "   and  (Polizas.Fecha_captura >= " & Eventos.Sql_hoy(Fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(FF) & " )"
        Sql &= " GROUP BY  Catalogo_de_Cuentas.Naturaleza  ) AS Tabla"
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            saldo = IIf(IsDBNull(ds.Tables(0).Rows(0)(0)) = True, 0, ds.Tables(0).Rows(0)(0))
        Else
            saldo = 0
        End If
        Dim Sal As Decimal = 0
        Dim Acumulado As Decimal = Calcula_Saldos_InicialesP(cuenta.ToString().PadRight(16, "0"), Fi.ToString.Substring(6, 4), Me.LstCliente.SelectItem, "  ( Polizas.Fecha_captura <" & Eventos.Sql_hoy(Fi) & " )")
        Sal = Eventos.Calcula_Saldos_Iniciales(cuenta.ToString().PadRight(16, "0"), Fi.ToString.Substring(6, 4) - 1, Me.LstCliente.SelectItem) + Acumulado
        If Evaluar = True Then


            If (Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Naturaleza", " Nivel1 = " & cuenta.ToString().Substring(0, 4) & " AND Nivel2=0000 And Id_Empresa= " & Me.LstCliente.SelectItem & "", True)).ToString().Trim() = "A" Then
                Sal = Sal * -1
            Else
                Sal = Sal
            End If
        End If

        Return Sal + saldo
    End Function
    Private Function Calcula_anteriores(ByVal Sql As String)
        Dim saldo As Decimal = 0
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                If ds.Tables(0).Rows(i)(0) <> 0 Then
                    saldo += ds.Tables(0).Rows(i)(0)
                Else
                    saldo += ds.Tables(0).Rows(i)(1)
                End If
            Next
        Else
            saldo = 0
        End If
        Return saldo
    End Function
    Private Sub QuitacuentasceroBalance()
        Dim filas As Integer = Me.Tabla.RowCount - 1

        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.Tabla.RowCount - 1
                If Me.Tabla.Rows(i).DefaultCellStyle.BackColor.Name = "0" Then
                    If Me.Tabla.Item(2, i).Value = 0 Or Me.Tabla.Item(2, i).Value = Nothing Then
                        'If Trim(Me.Tabla.Item(Desc.Index, i).Value) <> "Activo Circulante" Then
                        Me.Tabla.Rows.RemoveAt(i)
                        Exit For
                        'End If
                    End If
                End If


            Next
        Next
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.Tabla2.RowCount - 1
                If Me.Tabla2.Rows(i).DefaultCellStyle.BackColor.Name = "0" Then
                    If Me.Tabla2.Item(2, i).Value = 0 Or Me.Tabla2.Item(2, i).Value = Nothing Then
                        'If Trim(Me.Tabla2.Item(Desc.Index, i).Value) <> "Pasivo a Corto Plazo" Then
                        Me.Tabla2.Rows.RemoveAt(i)
                        Exit For
                        'End If
                    End If
                End If
            Next
        Next

        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.Tabla3.RowCount - 1
                If Me.Tabla3.Item(2, i).Value = 0 Or Me.Tabla3.Item(2, i).Value = Nothing Then
                    If Me.Tabla.Rows(i).DefaultCellStyle.BackColor.Name = "0" Then
                        If Trim(Me.Tabla3.Item(Desc.Index, i).Value) <> "Total Cuentas Orden" Then
                            'If Trim(Me.Tabla3.Item(Desc.Index, i).Value) <> "Cuentas de Orden" Then
                            Me.Tabla3.Rows.RemoveAt(i)
                            Exit For
                            'End If
                        End If
                    End If
                End If
            Next
        Next
    End Sub
    Public Function Calcula_Saldos_InicialesP(ByVal Cuenta As String, ByVal Anio As String, ByVal id_cliente As Integer, ByVal Periodo As String)
        Dim Saldo As Decimal = 0
        Dim where As String = ""
        If Cuenta.Substring(8, 4) = "0000" And Cuenta.Substring(4, 4) = "0000" And Cuenta.Substring(12, 4) = "0000" Then
            where = "Catalogo_de_Cuentas.Nivel1 =  '" & Cuenta.Substring(0, 4) & "' "
        ElseIf Cuenta.Substring(4, 4) >= "0000" And Cuenta.Substring(8, 4) = "0000" And Cuenta.Substring(12, 4) = "0000" Then
            where = "Catalogo_de_Cuentas.Nivel1 =  '" & Cuenta.Substring(0, 4) & "' and Catalogo_de_Cuentas.Nivel2 =  '" & Cuenta.Substring(4, 4) & "'  "
        ElseIf Cuenta.Substring(4, 4) > "0000" And Cuenta.Substring(8, 4) > "0000" And Cuenta.Substring(12, 4) = "0000" Then
            where = "Catalogo_de_Cuentas.Nivel1 =  '" & Cuenta.Substring(0, 4) & "' and Catalogo_de_Cuentas.Nivel2 =  '" & Cuenta.Substring(4, 4) & "' and Catalogo_de_Cuentas.Nivel3 =  '" & Cuenta.Substring(8, 4) & "'  "
        ElseIf Cuenta.Substring(4, 4) > "0000" And Cuenta.Substring(8, 4) > "0000" And Cuenta.Substring(12, 4) > "0000" Then
            where = "Catalogo_de_Cuentas.Nivel1 =  '" & Cuenta.Substring(0, 4) & "' and Catalogo_de_Cuentas.Nivel2 =  '" & Cuenta.Substring(4, 4) & "' and Catalogo_de_Cuentas.Nivel3 =  '" & Cuenta.Substring(8, 4) & "' and Catalogo_de_Cuentas.Nivel4 =  '" & Cuenta.Substring(12, 4) & "'  "
        End If


        Dim sql As String = " SELECT  CASE WHEN Naturaleza = 'D' THEN sUM(cargos - abonos) WHEN Naturaleza = 'A' THEN Sum(abonos - cargos) END AS Saldo   FROM (
	                              SELECT Detalle_Polizas.Cuenta ,Catalogo_de_Cuentas.Naturaleza,sum(cargo ) AS Cargos,sum (abono )AS Abonos FROM Detalle_Polizas 
	                              INNER JOIN Polizas ON Polizas.ID_poliza = Detalle_Polizas.ID_poliza
	                              INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
	                              INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Cuenta = Detalle_Polizas.Cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa 
	                              WHERE  Polizas.Aplicar_Poliza =1 AND Polizas.Id_Empresa= " & id_cliente & " AND  " & where & "  AND  Polizas.Id_Anio ='" & Anio & "'  AND " & Periodo & "
	                              GROUP BY Detalle_Polizas.Cuenta ,Catalogo_de_Cuentas.Naturaleza
	                              ) AS Tabla_Saldos GROUP BY NATURALEZA"
        ' Polizas.Fecha_captura < " & Eventos.Sql_hoy(fecha) & "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Saldo = IIf(IsDBNull(ds.Tables(0).Rows(0)(0)) = True, 0, ds.Tables(0).Rows(0)(0))
        Else
            Saldo = 0
        End If
        Return Saldo

    End Function
    Private Sub Crear_Reporte()
        Dim DT As New DataTable
        Dim Pas As New DataTable
        Dim Co As New DataTable
        With DT
            .Columns.Add("Descripcion")
            .Columns.Add("Saldo", Type.GetType("System.Decimal"))
        End With
        With Pas
            .Columns.Add("Descripcion")
            .Columns.Add("Saldo", Type.GetType("System.Decimal"))
        End With
        With Co
            .Columns.Add("Descripcion")
            .Columns.Add("Saldo", Type.GetType("System.Decimal"))
        End With
        For Each dr As DataGridViewRow In Me.Tabla.Rows
            DT.Rows.Add(UCase(dr.Cells(Desc.Index).Value), dr.Cells(Saldof.Index).Value)
        Next
        For Each dr2 As DataGridViewRow In Me.Tabla2.Rows
            Pas.Rows.Add(UCase(dr2.Cells(desc2.Index).Value), dr2.Cells(sald2.Index).Value)
        Next
        For Each dr3 As DataGridViewRow In Me.Tabla3.Rows
            Co.Rows.Add(UCase(dr3.Cells(DescO.Index).Value), dr3.Cells(SaldoO.Index).Value)
        Next
        Dim Cr As New BalancePdf
        Cr.Database.Tables("Activo").SetDataSource(DT)
        Cr.Database.Tables("Pasivo").SetDataSource(Pas)
        Cr.Database.Tables("CtasOrden").SetDataSource(Co)
        'Cr.SetDataSource(DT)
        Dim Reporte As New ReporteBalanzaGeneral

        Reporte.CrvBalanza.ReportSource = Cr

        Dim FechaD As New CrystalDecisions.Shared.ParameterDiscreteValue()
        Dim Rptinv As New CrystalDecisions.Shared.ParameterValues()
        FechaD.Value = Me.DtInicio.Value.ToString.Substring(0, 10)
        Rptinv.Add(FechaD)
        Cr.DataDefinition.ParameterFields("FechaD").ApplyCurrentValues(Rptinv)

        Dim FechaF As New CrystalDecisions.Shared.ParameterDiscreteValue()
        Dim ff As New CrystalDecisions.Shared.ParameterValues()
        FechaF.Value = Me.Dtfin.Value.ToString.Substring(0, 10)
        ff.Add(FechaF)
        Cr.DataDefinition.ParameterFields("FechaF").ApplyCurrentValues(ff)

        Dim RFC As String = Eventos.ObtenerValorDB("Empresa", "Reg_fed_causantes", " Id_Empresa = " & Me.LstCliente.SelectItem & "", True)
        Dim Cliente As New CrystalDecisions.Shared.ParameterDiscreteValue()
        Dim Cl As New CrystalDecisions.Shared.ParameterValues()
        Cliente.Value = Me.LstCliente.SelectText & vbCr & RFC
        Cl.Add(Cliente)
        Cr.DataDefinition.ParameterFields("Cliente").ApplyCurrentValues(Cl)
        Dim Clt As DataSet = Eventos.Obtener_DS("select * from Empresa where Id_Empresa = " & Me.LstCliente.SelectItem & " ")
        Cliente.Value = Clt.Tables(0).Rows(0)("Direccion")
        Cl.Add(Cliente)
        Cr.DataDefinition.ParameterFields("Direccion").ApplyCurrentValues(Cl)
        Dim Archivo As String = "C:\Program Files\Contable\SetupProyectoContable\Balance\PDF\Balance" & RFC & ".pdf"
        If Directory.Exists("C:\Program Files\Contable\SetupProyectoContable\Balance\PDF") = False Then
            ' si no existe la carpeta se crea
            Directory.CreateDirectory("C:\Program Files\Contable\SetupProyectoContable\Balance\PDF")
        End If
        If Directory.Exists(Archivo) Then
            My.Computer.FileSystem.DeleteFile(Archivo)
        End If
        Cr.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Archivo)
        Process.Start(Archivo)
    End Sub

    Private Sub CmdPdf_Click(sender As Object, e As EventArgs) Handles CmdPdf.Click
        Crear_Reporte()
    End Sub

    Private Sub CmdExp_Click(sender As Object, e As EventArgs) Handles CmdExp.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Tabla.RowCount > 0 Then
            RadMessageBox.Show("Este Proceso puede tardar dependiendo de la información a exportar, presione Aceptar y espere a que el proceso termine...", "Contable" & Eventos.versionDB, MessageBoxButtons.OK, RadMessageIcon.Info)

            Dim m_Excel As Microsoft.Office.Interop.Excel.Application
            Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook
            Dim objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet

            m_Excel = New Microsoft.Office.Interop.Excel.Application


            objLibroExcel = m_Excel.Workbooks.Add()
            objHojaExcel = objLibroExcel.Worksheets(1)
            objHojaExcel.Name = "Estado de Resultados"
            objHojaExcel.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
            objHojaExcel.Activate()
            Dim i As Integer, j As Integer
            Dim Item As Integer = 1
            For i = 0 To Tabla.Columns.Count - 1
                If i = 1 Or i = 2 Then
                    objHojaExcel.Cells(1, Item) = Me.Tabla.Columns.Item(i).HeaderCell.Value
                    Item += 1
                End If
            Next
            Dim Valor As String
            For i = 0 To Tabla.RowCount - 1

                For j = 0 To Tabla.Columns.Count - 1
                    Valor = IIf(IsDBNull(Me.Tabla.Item(j, i).Value), "", Me.Tabla.Item(j, i).Value)
                    If j = 1 Then
                        objHojaExcel.Cells(i + 2, 1) = Valor
                    ElseIf j = 2 Then
                        m_Excel.Workbooks(1).Worksheets(1).Columns(2).NumberFormat = "$#,##0.00_);[Red]($#,##0.00)"
                        objHojaExcel.Cells(i + 2, 2) = Valor
                    End If
                Next
            Next

            'Pasivos
            Item = 4
            For i = 0 To Tabla2.Columns.Count - 1
                If i = 1 Or i = 2 Then
                    objHojaExcel.Cells(1, Item) = Me.Tabla2.Columns.Item(i).HeaderCell.Value
                    Item += 1
                End If
            Next

            For i = 0 To Tabla2.RowCount - 1

                For j = 0 To Tabla2.Columns.Count - 1
                    Valor = IIf(IsDBNull(Me.Tabla2.Item(j, i).Value), "", Me.Tabla2.Item(j, i).Value)
                    If j = 1 Then
                        objHojaExcel.Cells(i + 2, 4) = Valor
                    ElseIf j = 2 Then
                        m_Excel.Workbooks(1).Worksheets(1).Columns(5).NumberFormat = "$#,##0.00_);[Red]($#,##0.00)"
                        objHojaExcel.Cells(i + 2, 5) = Valor
                    End If
                Next
            Next

            Item = 7
            For i = 0 To Tabla3.Columns.Count - 1
                If i = 1 Or i = 2 Then
                    objHojaExcel.Cells(1, Item) = Me.Tabla3.Columns.Item(i).HeaderCell.Value
                    Item += 1
                End If
            Next

            For i = 0 To Tabla3.RowCount - 1

                For j = 0 To Tabla3.Columns.Count - 1
                    Valor = IIf(IsDBNull(Me.Tabla3.Item(j, i).Value), "", Me.Tabla3.Item(j, i).Value)
                    If j = 1 Then
                        objHojaExcel.Cells(i + 2, 7) = Valor
                    ElseIf j = 2 Then
                        m_Excel.Workbooks(1).Worksheets(1).Columns(8).NumberFormat = "$#,##0.00_);[Red]($#,##0.00)"
                        objHojaExcel.Cells(i + 2, 8) = Valor
                    End If
                Next
            Next



            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            m_Excel.Visible = True
        Else
            RadMessageBox.Show("No hay registros a exportar....", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub
End Class