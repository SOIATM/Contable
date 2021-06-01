Imports Telerik.WinControls
Public Class CancelacionesdeIVAClientes
	Dim Ct As New List(Of Ctas)
	Dim CtIp As New List(Of CtasIP)
	Dim CtAf As New List(Of CtasAF)
	Private Sub CancelacionesdeIVAClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Eventos.DiseñoTabla(Me.TablaAF)
		Eventos.DiseñoTabla(Me.TablaAF1)
		Eventos.DiseñoTabla(Me.TablaEnero)
		Eventos.DiseñoTabla(Me.TablaFebrero)
		Eventos.DiseñoTabla(Me.TablaMarzo)
		Eventos.DiseñoTabla(Me.TablaAbril)
		Eventos.DiseñoTabla(Me.TablaMayo)
		Eventos.DiseñoTabla(Me.TablaJunio)
		Eventos.DiseñoTabla(Me.TablaJulio)
		Eventos.DiseñoTabla(Me.TablaAgosto)
		Eventos.DiseñoTabla(Me.TablaSeptiembre)
		Eventos.DiseñoTabla(Me.TablaOctubre)
		Eventos.DiseñoTabla(Me.TablaNoviembre)
		Eventos.DiseñoTabla(Me.TablaDiciembre)
		Eventos.DiseñoTabla(Me.TablaIP)
		Eventos.DiseñoTabla(Me.TablaGN)
		Eventos.DiseñoTabla(Me.TablaOP)


		Dim i As Integer
		For i = DateTime.Now.Year To DateTime.Now.Year - 10 Step -1
			If i >= 2009 Then
				Me.comboAño.Items.Add(Str(i))
			End If
		Next
		Me.comboAño.Items.Add("*")
		Me.comboAño.Text = Now.Date.Year.ToString.Trim()

		Cargar_Listas()
	End Sub

	Private Sub Cargar_Listas()
		Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & Inicio.LblUsuario.Text & "%')")
		Me.lstCliente.SelectItem = Inicio.Clt
	End Sub
    Private Function CreapolizaS(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String, ByVal dia As String, ByVal numpol As Integer,
                         ByVal consecutivo As Integer, ByVal tipo As Integer, ByVal fecha As String,
                         ByVal concepto As String, ByVal movimiento As String)
        Dim hacer As Boolean
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Polizas "
        sql &= "("
        sql &= " 	ID_poliza ,"
        sql &= "     ID_anio,"
        sql &= "     ID_mes, "
        sql &= "     ID_dia,        "
        sql &= "     Num_Pol,    "
        sql &= "     consecutivo,    "
        sql &= "     Id_Tipo_Pol_Sat,"
        sql &= "     Fecha,          "
        sql &= "     Concepto,      "
        sql &= "     Id_Empresa,     "
        sql &= "     No_Mov,        "
        sql &= "     Fecha_captura,  "
        sql &= "     Movto,         "
        sql &= "     Usuario ,Aplicar_Poliza          "
        sql &= " 	)               "
        sql &= " VALUES              "
        sql &= " 	(               "
        sql &= " 	'" & id_poliza & "'," '@id_poliza,         
        sql &= " 	" & anio & "," '@id_anio,           
        sql &= " 	'" & mes & "'," '@id_mes,      
        sql &= " 	'" & dia & "'," '@id_dia,  
        sql &= " 	'" & numpol & "'," '@id_dia,     
        sql &= " 	" & consecutivo & "," '@consecutivo,       
        sql &= " 	" & Trim(tipo) & "," '@id_tipo_poliza, 
        sql &= " 	" & Eventos.Sql_hoy(fecha) & "," '@fecha,             
        sql &= " 	'" & concepto & "'," '@concepto,          
        sql &= " 	" & Me.lstCliente.SelectItem & "," '@Id_Empresa,        
        sql &= " 	'" & movimiento & "'," '@no_mov,            
        sql &= " 	" & Eventos.Sql_hoy(fecha) & "," '@fecha_captura,     
        sql &= " 	'A'," '@movto,             
        sql &= "  '" & Eventos.Usuario(Inicio.LblUsuario.Text) & "',1" '@usuario            
        sql &= " 	) "

        If Eventos.Comando_sql(sql) > 0 Then
            hacer = True
            Eventos.Insertar_usuariol("InsertarPolizCIVA", sql)
        Else
            hacer = False
        End If
        Return hacer
    End Function
    Private Sub Crea_detalle_polizaS(ByVal id_poliza As String, ByVal item As Integer, ByVal cargo As Decimal,
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
            Eventos.Insertar_usuariol("InsertarPolizDCIVA", sql)
        End If
    End Sub
    Private Function Calcula_poliza(ByVal tipo As String, ByVal Mes As String)

        Dim poliza As String = Eventos.Num_polizaS(Me.lstCliente.SelectItem, tipo, Trim(Me.comboAño.Text), Trim(Mes), tipo)
        Return poliza
    End Function
    Private Sub CmdGuardarF_Click(sender As Object, e As EventArgs) Handles CmdGuardarF.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim Arreglo(11) As Windows.Forms.DataGridView

        If lstCliente.SelectText <> "" Then

            Arreglo(0) = TablaEnero
            Arreglo(1) = TablaFebrero
            Arreglo(2) = TablaMarzo
            Arreglo(3) = TablaAbril
            Arreglo(4) = TablaMayo
            Arreglo(5) = TablaJunio
            Arreglo(6) = TablaJulio
            Arreglo(7) = TablaAgosto
            Arreglo(8) = TablaSeptiembre
            Arreglo(9) = TablaOctubre
            Arreglo(10) = TablaNoviembre
            Arreglo(11) = TablaDiciembre
            Dim i As Integer = 1
            Dim Item As Integer = 1
            For Each Tabla As Windows.Forms.DataGridView In Arreglo
                If Tabla.Rows.Count > 0 Then
                    Dim Tipo As String = ObtenerValorDB("Tipos_Poliza_Sat", "Id_Tipo_Pol_Sat", " Clave = 001 AND Id_Empresa = " & Me.lstCliente.SelectItem & "", True)
                    Dim Poliza As String = Calcula_poliza(Tipo, IIf(Len(i) = 1, "0" & i, i))
                    Dim posicion As Integer = InStr(1, Poliza, "-", CompareMethod.Binary)
                    Dim cuantos As Integer = Len(Poliza) - Len(Poliza.Substring(0, posicion))
                    Dim Consecutivo As Integer = Val(Poliza.Substring(posicion, cuantos))
                    If Buscar_Poliza(Trim(Me.comboAño.Text), Trim(IIf(Len(i) = 1, "0" & i, i)), Me.lstCliente.SelectItem) = True Then
                        Dim FECHA As String = DateTime.DaysInMonth(Trim(Me.comboAño.Text), Trim(IIf(Len(i) = 1, "0" & i, i))) & "/" & Trim(IIf(Len(i.ToString) = 1, "0" & i.ToString, i.ToString)) & "/" & Trim(Me.comboAño.Text)
                        If CreapolizaS(Poliza, Trim(Me.comboAño.Text), Trim(IIf(Len(i.ToString) = 1, "0" & i.ToString, i.ToString)), DateTime.DaysInMonth(Trim(Me.comboAño.Text), Trim(IIf(Len(i) = 1, "0" & i, i))),
                                       Numero(Trim(Me.comboAño.Text), Trim(IIf(Len(i.ToString) = 1, "0" & i.ToString, i.ToString)), Tipo, Me.lstCliente.SelectItem),
                                       Consecutivo, Tipo, FECHA, "Cancelacion de IVA Trasladado VS Acreditable", "") = True Then
                            For j As Integer = 0 To Tabla.Rows.Count - 1
                                Crea_detalle_polizaS(Poliza, Item, Tabla.Item(Car.Index, j).Value, Tabla.Item(Abon.Index, j).Value, Tabla.Item(cta.Index, j).Value, "")
                                Item += 1
                            Next
                            Me.TablaCancelaciones.SelectedIndex = i - 1
                            Me.TablaCancelaciones_SelectedIndexChanged(Me.TablaEnero, Nothing)
                            System.Threading.Thread.Sleep(3000)
                            'Crear detalle de polzias
                            If Me.TablaAF.Rows.Count > 0 Then
                                If Me.TablaAF.Item(0, 0).Value IsNot Nothing Then
                                    If Me.TablaAF.Item(1, 0).Value > 0 Then
                                        If Me.TablaIP.Rows.Count > 0 Then
                                            Crea_detalle_polizaS(Poliza, Item, 0, Me.TablaAF.Item(1, 0).Value, Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""), "")
                                            Item += 1
                                        Else
                                            Crea_detalle_polizaS(Poliza, Item, Me.TablaAF.Item(1, 0).Value, 0, Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""), "")
                                            Item += 1
                                        End If
                                    End If
                                End If
                            End If
                            If Me.TablaAF1.Rows.Count > 0 Then
                                For p As Integer = 0 To Me.TablaAF1.RowCount - 1
                                    If Me.TablaAF1.Item(1, p).Value > 0 Then
                                        Crea_detalle_polizaS(Poliza, Item, 0, Me.TablaAF1.Item(1, p).Value, Me.TablaAF1.Item(0, p).Value.ToString.Substring(0, 19).Replace("-", ""), "")

                                    End If
                                    Item += 1
                                Next
                            End If
                            If Me.TablaIP.Rows.Count = 1 Then
                                If Me.TablaIP.Item(0, 0).Value <> "" Then
                                    If Me.TablaIP.Item(1, 0).Value > 0 Then
                                        Crea_detalle_polizaS(Poliza, Item, 0, Me.TablaIP.Item(1, 0).Value, Me.TablaIP.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""), "")
                                        Item += 1
                                    End If
                                End If
                            End If


                            'Verificar cargo o abono
                            If Me.TablaGN.Rows.Count = 1 Then
                                If Me.TablaGN.Item(0, 0).Value IsNot Nothing Then
                                    If Me.TablaGN.Item(1, 0).Value > 0 Then
                                        Crea_detalle_polizaS(Poliza, Item, Me.TablaGN.Item(1, 0).Value, 0, Me.TablaGN.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""), "")
                                        Item += 1
                                    End If
                                End If
                            End If
                            'Verificar cargo o abono
                            If Me.TablaOP.Rows.Count = 1 Then
                                If Me.TablaOP.Item(0, 0).Value IsNot Nothing Then
                                    If Me.TablaOP.Item(1, 0).Value > 0 Then
                                        Crea_detalle_polizaS(Poliza, Item, 0, Me.TablaOP.Item(1, 0).Value, Me.TablaOP.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""), "") '
                                        Item += 1
                                    End If
                                End If
                            End If

                        End If

                    Else
                        If MessageBox.Show("La poliza ya existe en el mes  " & Eventos.MesEnletra(IIf(Len(i.ToString) = 1, "0" & i.ToString, i.ToString)) & " deseas Reemplazarla ?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            If Elimina_Poliza(Trim(Me.comboAño.Text), Trim(IIf(Len(i.ToString) = 1, "0" & i.ToString, i.ToString)), Me.lstCliente.SelectItem) = True Then
                                Dim FECHA As String = DateTime.DaysInMonth(Trim(Me.comboAño.Text), Trim(IIf(Len(i) = 1, "0" & i, i))) & "/" & Trim(IIf(Len(i.ToString) = 1, "0" & i.ToString, i.ToString)) & "/" & Trim(Me.comboAño.Text)

                                If CreapolizaS(Poliza, Trim(Me.comboAño.Text), Trim(IIf(Len(i.ToString) = 1, "0" & i.ToString, i.ToString)), DateTime.DaysInMonth(Trim(Me.comboAño.Text), Trim(IIf(Len(i) = 1, "0" & i, i))),
                                           Numero(Trim(Me.comboAño.Text), Trim(IIf(Len(i) = 1, "0" & i, i)), Tipo, Me.lstCliente.SelectItem),
                                           Consecutivo, Tipo, FECHA, "Cancelacion de IVA Trasladado VS Acreditable", "") = True Then

                                    If Tabla.Rows.Count > 0 Then
                                        For j As Integer = 0 To Tabla.Rows.Count - 1
                                            Crea_detalle_polizaS(Poliza, Item, Tabla.Item(Car.Index, j).Value, Tabla.Item(Abon.Index, j).Value, Tabla.Item(cta.Index, j).Value, "")
                                            Item += 1
                                        Next
                                    End If
                                    Me.TablaCancelaciones.SelectedIndex = i - 1
                                    Me.TablaCancelaciones_SelectedIndexChanged(Me.TablaEnero, Nothing)
                                    System.Threading.Thread.Sleep(3000)
                                    If Me.TablaAF.Rows.Count > 0 Then
                                        If Me.TablaAF.Item(0, 0).Value IsNot Nothing Then
                                            If Me.TablaAF.Item(1, 0).Value > 0 Then
                                                If Me.TablaIP.Rows.Count > 0 Then
                                                    Crea_detalle_polizaS(Poliza, Item, 0, Me.TablaAF.Item(1, 0).Value, Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""), "")
                                                    Item += 1
                                                Else
                                                    Crea_detalle_polizaS(Poliza, Item, Me.TablaAF.Item(1, 0).Value, 0, Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""), "")
                                                    Item += 1
                                                End If
                                            End If
                                        End If
                                    End If
                                    If Me.TablaAF1.Rows.Count > 0 Then
                                        For p As Integer = 0 To Me.TablaAF1.RowCount - 1
                                            If Me.TablaAF1.Item(1, p).Value > 0 Then
                                                Crea_detalle_polizaS(Poliza, Item, 0, Me.TablaAF1.Item(1, p).Value, Me.TablaAF1.Item(0, p).Value.ToString.Substring(0, 19).Replace("-", ""), "")
                                                Item += 1
                                            End If
                                        Next
                                    End If
                                    If Me.TablaIP.Rows.Count = 1 Then
                                        If Me.TablaIP.Item(0, 0).Value <> "" Then
                                            If Me.TablaIP.Item(1, 0).Value > 0 Then
                                                Crea_detalle_polizaS(Poliza, Item, 0, Me.TablaIP.Item(1, 0).Value, Me.TablaIP.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""), "")
                                                Item += 1
                                            End If
                                        End If
                                    End If
                                    'Verificar cargo o abono
                                    If Me.TablaGN.Rows.Count = 1 Then
                                        If Me.TablaGN.Item(0, 0).Value IsNot Nothing Then
                                            If Me.TablaGN.Item(1, 0).Value > 0 Then
                                                Crea_detalle_polizaS(Poliza, Item, Me.TablaGN.Item(1, 0).Value, 0, Me.TablaGN.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""), "")
                                                Item += 1
                                            End If
                                        End If
                                    End If
                                    'Verificar cargo o abono
                                    If Me.TablaOP.Rows.Count = 1 Then
                                        If Me.TablaOP.Item(0, 0).Value IsNot Nothing Then
                                            If Me.TablaOP.Item(1, 0).Value > 0 Then
                                                Crea_detalle_polizaS(Poliza, Item, 0, Me.TablaOP.Item(1, 0).Value, Me.TablaOP.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""), "") '
                                                Item += 1
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                    i += 1
                End If
            Next
            Me.TablaCancelaciones.SelectedIndex = 0
            Me.TablaCancelaciones_SelectedIndexChanged(Me.TablaEnero, Nothing)
            System.Threading.Thread.Sleep(3000)

            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

        Else

        End If

    End Sub
    Private Function Numero(ByVal Anio As String, ByVal Mes As String, ByVal tipo As String, ByVal cliente As Integer)
        Dim Num As Integer
        Dim Sql As String = "SELECT max (Num_Pol)+1 AS Numero FROM Polizas WHERE ID_anio = " & Trim(Anio) & " AND ID_mes = " & Trim(Mes) & " AND Id_Tipo_Pol_Sat = " & Trim(tipo) & " AND Id_Empresa = " & cliente & ""
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Num = IIf(IsDBNull(ds.Tables(0).Rows(0)(0)) = True, 1, ds.Tables(0).Rows(0)(0))
        Else
            Num = 1
        End If


        Return Num
    End Function
    Private Function Buscar_Poliza(ByVal Anio As String, ByVal Mes As String, ByVal Cliente As Integer)
        Dim Hacer As Boolean
        Dim sql As String = "Select * from Polizas where Concepto = 'Cancelacion de IVA Trasladado VS Acreditable' and ID_Anio= " & Anio & " and ID_Mes =" & Mes & " and Id_Empresa = " & Cliente & " "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Hacer = False
        Else
            Hacer = True
        End If
        Return Hacer
    End Function
    Private Function Elimina_Poliza(ByVal Anio As String, ByVal Mes As String, ByVal Cliente As Integer)
        Dim Hacer As Boolean
        Dim sql As String = "Select * from Polizas where Concepto ='Cancelacion de IVA Trasladado VS Acreditable' and ID_Anio= " & Anio & " and ID_Mes =" & Mes & " and Id_Empresa = " & Cliente & " "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            sql = " DELETE From Detalle_Polizas  WHERE ID_poliza  = '" & Trim(ds.Tables(0).Rows(0)("ID_poliza")) & "' "
            If Eventos.Comando_sql(sql) >= 0 Then

            End If
            sql = "DELETE From Polizas   WHERE ID_poliza  = '" & Trim(ds.Tables(0).Rows(0)("ID_poliza")) & "'"
            If Eventos.Comando_sql(sql) > 0 Then
                Eventos.Insertar_usuariol("EliminaPolizas", sql)
                Hacer = True
            Else
                Hacer = False
            End If
        Else
            Hacer = True
        End If
        Return Hacer
    End Function
    Private Sub CmdNuevoF_Click(sender As Object, e As EventArgs) Handles CmdNuevoF.Click
        Dim Sql As String = "SELECT Catalogo_de_Cuentas.Cuenta, Catalogo_de_Cuentas.Descripcion,CancelacionesIVA.Abono FROM dbo.CancelacionesIVA INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = CancelacionesIVA.Id_cat_Cuentas where CancelacionesIVA.Id_Empresa= " & Me.lstCliente.SelectItem & " order by Cuenta"

        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        For j As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Me.TablaEnero.RowCount = ds.Tables(0).Rows.Count
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

            Me.TablaEnero.Item(0, j).Value = ds.Tables(0).Rows(j)("Cuenta")
            Me.TablaEnero.Item(1, j).Value = ds.Tables(0).Rows(j)("Descripcion")
            Me.TablaEnero.Item(2, j).Value = 0
            Me.TablaEnero.Item(3, j).Value = 0
            Me.TablaEnero.Item(Ab.Index, j).Value = ds.Tables(0).Rows(j)("Abono")

            Me.TablaFebrero.Item(0, j).Value = ds.Tables(0).Rows(j)("Cuenta")
            Me.TablaFebrero.Item(1, j).Value = ds.Tables(0).Rows(j)("Descripcion")
            Me.TablaFebrero.Item(2, j).Value = 0
            Me.TablaFebrero.Item(3, j).Value = 0
            Me.TablaFebrero.Item(Ab.Index, j).Value = ds.Tables(0).Rows(j)("Abono")

            Me.TablaMarzo.Item(0, j).Value = ds.Tables(0).Rows(j)("Cuenta")
            Me.TablaMarzo.Item(1, j).Value = ds.Tables(0).Rows(j)("Descripcion")
            Me.TablaMarzo.Item(2, j).Value = 0
            Me.TablaMarzo.Item(3, j).Value = 0
            Me.TablaMarzo.Item(Ab.Index, j).Value = ds.Tables(0).Rows(j)("Abono")

            Me.TablaAbril.Item(0, j).Value = ds.Tables(0).Rows(j)("Cuenta")
            Me.TablaAbril.Item(1, j).Value = ds.Tables(0).Rows(j)("Descripcion")
            Me.TablaAbril.Item(2, j).Value = 0
            Me.TablaAbril.Item(3, j).Value = 0
            Me.TablaAbril.Item(Ab.Index, j).Value = ds.Tables(0).Rows(j)("Abono")

            Me.TablaMayo.Item(0, j).Value = ds.Tables(0).Rows(j)("Cuenta")
            Me.TablaMayo.Item(1, j).Value = ds.Tables(0).Rows(j)("Descripcion")
            Me.TablaMayo.Item(2, j).Value = 0
            Me.TablaMayo.Item(3, j).Value = 0
            Me.TablaMayo.Item(Ab.Index, j).Value = ds.Tables(0).Rows(j)("Abono")

            Me.TablaJunio.Item(0, j).Value = ds.Tables(0).Rows(j)("Cuenta")
            Me.TablaJunio.Item(1, j).Value = ds.Tables(0).Rows(j)("Descripcion")
            Me.TablaJunio.Item(2, j).Value = 0
            Me.TablaJunio.Item(3, j).Value = 0
            Me.TablaJunio.Item(Ab.Index, j).Value = ds.Tables(0).Rows(j)("Abono")


            Me.TablaJulio.Item(0, j).Value = ds.Tables(0).Rows(j)("Cuenta")
            Me.TablaJulio.Item(1, j).Value = ds.Tables(0).Rows(j)("Descripcion")
            Me.TablaJulio.Item(2, j).Value = 0
            Me.TablaJulio.Item(3, j).Value = 0
            Me.TablaJulio.Item(Ab.Index, j).Value = ds.Tables(0).Rows(j)("Abono")

            Me.TablaAgosto.Item(0, j).Value = ds.Tables(0).Rows(j)("Cuenta")
            Me.TablaAgosto.Item(1, j).Value = ds.Tables(0).Rows(j)("Descripcion")
            Me.TablaAgosto.Item(2, j).Value = 0
            Me.TablaAgosto.Item(3, j).Value = 0
            Me.TablaAgosto.Item(Ab.Index, j).Value = ds.Tables(0).Rows(j)("Abono")

            Me.TablaSeptiembre.Item(0, j).Value = ds.Tables(0).Rows(j)("Cuenta")
            Me.TablaSeptiembre.Item(1, j).Value = ds.Tables(0).Rows(j)("Descripcion")
            Me.TablaSeptiembre.Item(2, j).Value = 0
            Me.TablaSeptiembre.Item(3, j).Value = 0
            Me.TablaSeptiembre.Item(Ab.Index, j).Value = ds.Tables(0).Rows(j)("Abono")


            Me.TablaOctubre.Item(0, j).Value = ds.Tables(0).Rows(j)("Cuenta")
            Me.TablaOctubre.Item(1, j).Value = ds.Tables(0).Rows(j)("Descripcion")
            Me.TablaOctubre.Item(2, j).Value = 0
            Me.TablaOctubre.Item(3, j).Value = 0
            Me.TablaOctubre.Item(Ab.Index, j).Value = ds.Tables(0).Rows(j)("Abono")

            Me.TablaNoviembre.Item(0, j).Value = ds.Tables(0).Rows(j)("Cuenta")
            Me.TablaNoviembre.Item(1, j).Value = ds.Tables(0).Rows(j)("Descripcion")
            Me.TablaNoviembre.Item(2, j).Value = 0
            Me.TablaNoviembre.Item(3, j).Value = 0
            Me.TablaNoviembre.Item(Ab.Index, j).Value = ds.Tables(0).Rows(j)("Abono")

            Me.TablaDiciembre.Item(0, j).Value = ds.Tables(0).Rows(j)("Cuenta")
            Me.TablaDiciembre.Item(1, j).Value = ds.Tables(0).Rows(j)("Descripcion")
            Me.TablaDiciembre.Item(2, j).Value = 0
            Me.TablaDiciembre.Item(3, j).Value = 0
            Me.TablaDiciembre.Item(Ab.Index, j).Value = ds.Tables(0).Rows(j)("Abono")

        Next

        Buscar_Listas()
        If Me.TablaAF.Rows.Count > 0 Then
            Me.TablaAF.Rows.Clear()
        End If
        Me.TablaAF.Rows.Add()
        If Me.TablaAF1.Rows.Count > 0 Then
            Me.TablaAF1.Rows.Clear()
        End If
        Me.TablaAF1.Rows.Add()
        If Me.TablaIP.Rows.Count > 0 Then
            Me.TablaIP.Rows.Clear()
        End If
        Me.TablaIP.Rows.Add()
        If Me.TablaGN.Rows.Count > 0 Then
            Me.TablaGN.Rows.Clear()
        End If
        Me.TablaGN.Rows.Add()
        If Me.TablaOP.Rows.Count > 0 Then
            Me.TablaOP.Rows.Clear()
        End If
        Me.TablaOP.Rows.Add()

    End Sub

    Private Sub Buscar_Listas()
        Dim sql As String = "  SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias  
                            FROM Catalogo_de_Cuentas WHERE Nivel1 ='1130' AND Nivel2 ='0001'  AND Nivel3 >0 and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        Dim RP As DataSet = Eventos.Obtener_DS(sql)
        If RP.Tables(0).Rows.Count > 0 Then

            If Me.CtaAf.Items.Count = 0 Then
                For i As Integer = 0 To RP.Tables(0).Rows.Count - 1
                    Me.CtaAf.Items.Add(RP.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If

            If Me.CtaAf1.Items.Count = 0 Then
                For i As Integer = 0 To RP.Tables(0).Rows.Count - 1
                    Me.CtaAf1.Items.Add(RP.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If

        End If
        RP.Clear()
        sql = "  SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias  
                            FROM Catalogo_de_Cuentas WHERE Nivel1 in ('2130') AND Nivel2 ='0001'  and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        RP = Eventos.Obtener_DS(sql)
        If RP.Tables(0).Rows.Count > 0 Then

            If Me.CtaIP.Items.Count = 0 Then
                For i As Integer = 0 To RP.Tables(0).Rows.Count - 1
                    Me.CtaIP.Items.Add(RP.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If
        End If

        RP.Clear()
        sql = "  SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias  
                            FROM Catalogo_de_Cuentas WHERE Nivel1 ='6120' AND Nivel2 ='0001' AND Nivel3 >0  and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        RP = Eventos.Obtener_DS(sql)
        If RP.Tables(0).Rows.Count > 0 Then

            If Me.CtaGN.Items.Count = 0 Then
                For i As Integer = 0 To RP.Tables(0).Rows.Count - 1
                    Me.CtaGN.Items.Add(RP.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If
        End If
        RP.Clear()
        sql = "  SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias  
                            FROM Catalogo_de_Cuentas WHERE Nivel1 ='7040' AND Nivel2 ='0023' AND Nivel3 >0  and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        RP = Eventos.Obtener_DS(sql)
        If RP.Tables(0).Rows.Count > 0 Then

            If Me.CtaOP.Items.Count = 0 Then
                For i As Integer = 0 To RP.Tables(0).Rows.Count - 1
                    Me.CtaOP.Items.Add(RP.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If
        End If
    End Sub
    Private Function Buscar_cuenta(ByVal Cta As String)
        Dim Nombre As String = ""
        Nombre = "SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias FROM Catalogo_de_Cuentas WHERE   Cuenta =" & Cta & ""
        Dim ds As DataSet = Eventos.Obtener_DS(Nombre)
        If ds.Tables(0).Rows.Count > 0 Then
            Nombre = ds.Tables(0).Rows(0)(0)
        Else
            Nombre = ""
        End If
        Return Nombre
    End Function
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
    Private Sub BuscarCargos(ByVal Anio As String, ByVal Mes As String, ByVal cta As String, ByVal Tab As Windows.Forms.DataGridView, ByVal posi As Integer)
        Dim sql As String = " SELECT CASE WHEN  sum (cargo) IS NULL THEN 0 ELSE sum (cargo) END AS Cargos FROM Detalle_Polizas 
                                INNER JOIN Polizas ON Polizas.ID_poliza=Detalle_Polizas.ID_poliza 
                                WHERE Detalle_Polizas.Cuenta =" & cta & "  AND Detalle_Polizas.Cargo > 0 
                                AND Polizas.ID_anio = " & Trim(Anio) & " AND Polizas.ID_mes = " & Trim(Mes) & " AND Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Tab.Item(3, posi).Value = ds.Tables(0).Rows(0)("Cargos")
        Else
            Tab.Item(3, posi).Value = 0
        End If
    End Sub
    Private Sub BuscarAbonos(ByVal Anio As String, ByVal mes As String, ByVal cta As String, ByVal Tab As Windows.Forms.DataGridView, ByVal posi As Integer)
        Dim sql As String = " SELECT CASE WHEN  sum (Abono) IS NULL THEN 0 ELSE sum (Abono) END AS Abonos FROM Detalle_Polizas 
                                INNER JOIN Polizas ON Polizas.ID_poliza=Detalle_Polizas.ID_poliza 
                                WHERE Detalle_Polizas.Cuenta =" & cta & "  AND Detalle_Polizas.Abono > 0 
                                AND Polizas.ID_anio = " & Trim(Anio) & " AND Polizas.ID_mes = " & Trim(mes) & " AND Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Tab.Item(2, posi).Value = ds.Tables(0).Rows(0)("Abonos")
        Else
            Tab.Item(2, posi).Value = 0
        End If
    End Sub
    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub
    Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Try
            Ct.Clear()
        Catch ex As Exception

        End Try
        If Me.TablaAF.Rows.Count > 0 Or Me.TablaAF1.Rows.Count Then
            If Me.TablaEnero.Rows.Count > 1 Then
                If Me.TablaAF.Item(CtaAf.Index, 0).Value <> "" Or Me.TablaAF.Item(CtaAf1.Index, 0).Value <> "" Then
                    Dim arreglo(11) As Windows.Forms.DataGridView
                    arreglo(0) = TablaEnero
                    arreglo(1) = TablaFebrero
                    arreglo(2) = TablaMarzo
                    arreglo(3) = TablaAbril
                    arreglo(4) = TablaMayo
                    arreglo(5) = TablaJunio
                    arreglo(6) = TablaJulio
                    arreglo(7) = TablaAgosto
                    arreglo(8) = TablaSeptiembre
                    arreglo(9) = TablaOctubre
                    arreglo(10) = TablaNoviembre
                    arreglo(11) = TablaDiciembre

                    Dim i As Integer = 1
                    For Each tabla As Windows.Forms.DataGridView In arreglo
                        For j As Integer = 0 To tabla.Rows.Count - 1
                            If tabla.Item(Ab.Index, j).Value = True Then
                                BuscarCargos(Me.comboAño.Text, IIf(Len(i) = 1, "0" & i, i), tabla.Item(0, j).Value, tabla, j)
                            Else
                                BuscarAbonos(Me.comboAño.Text, IIf(Len(i) = 1, "0" & i, i), tabla.Item(0, j).Value, tabla, j)
                            End If
                        Next
                        i += 1
                        Calcula_Saldo_FavorAnteriores(i, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))

                        'Calcula_Saldo_Favor(i, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))
                    Next
                    For Each tabla As Windows.Forms.DataGridView In arreglo
                        QuitacuentasceroBalanzaNormal(tabla)
                    Next

                    For s As Integer = 0 To Me.TablaAF1.Rows.Count - 1
                        Ct.Add(New CancelacionesdeIVAClientes.Ctas() With {.Cta = Me.TablaAF1.Item(0, s).Value.ToString()})
                    Next
                    For s As Integer = 0 To Me.TablaIP.Rows.Count - 1
                        CtIp.Add(New CancelacionesdeIVAClientes.CtasIP() With {.Cta = Me.TablaIP.Item(0, s).Value.ToString()})
                    Next
                    For s As Integer = 0 To Me.TablaAF.Rows.Count - 1
                        CtAf.Add(New CancelacionesdeIVAClientes.CtasAF() With {.Cta = Me.TablaAF.Item(0, s).Value.ToString()})
                    Next
                    Me.TablaCancelaciones_SelectedIndexChanged(Me.TablaEnero, Nothing)


                Else
                    RadMessageBox.Show("Debes seleccionar las cuentas a Favor y por Pagar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                End If
            Else
                RadMessageBox.Show("Debes precionar primero el Boton de Nuevo...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            End If
        End If
    End Sub
    Private Sub QuitacuentasceroBalanzaNormal(ByVal Tabla As DataGridView)
        Dim filas As Integer = Tabla.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Tabla.RowCount - 1
                If Convert.ToDecimal(Tabla.Item(Car.Index, i).Value) = 0 And Convert.ToDecimal(Tabla.Item(Abon.Index, i).Value) = 0 Then
                    Tabla.Rows.RemoveAt(i)
                    Exit For
                End If
            Next
        Next
    End Sub
    Private Function CalculaAf(ByVal Cuenta As String, ByVal mes As String)
        Dim saldo As Decimal = 0
        Dim sql As String = " SELECT CASE WHEN  sum (cargo) IS NULL THEN 0 ELSE sum (cargo) END AS Cargos FROM Detalle_Polizas 
                                INNER JOIN Polizas ON Polizas.ID_poliza=Detalle_Polizas.ID_poliza 
                                WHERE Detalle_Polizas.Cuenta =" & Cuenta.Replace("-", "").Substring(0, 16) & "  AND Detalle_Polizas.Cargo > 0 
                                AND Polizas.ID_anio = " & Me.comboAño.Text.Trim() & " AND Polizas.ID_mes = " & Trim(mes) & " AND Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            saldo = ds.Tables(0).Rows(0)("Cargos")
        End If
        Return saldo
    End Function
    Private Function CalculaIP(ByVal Cuenta As String, ByVal mes As String)
        Dim saldo As Decimal = 0
        Dim sql As String = " SELECT CASE WHEN  sum (Abono) IS NULL THEN 0 ELSE sum (Abono) END AS Abonos FROM Detalle_Polizas 
                                INNER JOIN Polizas ON Polizas.ID_poliza=Detalle_Polizas.ID_poliza 
                                WHERE Detalle_Polizas.Cuenta =" & Cuenta.Replace("-", "").Substring(0, 16) & "  AND Detalle_Polizas.Abono > 0 
                                AND Polizas.ID_anio = " & Me.comboAño.Text.Trim() & " AND Polizas.ID_mes = " & Trim(mes) & " AND Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            saldo = ds.Tables(0).Rows(0)("Abonos")
        End If
        Return saldo
    End Function
    Private Function CalculaGN(ByVal Cuenta As String, ByVal mes As String)
        Dim saldo As Decimal = 0
        Dim sql As String = ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then


        End If
        Return saldo
    End Function
    Private Function CalculaOP(ByVal Cuenta As String, ByVal mes As String)
        Dim saldo As Decimal = 0
        Dim sql As String = ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then


        End If
        Return saldo
    End Function
    Private Class Ctas
        Public Property Cta As String
    End Class
    Private Class CtasIP
        Public Property Cta As String
    End Class
    Private Class CtasAF
        Public Property Cta As String
    End Class
    Private Sub ANTERIOR()
        Try
            If Me.TablaAF.Item(CtaAf.Index, 0).Value IsNot Nothing And Me.TablaIP.Item(CtaIP.Index, 0).Value IsNot Nothing Then
                Dim Importe As Decimal = 0
                Dim Diferencia As Decimal = 0
                Dim Separador() As String

                Dim Valor As Decimal = 0
                Dim A, B As Decimal
                Select Case Me.TablaCancelaciones.SelectedIndex
                    Case 0
                        Try


                            For i As Integer = 0 To Me.TablaEnero.Rows.Count - 1
                                A = A + Me.TablaEnero.Item(Car.Index, i).Value
                                B = B + Me.TablaEnero.Item(Abon.Index, i).Value
                            Next
                            If A > B Then
                                Me.TablaIP.Item(ImpIP.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                                Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                                B = B + Me.TablaIP.Item(ImpIP.Index, 0).Value
                            ElseIf A < B Then
                                Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                                Me.TablaAF.Item(ImpAF.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                                A = A + Me.TablaAF.Item(ImpAF.Index, 0).Value
                            Else
                                Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                                Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                            End If


                            Importe = A - B
                            If Importe < 0 Then
                                Me.TablaGN.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                                Me.TablaOP.Item(1, 0).Value = 0
                            ElseIf Importe > 0 Then
                                Me.TablaGN.Item(1, 0).Value = 0
                                Me.TablaOP.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                            Else
                                Me.TablaGN.Item(1, 0).Value = 0
                                Me.TablaOP.Item(1, 0).Value = 0
                            End If

                        Catch ex As Exception

                        End Try
                        Importe = 0
                        For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1
                            Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(1, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Importe += Me.TablaAF1.Item(1, i).Value
                        Next

                    'Calcula_Saldo_Favor(13, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim() - 1, Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))
                    Case 1
                        For i As Integer = 0 To Me.TablaFebrero.Rows.Count - 1
                            A = A + Me.TablaFebrero.Item(Car.Index, i).Value
                            B = B + Me.TablaFebrero.Item(Abon.Index, i).Value
                        Next
                        If A > B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                            B = B + Me.TablaIP.Item(ImpIP.Index, 0).Value
                        ElseIf A < B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            A = A + Me.TablaAF.Item(ImpAF.Index, 0).Value

                        Else
                            If A = 0 And B = 0 Then
                                Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                                Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            End If
                        End If


                        Importe = A - B
                        If Importe < 0 Then
                            Me.TablaGN.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                            Me.TablaOP.Item(1, 0).Value = 0
                        ElseIf Importe > 0 Then
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                        Else
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = 0
                        End If

                        Importe = 0
                        For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1
                            Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(2, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Importe += Me.TablaAF1.Item(1, i).Value
                        Next
'Calcula_Saldo_Favor(2, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))
                    Case 2

                        For i As Integer = 0 To Me.TablaMarzo.Rows.Count - 1 ' Calcula si es a favor o por pagar
                            A = A + Me.TablaMarzo.Item(Car.Index, i).Value
                            B = B + Me.TablaMarzo.Item(Abon.Index, i).Value
                        Next
                        Me.TablaAF1.RowCount = Ct.Count
                        For i As Integer = 0 To Ct.Count - 1 'Se recuerdan las  cuentas a favor años anteriores
                            Me.TablaAF1.Item(0, i).Value = Obtener_Index(Ct(i).Cta, Me.CtaAf1)
                        Next

                        'For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1 'Calcula es saldo actual de las cuentas a favor años anteriores
                        '    Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(3, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                        'Next

                        Me.TablaIP.Item(ImpIP.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                        If Me.TablaAF1.Rows.Count > 0 Then

                            If A > B Then ' si cargos son mayores a abonos
                                For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1
                                    If Me.TablaIP.Item(ImpIP.Index, 0).Value < Me.TablaAF1.Item(1, 0).Value Then
                                        Me.TablaAF1.Rows.Clear()
                                        Me.TablaAF1.RowCount = 1
                                        Me.TablaAF1.Item(1, 0).Value = Ct(0)
                                        Me.TablaAF1.Item(1, 0).Value = Me.TablaIP.Item(ImpIP.Index, 0).Value
                                        Me.TablaIP.Rows.Clear()
                                        Exit For
                                    ElseIf Me.TablaIP.Item(ImpIP.Index, 0).Value > Me.TablaAF1.Item(1, i).Value Then
                                        Me.TablaIP.Item(ImpIP.Index, 0).Value -= Me.TablaAF1.Item(1, i).Value
                                    Else
                                        Me.TablaAF1.RowCount = i
                                        Me.TablaAF1.Item(1, i).Value = Me.TablaIP.Item(ImpIP.Index, 0).Value
                                        Me.TablaIP.Rows.Clear()
                                    End If

                                Next


                            ElseIf A < B Then
                                Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                                Me.TablaAF.Item(ImpAF.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                                A = A + Me.TablaAF.Item(ImpAF.Index, 0).Value
                                Try
                                    Me.TablaAF1.Rows.Clear()
                                Catch ex As Exception

                                End Try
                            Else
                                If A = 0 And B = 0 Then
                                    Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                                    Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                                End If
                            End If
                        Else
                            If A > B Then
                                Me.TablaIP.Item(ImpIP.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                                Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                                B = B + Me.TablaIP.Item(ImpIP.Index, 0).Value
                            ElseIf A < B Then
                                Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                                Me.TablaAF.Item(ImpAF.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                                A = A + Me.TablaAF.Item(ImpAF.Index, 0).Value
                            Else
                                If A = 0 And B = 0 Then
                                    Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                                    Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                                End If
                            End If
                        End If

                        Importe = A - B
                        If Importe < 0 Then
                            Me.TablaGN.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                            Me.TablaOP.Item(1, 0).Value = 0
                        ElseIf Importe > 0 Then
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                        Else
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = 0
                        End If
                        'Calcula_Saldo_Favor(3, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))
                        Importe = 0

                    Case 3
                        For i As Integer = 0 To Me.TablaAbril.Rows.Count - 1
                            A = A + Me.TablaAbril.Item(Car.Index, i).Value
                            B = B + Me.TablaAbril.Item(Abon.Index, i).Value
                        Next
                        If A > B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                            B = B + Me.TablaIP.Item(ImpIP.Index, 0).Value
                        ElseIf A < B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            A = A + Me.TablaAF.Item(ImpAF.Index, 0).Value
                        Else
                            If A = 0 And B = 0 Then
                                Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                                Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            End If
                        End If


                        Importe = A - B
                        If Importe < 0 Then
                            Me.TablaGN.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                            Me.TablaOP.Item(1, 0).Value = 0
                        ElseIf Importe > 0 Then
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                        Else
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = 0
                        End If
                        'Calcula_Saldo_Favor(4, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))
                        Importe = 0
                        For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1
                            Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(4, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Importe += Me.TablaAF1.Item(1, i).Value
                        Next
                    Case 4
                        For i As Integer = 0 To Me.TablaMayo.Rows.Count - 1
                            A = A + Me.TablaMayo.Item(Car.Index, i).Value
                            B = B + Me.TablaMayo.Item(Abon.Index, i).Value
                        Next
                        If A > B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                            B = B + Me.TablaIP.Item(ImpIP.Index, 0).Value
                        ElseIf A < B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            A = A + Me.TablaAF.Item(ImpAF.Index, 0).Value
                        Else
                            If A = 0 And B = 0 Then
                                Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                                Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            End If
                        End If


                        Importe = A - B
                        If Importe < 0 Then
                            Me.TablaGN.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                            Me.TablaOP.Item(1, 0).Value = 0
                        ElseIf Importe > 0 Then
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                        Else
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = 0
                        End If
                        'Calcula_Saldo_Favor(5, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))
                        Importe = 0
                        For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1
                            Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(5, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Importe += Me.TablaAF1.Item(1, i).Value
                        Next
                    Case 5
                        For i As Integer = 0 To Me.TablaJunio.Rows.Count - 1
                            A = A + Me.TablaJunio.Item(Car.Index, i).Value
                            B = B + Me.TablaJunio.Item(Abon.Index, i).Value
                        Next
                        If A > B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                            B = B + Me.TablaIP.Item(ImpIP.Index, 0).Value
                        ElseIf A < B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            A = A + Me.TablaAF.Item(ImpAF.Index, 0).Value
                        Else
                            If A = 0 And B = 0 Then
                                Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                                Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            End If
                        End If


                        Importe = A - B
                        If Importe < 0 Then
                            Me.TablaGN.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                            Me.TablaOP.Item(1, 0).Value = 0
                        ElseIf Importe > 0 Then
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                        Else
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = 0
                        End If
                        'Calcula_Saldo_Favor(6, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))
                        Importe = 0
                        For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1
                            Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(6, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Importe += Me.TablaAF1.Item(1, i).Value
                        Next
                    Case 6
                        For i As Integer = 0 To Me.TablaJulio.Rows.Count - 1
                            A = A + Me.TablaJulio.Item(Car.Index, i).Value
                            B = B + Me.TablaJulio.Item(Abon.Index, i).Value
                        Next
                        If A > B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                            B = B + Me.TablaIP.Item(ImpIP.Index, 0).Value
                        ElseIf A < B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            A = A + Me.TablaAF.Item(ImpAF.Index, 0).Value
                        Else
                            If A = 0 And B = 0 Then
                                Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                                Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            End If
                        End If


                        Importe = A - B
                        If Importe < 0 Then
                            Me.TablaGN.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                            Me.TablaOP.Item(1, 0).Value = 0
                        ElseIf Importe > 0 Then
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                        Else
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = 0
                        End If
                        'Calcula_Saldo_Favor(7, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))
                        Importe = 0
                        For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1
                            Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(7, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Importe += Me.TablaAF1.Item(1, i).Value
                        Next
                    Case 7
                        For i As Integer = 0 To Me.TablaAgosto.Rows.Count - 1
                            A = A + Me.TablaAgosto.Item(Car.Index, i).Value
                            B = B + Me.TablaAgosto.Item(Abon.Index, i).Value
                        Next
                        If A > B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                            B = B + Me.TablaIP.Item(ImpIP.Index, 0).Value
                        ElseIf A < B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            A = A + Me.TablaAF.Item(ImpAF.Index, 0).Value
                        Else
                            If A = 0 And B = 0 Then
                                Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                                Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            End If
                        End If


                        Importe = A - B
                        If Importe < 0 Then
                            Me.TablaGN.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                            Me.TablaOP.Item(1, 0).Value = 0
                        ElseIf Importe > 0 Then
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                        Else
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = 0

                        End If
                        'Calcula_Saldo_Favor(8, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))
                        Importe = 0
                        For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1
                            Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(8, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Importe += Me.TablaAF1.Item(1, i).Value
                        Next
                    Case 8
                        For i As Integer = 0 To Me.TablaSeptiembre.Rows.Count - 1
                            A = A + Me.TablaSeptiembre.Item(Car.Index, i).Value
                            B = B + Me.TablaSeptiembre.Item(Abon.Index, i).Value
                        Next
                        If A > B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                            B = B + Me.TablaIP.Item(ImpIP.Index, 0).Value
                        ElseIf A < B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            A = A + Me.TablaAF.Item(ImpAF.Index, 0).Value
                        Else
                            If A = 0 And B = 0 Then
                                Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                                Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            End If
                        End If


                        Importe = A - B
                        If Importe < 0 Then
                            Me.TablaGN.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                            Me.TablaOP.Item(1, 0).Value = 0
                        ElseIf Importe > 0 Then
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                        Else
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = 0
                        End If
                        'Calcula_Saldo_Favor(9, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))
                        Importe = 0
                        For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1
                            Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(9, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Importe += Me.TablaAF1.Item(1, i).Value
                        Next
                    Case 9
                        For i As Integer = 0 To Me.TablaOctubre.Rows.Count - 1
                            A = A + Me.TablaOctubre.Item(Car.Index, i).Value
                            B = B + Me.TablaOctubre.Item(Abon.Index, i).Value
                        Next
                        If A > B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                            B = B + Me.TablaIP.Item(ImpIP.Index, 0).Value
                        ElseIf A < B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            A = A + Me.TablaAF.Item(ImpAF.Index, 0).Value
                        Else
                            If A = 0 And B = 0 Then
                                Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                                Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            End If
                        End If


                        Importe = A - B
                        If Importe < 0 Then
                            Me.TablaGN.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                            Me.TablaOP.Item(1, 0).Value = 0
                        ElseIf Importe > 0 Then
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                        Else
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = 0
                        End If
                        'Calcula_Saldo_Favor(10, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))
                        Importe = 0
                        For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1
                            Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(10, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Importe += Me.TablaAF1.Item(1, i).Value
                        Next
                    Case 10
                        For i As Integer = 0 To Me.TablaNoviembre.Rows.Count - 1
                            A = A + Me.TablaNoviembre.Item(Car.Index, i).Value
                            B = B + Me.TablaNoviembre.Item(Abon.Index, i).Value
                        Next
                        If A > B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                            B = B + Me.TablaIP.Item(ImpIP.Index, 0).Value
                        ElseIf A < B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            A = A + Me.TablaAF.Item(ImpAF.Index, 0).Value
                        Else
                            If A = 0 And B = 0 Then
                                Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                                Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            End If
                        End If


                        Importe = A - B
                        If Importe < 0 Then
                            Me.TablaGN.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                            Me.TablaOP.Item(1, 0).Value = 0
                        ElseIf Importe > 0 Then
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                        Else
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = 0
                        End If
                        'Calcula_Saldo_Favor(11, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))
                        Importe = 0
                        For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1
                            Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(11, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Importe += Me.TablaAF1.Item(1, i).Value
                        Next
                    Case 11
                        For i As Integer = 0 To Me.TablaDiciembre.Rows.Count - 1
                            A = A + Me.TablaDiciembre.Item(Car.Index, i).Value
                            B = B + Me.TablaDiciembre.Item(Abon.Index, i).Value
                        Next
                        If A > B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                            B = B + Me.TablaIP.Item(ImpIP.Index, 0).Value
                        ElseIf A < B Then
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                            A = A + Me.TablaAF.Item(ImpAF.Index, 0).Value
                        Else
                            If A = 0 And B = 0 Then
                                Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                                Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            End If
                        End If


                        Importe = A - B
                        If Importe < 0 Then
                            Me.TablaGN.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                            Me.TablaOP.Item(1, 0).Value = 0
                        ElseIf Importe > 0 Then
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                        Else
                            Me.TablaGN.Item(1, 0).Value = 0
                            Me.TablaOP.Item(1, 0).Value = 0
                        End If
                        'Calcula_Saldo_Favor(12, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))
                        Importe = 0
                        For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1
                            Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(12, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Importe += Me.TablaAF1.Item(1, i).Value
                        Next
                End Select
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub TablaCancelaciones_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TablaCancelaciones.SelectedIndexChanged
        Try
            Me.TablaAF1.RowCount = Ct.Count
            For i As Integer = 0 To Ct.Count - 1 'Se recuerdan las  cuentas a favor años anteriores
                Me.TablaAF1.Item(0, i).Value = Me.CtaAf1.Items(Obtener_Index(Ct(i).Cta, Me.CtaAf1))
            Next

            Me.TablaIP.RowCount = CtIp.Count
            For i As Integer = 0 To CtIp.Count - 1 'Se recuerdan las  cuentas a favor años anteriores
                Me.TablaIP.Item(0, i).Value = Me.CtaIP.Items(Obtener_Index(CtIp(i).Cta, Me.CtaIP))
            Next
            Me.TablaAF.RowCount = CtAf.Count
            For i As Integer = 0 To CtAf.Count - 1 'Se recuerdan las  cuentas a favor años anteriores
                Me.TablaAF.Item(0, i).Value = Me.CtaAf.Items(Obtener_Index(CtAf(i).Cta, Me.CtaAf))
            Next
            If Me.TablaAF.Item(CtaAf.Index, 0).Value IsNot Nothing And Me.TablaIP.Item(CtaIP.Index, 0).Value IsNot Nothing Then
                Dim Importe As Decimal = 0
                Dim Diferencia As Decimal = 0
                Dim Separador() As String

                Dim Valor As Decimal = 0
                Dim A, B As Decimal
                Select Case Me.TablaCancelaciones.SelectedIndex
                    Case 0
                        For i As Integer = 0 To Me.TablaEnero.Rows.Count - 1
                            A = A + Me.TablaEnero.Item(Car.Index, i).Value
                            B = B + Me.TablaEnero.Item(Abon.Index, i).Value
                        Next
                        If Me.TablaAF1.Rows.Count > 0 Then
                            For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1 'Calcula es saldo actual de las cuentas a favor años anteriores
                                Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(1, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Next
                        End If
                        Calcula_Saldo_Favor(13, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))

                    Case 1
                        For i As Integer = 0 To Me.TablaFebrero.Rows.Count - 1
                            A = A + Me.TablaFebrero.Item(Car.Index, i).Value
                            B = B + Me.TablaFebrero.Item(Abon.Index, i).Value
                        Next
                        If Me.TablaAF1.Rows.Count > 0 Then
                            For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1 'Calcula es saldo actual de las cuentas a favor años anteriores
                                Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(2, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Next
                        End If
                        Calcula_Saldo_Favor(1, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))

                    Case 2
                        For i As Integer = 0 To Me.TablaMarzo.Rows.Count - 1 ' Calcula si es a favor o por pagar
                            A = A + Me.TablaMarzo.Item(Car.Index, i).Value
                            B = B + Me.TablaMarzo.Item(Abon.Index, i).Value
                        Next
                        If Me.TablaAF1.Rows.Count > 0 Then
                            For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1 'Calcula es saldo actual de las cuentas a favor años anteriores
                                Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(3, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Next
                        End If
                        Calcula_Saldo_Favor(3, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))

                    Case 3
                        For i As Integer = 0 To Me.TablaAbril.Rows.Count - 1 ' Calcula si es a favor o por pagar
                            A = A + Me.TablaAbril.Item(Car.Index, i).Value
                            B = B + Me.TablaAbril.Item(Abon.Index, i).Value
                        Next
                        If Me.TablaAF1.Rows.Count > 0 Then
                            For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1 'Calcula es saldo actual de las cuentas a favor años anteriores
                                Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(4, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Next
                        End If
                        Calcula_Saldo_Favor(4, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))

                    Case 4
                        For i As Integer = 0 To Me.TablaMayo.Rows.Count - 1 ' Calcula si es a favor o por pagar
                            A = A + Me.TablaMayo.Item(Car.Index, i).Value
                            B = B + Me.TablaMayo.Item(Abon.Index, i).Value
                        Next
                        If Me.TablaAF1.Rows.Count > 0 Then
                            For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1 'Calcula es saldo actual de las cuentas a favor años anteriores
                                Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(5, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Next
                        End If
                        Calcula_Saldo_Favor(5, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))

                    Case 5
                        For i As Integer = 0 To Me.TablaJunio.Rows.Count - 1 ' Calcula si es a favor o por pagar
                            A = A + Me.TablaJunio.Item(Car.Index, i).Value
                            B = B + Me.TablaJunio.Item(Abon.Index, i).Value
                        Next
                        If Me.TablaAF1.Rows.Count > 0 Then
                            For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1 'Calcula es saldo actual de las cuentas a favor años anteriores
                                Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(6, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Next
                        End If
                        Calcula_Saldo_Favor(6, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))

                    Case 6
                        For i As Integer = 0 To Me.TablaJulio.Rows.Count - 1 ' Calcula si es a favor o por pagar
                            A = A + Me.TablaJulio.Item(Car.Index, i).Value
                            B = B + Me.TablaJulio.Item(Abon.Index, i).Value
                        Next
                        If Me.TablaAF1.Rows.Count > 0 Then
                            For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1 'Calcula es saldo actual de las cuentas a favor años anteriores
                                Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(7, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Next
                        End If
                        Calcula_Saldo_Favor(7, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))

                    Case 7
                        For i As Integer = 0 To Me.TablaAgosto.Rows.Count - 1 ' Calcula si es a favor o por pagar
                            A = A + Me.TablaAgosto.Item(Car.Index, i).Value
                            B = B + Me.TablaAgosto.Item(Abon.Index, i).Value
                        Next
                        If Me.TablaAF1.Rows.Count > 0 Then
                            For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1 'Calcula es saldo actual de las cuentas a favor años anteriores
                                Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(8, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Next
                        End If
                        Calcula_Saldo_Favor(8, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))

                    Case 8
                        For i As Integer = 0 To Me.TablaSeptiembre.Rows.Count - 1 ' Calcula si es a favor o por pagar
                            A = A + Me.TablaSeptiembre.Item(Car.Index, i).Value
                            B = B + Me.TablaSeptiembre.Item(Abon.Index, i).Value
                        Next
                        If Me.TablaAF1.Rows.Count > 0 Then
                            For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1 'Calcula es saldo actual de las cuentas a favor años anteriores
                                Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(9, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Next
                        End If
                        Calcula_Saldo_Favor(9, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))

                    Case 9
                        For i As Integer = 0 To Me.TablaOctubre.Rows.Count - 1 ' Calcula si es a favor o por pagar
                            A = A + Me.TablaOctubre.Item(Car.Index, i).Value
                            B = B + Me.TablaOctubre.Item(Abon.Index, i).Value
                        Next
                        If Me.TablaAF1.Rows.Count > 0 Then
                            For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1 'Calcula es saldo actual de las cuentas a favor años anteriores
                                Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(10, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Next
                        End If
                        Calcula_Saldo_Favor(10, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))

                    Case 10
                        For i As Integer = 0 To Me.TablaNoviembre.Rows.Count - 1 ' Calcula si es a favor o por pagar
                            A = A + Me.TablaNoviembre.Item(Car.Index, i).Value
                            B = B + Me.TablaNoviembre.Item(Abon.Index, i).Value
                        Next
                        If Me.TablaAF1.Rows.Count > 0 Then
                            For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1 'Calcula es saldo actual de las cuentas a favor años anteriores
                                Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(11, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Next
                        End If
                        Calcula_Saldo_Favor(11, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))

                    Case 11
                        For i As Integer = 0 To Me.TablaDiciembre.Rows.Count - 1 ' Calcula si es a favor o por pagar
                            A = A + Me.TablaDiciembre.Item(Car.Index, i).Value
                            B = B + Me.TablaDiciembre.Item(Abon.Index, i).Value
                        Next
                        If Me.TablaAF1.Rows.Count > 0 Then
                            For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1 'Calcula es saldo actual de las cuentas a favor años anteriores
                                Me.TablaAF1.Item(1, i).Value = Calcula_Saldo_FavorAnteriores(12, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF1.Item(0, i).Value.ToString.Substring(0, 19).Replace("-", ""))
                            Next
                        End If
                        Calcula_Saldo_Favor(12, Me.lstCliente.SelectItem, Me.comboAño.Text.Trim(), Me.TablaAF.Item(0, 0).Value.ToString.Substring(0, 19).Replace("-", ""))

                End Select
                Me.TablaIP.Item(ImpIP.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                If Me.TablaAF1.Rows.Count > 0 Then
                    Diferencia = Me.TablaIP.Item(ImpIP.Index, 0).Value
                    Me.TablaAF.RowCount = 1
                    Me.TablaIP.RowCount = 1
                    If A > B Then ' si cargos (A) son mayores a abonos (B)
                        For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1
                            If Me.TablaIP.Item(ImpIP.Index, 0).Value < Me.TablaAF1.Item(1, 0).Value And Diferencia = 0 Then
                                Me.TablaAF1.Rows.Clear()
                                Me.TablaAF1.RowCount = 1
                                Me.TablaAF1.Item(0, 0).Value = Me.CtaAf1.Items(Obtener_Index(Ct(0).Cta, Me.CtaAf1))
                                Me.TablaAF1.Item(1, 0).Value = Me.TablaIP.Item(ImpIP.Index, 0).Value
                                Diferencia = 0
                                Me.TablaIP.Rows.Clear()
                                B += Me.TablaAF1.Item(1, 0).Value
                                Exit For
                            ElseIf Diferencia > Me.TablaAF1.Item(1, i).Value Then
                                Diferencia -= Me.TablaAF1.Item(1, i).Value
                                B += Me.TablaAF1.Item(1, i).Value
                            Else
                                Me.TablaAF1.RowCount = i + 1
                                Me.TablaAF1.Item(1, i).Value = Diferencia
                                Diferencia -= Diferencia
                                Me.TablaIP.Rows.Clear()
                                B += Me.TablaAF1.Item(1, i).Value
                                Exit For
                            End If
                        Next
                        If Diferencia = 0 Then
                            Me.TablaAF.Rows.Clear()
                        Else
                            Diferencia -= Me.TablaAF.Item(ImpAF.Index, 0).Value
                            B += Me.TablaAF.Item(ImpAF.Index, 0).Value
                            If Diferencia > 0 Then
                                Me.TablaIP.Item(ImpIP.Index, 0).Value = Diferencia
                                B += Me.TablaIP.Item(ImpIP.Index, 0).Value
                            Else
                                B += Me.TablaAF.Item(0, 0).Value
                                Me.TablaIP.Item(ImpIP.Index, 0).Value = Diferencia
                                B += Me.TablaIP.Item(ImpIP.Index, 0).Value
                            End If

                        End If
                    ElseIf A < B Then

                        Me.TablaAF.Item(ImpAF.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                        A = A + Me.TablaAF.Item(ImpAF.Index, 0).Value
                        Try
                            Me.TablaAF1.Rows.Clear()
                            Me.TablaIP.Rows.Clear()
                        Catch ex As Exception

                        End Try
                    Else
                        If A = 0 And B = 0 Then
                            Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                            Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                            Me.TablaIP.Rows.Clear()
                            Me.TablaAF.Rows.Clear()
                            Me.TablaAF1.Rows.Clear()
                        ElseIf A = B Then
                            Me.TablaIP.Rows.Clear()
                            Me.TablaAF.Rows.Clear()
                            Me.TablaAF1.Rows.Clear()
                        End If
                    End If
                Else
                    If A > B Then
                        Me.TablaIP.Item(ImpIP.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                        Me.TablaAF.Item(ImpAF.Index, 0).Value = 0
                        B = B + Me.TablaIP.Item(ImpIP.Index, 0).Value
                    ElseIf A < B Then
                        Me.TablaIP.Item(ImpIP.Index, 0).Value = 0
                        Me.TablaAF.Item(ImpAF.Index, 0).Value = Math.Round(IIf(A - B > 0, A - B, (A - B) * -1))
                        A = A + Me.TablaAF.Item(ImpAF.Index, 0).Value
                    Else
                        If A = 0 And B = 0 Then
                            Me.TablaIP.Rows.Clear()
                            Me.TablaAF.Rows.Clear()
                            Me.TablaAF1.Rows.Clear()
                        End If
                    End If
                End If

                Importe = A - B
                If Importe < 0 Then
                    Me.TablaGN.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                    Me.TablaOP.Item(1, 0).Value = 0
                ElseIf Importe > 0 Then
                    Me.TablaGN.Item(1, 0).Value = 0
                    Me.TablaOP.Item(1, 0).Value = IIf(A - B > 0, A - B, (A - B) * -1)
                Else
                    Me.TablaGN.Item(1, 0).Value = 0
                    Me.TablaOP.Item(1, 0).Value = 0
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub


    Private Sub Calcula_Saldo_Favor(ByVal Mes As Integer, ByVal Cliente As String, ByVal Anio As Integer, ByVal Cta As String)
        Dim Importe As Decimal = 0

        Dim Sql As String = ""
        Sql = "Select CASE WHEN    sum(Detalle_Polizas.Cargo - Detalle_Polizas.Abono) IS NULL OR sum(Detalle_Polizas.Cargo - Detalle_Polizas.Abono) < 0  THEN 0 WHEN sum(Detalle_Polizas.Cargo - Detalle_Polizas.Abono) >0 THEN sum(Detalle_Polizas.Cargo - Detalle_Polizas.Abono) END  As Saldo FROM Polizas "
        Sql &= " INNER Join Detalle_Polizas ON Detalle_Polizas.ID_poliza = Polizas.ID_poliza "
        Sql &= " WHERE Id_Empresa =" & Cliente & " And Polizas.ID_anio = " & Anio & " And Polizas.ID_mes < " & Mes & " "
        Sql &= " And Polizas.Concepto = 'Cancelacion de IVA Trasladado VS Acreditable' AND Detalle_Polizas.Cuenta = " & Cta & ""
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If Convert.ToDecimal(IIf(IsDBNull(ds.Tables(0).Rows(0)(0)), 0, ds.Tables(0).Rows(0)(0))) > 0 Then
                Me.TablaAF.Item(1, 0).Value = ds.Tables(0).Rows(0)(0)
            Else
                Me.TablaAF.Item(1, 0).Value = 0
            End If
        End If
    End Sub
    Private Function Calcula_Saldo_FavorAnteriores(ByVal Mes As Integer, ByVal Cliente As String, ByVal Anio As Integer, ByVal Cta As String)
        Dim Importe As Decimal = 0
        Try
            If TablaIP.Rows.Count = 1 Then
                Me.TablaIP.Rows.RemoveAt(1)
            ElseIf TablaIP.Rows.Count = 2 Then
                Me.TablaIP.Rows.RemoveAt(2)
                Me.TablaIP.Rows.RemoveAt(1)
            End If
        Catch ex As Exception

        End Try


        Dim Sql As String = ""

        Sql = "Select CASE WHEN    sum(Detalle_Polizas.Cargo - Detalle_Polizas.Abono) IS NULL OR sum(Detalle_Polizas.Cargo - Detalle_Polizas.Abono) < 0  THEN 0 WHEN sum(Detalle_Polizas.Cargo - Detalle_Polizas.Abono) >0 THEN sum(Detalle_Polizas.Cargo - Detalle_Polizas.Abono) END  As Saldo FROM Polizas "
        Sql &= " INNER Join Detalle_Polizas ON Detalle_Polizas.ID_poliza = Polizas.ID_poliza "
        Sql &= " WHERE Id_Empresa =" & Cliente & " AND (( Polizas.ID_anio = " & Anio - 1 & ")  OR (Polizas.ID_anio  = " & Anio & " And Polizas.ID_mes < " & Mes & "  ))  "
        Sql &= " And Polizas.Concepto in ('Cancelacion de IVA Trasladado VS Acreditable' ,'Poliza Cierre') AND Detalle_Polizas.Cuenta = " & Cta & ""
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If Convert.ToDecimal(IIf(IsDBNull(ds.Tables(0).Rows(0)(0)), 0, ds.Tables(0).Rows(0)(0))) > 0 Then
                Importe = ds.Tables(0).Rows(0)(0)
            Else
                Importe = 0
            End If
        End If
        Return Importe
    End Function
    Private Sub InsertarCtasFavor()
        Dim Saldo As Decimal = 0
        Dim Total As Decimal = 0
        For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1
            Saldo += Me.TablaAF1.Item(1, i).Value
        Next
        Try
            If Me.TablaIP.Item(1, 0).Value > 0 And Saldo > 0 Then
                Total = Me.TablaIP.Item(1, 0).Value
                If Saldo > Me.TablaIP.Item(1, 0).Value Then
                    For i As Integer = 0 To Me.TablaAF1.Rows.Count - 1
                        If Me.TablaAF1.Item(1, 0).Value > 0 Then
                            If Total > Me.TablaAF1.Item(1, 0).Value Then
                                Me.TablaIP.Rows.Add(Me.TablaAF1.Item(0, i).Value, Me.TablaAF1.Item(1, i).Value)
                                Total -= Me.TablaAF1.Item(1, i).Value
                            ElseIf Total < Me.TablaAF1.Item(1, 0).Value Then
                                Me.TablaIP.Rows.Add(Me.TablaAF1.Item(0, i).Value, Total)
                                Total -= Total
                            End If
                        End If
                    Next
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TablaAF1_KeyDown(sender As Object, e As KeyEventArgs) Handles TablaAF1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.TablaAF1.Rows.Add()
        End If
    End Sub

    Private Sub TablaAF1_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles TablaAF1.DataError

    End Sub
End Class
