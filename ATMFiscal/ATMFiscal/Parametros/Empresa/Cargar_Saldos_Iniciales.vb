
Imports Telerik.WinControls
Public Class Cargar_Saldos_Iniciales
    Private Sub Cargar_clientes()
        Me.lstCliente.Cargar(" SELECT Id_Empresa, Razon_social FROM Empresa ")
        Me.lstCliente.SelectItem = 1
    End Sub
    Dim Ctas As New List(Of Cuentas)
    Public Class Cuentas
        Property Cuenta As String
    End Class
    Private Sub Crear_cuenta(ByVal nivel1 As String, ByVal nivel2 As String, ByVal nivel3 As String,
                             ByVal nivel4 As String, ByVal cuenta As String, ByVal descripcion As String, ByVal cliente As Integer, ByVal letra As String, ByVal RFC As String)
        Dim ds As DataSet = Eventos.Obtener_DS("select Naturaleza, clasificacion,clave,Balanza,Cta_ceros,Cta_Cargo_Cero,Cta_Abono_Cero from Catalogo_de_Cuentas where nivel1 ='" & cuenta.ToString.Substring(0, 4) & "' and Id_Empresa = " & cliente & "  ")

        If ds.Tables(0).Rows.Count > 0 Then
            Dim sql As String = ""
            sql = "INSERT INTO dbo.Catalogo_de_Cuentas "
            sql &= "("
            sql &= "Nivel1, "
            sql &= "Nivel2,"
            sql &= "Nivel3,"
            sql &= "Nivel4,"
            sql &= "Cuenta,"
            sql &= "Descripcion,"
            sql &= "Naturaleza,"
            sql &= "Clasificacion,"
            sql &= "Codigo_Agrupador,"
            If RFC <> "" Then
                sql &= "RFC,"
            Else
                sql &= "RFC,"
            End If
            sql &= "Id_Empresa,clave,  "

            sql &= "Balanza,"
            sql &= "Cta_ceros,  "
            sql &= "Cta_Cargo_Cero,"
            sql &= "Cta_Abono_Cero  "

            sql &= "	)  "
            sql &= "VALUES  "
            sql &= "	(  "
            sql &= "	'" & nivel1 & "'," '@nivel1
            sql &= "	'" & nivel2 & "'," '@nivel2
            sql &= "	'" & nivel3 & "'," '@nivel3
            sql &= "	'" & nivel4 & "'," '@nivel4
            sql &= "	'" & cuenta & "'," '@cuenta
            sql &= "	'" & Trim(descripcion.Replace("'", " ").Replace("Â", "A").Replace("´", "")) & "'," '@descripcion
            sql &= "	'" & Trim(ds.Tables(0).Rows(0)("Naturaleza")) & "'," '@naturaleza
            sql &= "	'" & Trim(ds.Tables(0).Rows(0)("Clasificacion")) & "'," '@clasificacion
            Dim su As String = ""
            If (nivel4 <> "0000" Or nivel3 <> "0000") And nivel2 <> "0000" Then
                su = nivel3.Substring(2, 2)
            End If
            sql &= "	'" & nivel1.ToString.Substring(0, 3) & su & "'," '@codigo_agrupador
            If RFC = "" Then
                sql &= "	NULL," '@RFC
            Else
                sql &= "	'" & RFC & "'," '@RFC
            End If
            sql &= "	" & cliente & " , '" & Trim(letra) & "'," '@Id_Empresa     
            sql &= "	" & IIf(IsDBNull(ds.Tables(0).Rows(0)("Balanza")) = True, 0, Eventos.Bool2(ds.Tables(0).Rows(0)("Balanza"))) & "," '@Balanza
            sql &= "	" & IIf(IsDBNull(ds.Tables(0).Rows(0)("Cta_ceros")) = True, 0, Eventos.Bool2(ds.Tables(0).Rows(0)("Cta_ceros"))) & "," '@Cta_ceros    
            sql &= "	" & IIf(IsDBNull(ds.Tables(0).Rows(0)("Cta_Cargo_Cero")) = True, 0, Eventos.Bool2(ds.Tables(0).Rows(0)("Cta_Cargo_Cero"))) & "," '@Cta_Cargo_Cero
            sql &= "	" & IIf(IsDBNull(ds.Tables(0).Rows(0)("Cta_Abono_Cero")) = True, 0, Eventos.Bool2(ds.Tables(0).Rows(0)("Cta_Abono_Cero"))) & "" '@Cta_Abono_Cero  
            sql &= "  )"
            ' Ingresar codigo para importar catalogos
            If Eventos.Comando_sql(sql) > 0 Then
                Dim C As New Cuentas
                C.Cuenta = cuenta
                Ctas.Add(C)
            End If
        Else

        End If

    End Sub


    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub Cargar_Saldos_Iniciales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_clientes()
    End Sub

    Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
        If Me.Tabla.Rows.Count > 0 Then
            Creapoliza()
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No se ha seleccionado ninguna Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
            Exit Sub
        End If
        Me.CmdLimpiar.PerformClick()
    End Sub


    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        Cargar_clientes()
        Me.Tabla.Columns.Clear()
    End Sub

    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdImportar.Click
        Try
            Eventos.LlenarDataGrid_DS(Eventos.CargarExcelXMLCatSaldosIniciales("Saldos"), Me.Tabla)
            Me.Tabla.Rows.RemoveAt(0)
            If Me.Tabla.Rows.Count > 0 Then

                Dim P As DataSet = Eventos.Obtener_DS("SELECT  cuenta   FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " ORDER BY Cuenta")
                If P.Tables(0).Rows.Count > 0 Then
                    If Ctas.Count > 0 Then
                        Ctas.Clear()
                    End If
                    For i As Integer = 0 To P.Tables(0).Rows.Count - 1
                        Ctas.Add(New Cuentas() With {.Cuenta = P.Tables(0).Rows(i)("cuenta")})
                    Next
                End If
                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                Me.Barra.Minimum = 0
                Me.Barra.Value1 = 0
                Dim cuenta As String = ""
                For i As Integer = 0 To Me.Tabla.RowCount - 1
                    If Me.Tabla.Item(0, i).Value = Nothing Then
                    Else
                        cuenta = Me.Tabla.Item(0, i).Value.ToString.Replace("-", "")
                        Me.Tabla.Item(3, i).Value = Trim(cuenta)
                        If Len(Trim(Me.Tabla.Item(3, i).Value)) = 4 Then
                            Me.Tabla.Item(3, i).Value = Me.Tabla.Item(3, i).Value & "000000000000"
                        ElseIf Len(Trim(Me.Tabla.Item(3, i).Value)) = 8 Then
                            Me.Tabla.Item(3, i).Value = Me.Tabla.Item(3, i).Value & "00000000"
                        ElseIf Len(Trim(Me.Tabla.Item(3, i).Value)) = 12 Then
                            Me.Tabla.Item(3, i).Value = Me.Tabla.Item(3, i).Value & "0000"
                        ElseIf Len(Trim(Me.Tabla.Item(3, i).Value)) = 16 Then
                        End If
                    End If
                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                        Me.Barra.Minimum = 0
                        Me.Cursor = Cursors.Arrow
                        RadMessageBox.SetThemeName("MaterialBlueGrey")
                        RadMessageBox.Show("Proceso Terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                        Me.Barra.Value1 = 0
                    Else
                        Me.Barra.Value1 += 1
                    End If
                Next
            Else
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("No se ha Importado Ningún Archivo", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            End If
            QuitacuentasceroBalanzaNormal()
        Catch ex As Exception
        End Try
    End Sub
    Private Function Es_Hija(ByVal Cta As String)
        Dim hacer As Boolean
        Dim sql As String = ""
        If Cta.Substring(4, 4) > "0000" And Cta.Substring(8, 4) > "0000" And Cta.Substring(12, 4) > "0000" Then ' es ultima cuenta
            hacer = True
        ElseIf Cta.Substring(4, 4) > "0000" And Cta.Substring(8, 4) > "0000" And Cta.Substring(12, 4) = "0000" Then ' ES TERCER NIVEL
            sql = "select * from catalogo_de_cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and Nivel1 ='" & Cta.Substring(0, 4) & "'   and Nivel2 ='" & Cta.Substring(4, 4) & "'  and Nivel3 ='" & Cta.Substring(8, 4) & "'  and Nivel4 >'0000'"
            Dim ds As DataSet = Eventos.Obtener_DS(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                hacer = False
            Else
                hacer = True
            End If
        ElseIf Cta.Substring(4, 4) > "0000" And Cta.Substring(8, 4) = "0000" And Cta.Substring(12, 4) = "0000" Then
            sql = "select * from catalogo_de_cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and Nivel1 ='" & Cta.Substring(0, 4) & "'   and Nivel2 ='" & Cta.Substring(4, 4) & "'  and (Nivel3 >'0000'  or Nivel4 >'0000')"
            Dim ds As DataSet = Eventos.Obtener_DS(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                hacer = False
            Else
                hacer = True
            End If
        End If

        Return hacer
    End Function
    Private Function Calcula_poliza(ByVal anio As Integer)
        Dim poliza As String = ""

        poliza = Eventos.Num_poliza(Me.lstCliente.SelectItem, Tip(), anio, "12", Tip())

        Return poliza
    End Function
    Private Function Tip()
        Dim tipo As String = ""
        Dim sql As String = "SELECT id_tipo_pol_sat FROM dbo.Tipos_Poliza_Sat WHERE Nombre = 'Diario' AND Id_Empresa = " & Me.lstCliente.SelectItem & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            tipo = ds.Tables(0).Rows(0)(0)
        End If
        Return tipo
    End Function
    Private Sub Creapoliza()
        Dim anio As Object = InputBox("Para que año quieres aplicar la poliza de cierre de: " & Me.lstCliente.SelectText & "", Eventos.titulo_app, Now.Year)
        If anio.ToString <> "0" Or anio <> "" Then


            Dim poliza As String = Calcula_poliza(anio)
            Dim posicion As Integer = InStr(1, poliza, "-", CompareMethod.Binary)
            Dim cuantos As Integer = Len(poliza) - Len(poliza.Substring(0, posicion))
            Dim consecutivo As Integer = Val(poliza.Substring(posicion, cuantos))
            Dim sql As String = ""
            sql &= "         INSERT INTO dbo.Polizas"
            sql &= "("
            sql &= " 	ID_poliza,      "
            sql &= "     ID_anio,        "
            sql &= "     ID_mes,        "
            sql &= "     ID_Dia,        "
            sql &= "     consecutivo,    "
            sql &= "     Fecha,          "
            sql &= "     Concepto,      "
            sql &= "     Id_Empresa,     "
            sql &= "     Fecha_captura,  "
            sql &= "     Num_Pol,  "
            sql &= "     Movto,         "
            sql &= "     Usuario,Aplicar_Poliza "
            sql &= " 	)               "
            sql &= " VALUES              "
            sql &= " 	(               "
            sql &= " 	'" & poliza & "'," '@id_poliza,         
            sql &= " 	'" & anio & "'," '@id_anio,           
            sql &= " 	'12'," '@id_mes,    
            sql &= " 	'31'," '@id_dia,      
            sql &= " 	" & consecutivo & "," '@consecutivo,       
            sql &= " 	" & Eventos.Sql_hoy("31/12/" & anio & "") & "," '@fecha,             
            sql &= " 	'Poliza Cierre'," '@concepto,          
            sql &= " 	" & Me.lstCliente.SelectItem & "," '@Id_Empresa,        
            sql &= " 	" & Eventos.Sql_hoy("31/12/" & anio & "") & "," '@fecha_captura, representa la fecha de la poliza para contabilizar    
            sql &= " 	1," '@movto,  
            sql &= " 	'A'," '@movto,             
            sql &= "  '" & Eventos.Usuario(Inicio.LblUsuario.Text) & "', 1 " '@usuario            
            sql &= " 	) "
            If ObtenerValorDB("Polizas", "id_poliza", "  Concepto = 'Poliza Cierre' AND ID_anio=" & anio & "  AND Id_Empresa =" & Me.lstCliente.SelectItem & "", True) = " " Then
                If Eventos.Comando_sql(sql) > 0 Then
                    Eventos.Insertar_usuariol("InsertarSaldoI", sql)
                    Dim cargo, abono As Decimal
                    Me.Barra.Maximum = Me.Tabla.RowCount - 1
                    Me.Barra.Minimum = 0
                    Me.Barra.Value1 = 0
                    For p As Integer = 0 To Me.Tabla.Rows.Count - 1
                        If Trim(Me.Tabla.Item(2, p).Value) = "D" Then
                            cargo = Me.Tabla.Item(4, p).Value
                            abono = 0
                        Else
                            cargo = 0
                            abono = Me.Tabla.Item(4, p).Value
                        End If
                        If Es_Hija(Trim(Me.Tabla.Item(3, p).Value)) Then

                            Dim C As New Cuentas
                            C.Cuenta = Me.Tabla.Item(3, p).Value
                            If Not Ctas.Exists(Function(x) x.Cuenta = C.Cuenta) Then
                                Dim rf() As String = Split(Me.Tabla.Item(1, p).Value, " ")
                                Dim r As String = rf(0)
                                If Len(r) = 12 Or Len(r) = 13 Then
                                    If Char.IsLetter(r.Substring(0, 3)) And IsNumeric(r.Substring(4, 5)) And (IsNumeric(r.Chars(r.Length() - 1)) Or UCase(r.Chars(r.Length() - 1)) = "A") Then
                                        r = r
                                    Else
                                        r = ""
                                    End If
                                Else
                                    r = ""
                                End If
                                Crear_cuenta(Me.Tabla.Item(3, p).Value.ToString.Substring(0, 4), Me.Tabla.Item(3, p).Value.ToString.Substring(4, 4),
                                             Me.Tabla.Item(3, p).Value.ToString.Substring(8, 4),
                            Me.Tabla.Item(3, p).Value.ToString.Substring(12, 4), Me.Tabla.Item(3, p).Value,
                            Me.Tabla.Item(1, p).Value, Me.lstCliente.SelectItem, "", r)

                            End If


                            Crea_detalle_poliza(poliza, p + 1, cargo,
                                             abono, Me.Tabla.Item(3, p).Value)
                        End If
                        If Me.Barra.Value1 = Me.Barra.Maximum Then
                            Me.Barra.Minimum = 0
                            Me.Cursor = Cursors.Arrow
                            RadMessageBox.SetThemeName("MaterialBlueGrey")
                            RadMessageBox.Show("Proceso Terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                            Me.Barra.Value1 = 0
                        Else
                            Me.Barra.Value1 += 1
                        End If
                    Next
                End If
            Else
                'verificar si existe la poliza
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                If RadMessageBox.Show("Ya existe una poliza de saldos Iniciales deseas reemplazarla ?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    If Eventos.Comando_sql("DELETE From Detalle_Polizas WHERE ID_POLIZA = (SELECT id_poliza FROM Polizas WHERE Concepto = 'Poliza Cierre' AND ID_anio=" & anio & "  AND Id_Empresa =" & Me.lstCliente.SelectItem & ")") > 0 Then
                        If Eventos.Comando_sql("DELETE FROM Polizas WHERE Concepto = 'Poliza Cierre' AND ID_anio=" & anio & "  AND Id_Empresa =" & Me.lstCliente.SelectItem & "") > 0 Then
                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("InsertarSaldoIR", sql)

                                Dim cargo, abono As Decimal
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.Rows.Count - 1

                                    If Trim(Me.Tabla.Item(2, p).Value) = "D" Then
                                        cargo = Me.Tabla.Item(4, p).Value
                                        abono = 0
                                    Else
                                        cargo = 0
                                        abono = Me.Tabla.Item(4, p).Value
                                    End If
                                    If Es_Hija(Trim(Me.Tabla.Item(3, p).Value)) Then

                                        Dim C As New Cuentas
                                        C.Cuenta = Me.Tabla.Item(3, p).Value
                                        If Ctas.Exists(Function(S) S.Cuenta = C.Cuenta) Then
                                        Else

                                            Dim rf() As String = Split(Me.Tabla.Item(1, p).Value, " ")
                                            Dim r As String = rf(0)
                                            If Len(r) = 12 Or Len(r) = 13 Then
                                                If Char.IsLetter(r.Substring(0, 3)) And IsNumeric(r.Substring(4, 5)) And (IsNumeric(r.Chars(r.Length() - 1)) Or UCase(r.Chars(r.Length() - 1)) = "A") Then
                                                    r = r
                                                Else
                                                    r = ""
                                                End If
                                            Else
                                                r = ""
                                            End If
                                            Crear_cuenta(Me.Tabla.Item(3, p).Value.ToString.Substring(0, 4), Me.Tabla.Item(3, p).Value.ToString.Substring(4, 4),
                                             Me.Tabla.Item(3, p).Value.ToString.Substring(8, 4), Me.Tabla.Item(3, p).Value.ToString.Substring(12, 4), Me.Tabla.Item(3, p).Value, Me.Tabla.Item(1, p).Value, Me.lstCliente.SelectItem, "", r)

                                        End If


                                        Crea_detalle_poliza(poliza, p + 1, cargo,
                                             abono, Me.Tabla.Item(3, p).Value)
                                    End If
                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        RadMessageBox.SetThemeName("MaterialBlueGrey")
                                        RadMessageBox.Show("Proceso Terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                                        Me.Barra.Value1 = 0
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next
                            End If
                        Else
                            RadMessageBox.SetThemeName("MaterialBlueGrey")
                            RadMessageBox.Show("No se pudo eliminar la Poliza de Saldos Iniciales", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                        End If
                    End If
                Else
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    RadMessageBox.Show("No se pudo eliminar el detalle de la Poliza de Saldos Iniciales", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                End If

            End If
        End If
    End Sub
    Private Sub Crea_detalle_poliza(ByVal id_poliza As String, ByVal item As Integer, ByVal cargo As Decimal,
                                    ByVal Abono As Decimal, ByVal cuenta As String)
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Detalle_Polizas"
        sql &= "(   "
        sql &= " ID_poliza,      "
        sql &= " ID_item,       "
        sql &= " Cargo,          "
        sql &= " Abono,         "
        sql &= " Fecha_captura,  "
        sql &= " Movto,"
        sql &= " Cuenta  "
        sql &= " )  "
        sql &= " VALUES "
        sql &= "( "
        sql &= " '" & id_poliza & "'	," '@id_poliza,     
        sql &= "" & item & "," '@id_item,       
        sql &= "" & cargo & "," '@cargo,         
        sql &= "" & Abono & "," '@abono,         
        sql &= "" & Eventos.Sql_hoy() & "," '@fecha_captura, 
        sql &= " 'A'	," '@movto,         
        sql &= " " & cuenta & "	 " '@cuenta,        
        sql &= " 	)"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InsertarSaldoID", sql)
        End If
    End Sub
    Private Sub QuitacuentasceroBalanzaNormal()
        Dim filas As Integer = Me.Tabla.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.Tabla.RowCount - 1
                If Me.Tabla.Item(4, i).Value = 0 Then
                    Me.Tabla.Rows.RemoveAt(i)
                    Exit For
                End If
            Next
        Next
    End Sub



    Private Sub CmdPlantilla_Click(sender As Object, e As EventArgs) Handles CmdPlantilla.Click
        Dim Excel As Microsoft.Office.Interop.Excel.Application = Eventos.NuevoExcel("SaldosIniciales", False)
        Eventos.Mostrar_Excel(Excel)
    End Sub
End Class