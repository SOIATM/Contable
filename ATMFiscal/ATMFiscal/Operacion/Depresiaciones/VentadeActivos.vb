Imports Telerik.WinControls
Public Class VentadeActivos
    Dim Negrita_verde As New DataGridViewCellStyle
    Dim Negrita_morado As New DataGridViewCellStyle
    Dim anio = Str(DateTime.Now.Year)
    Dim m = Now.Date.Month.ToString
    Dim tasa As Decimal = 0.16
    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub VentadeActivos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Eventos.DiseñoTabla(Me.TablaResumen)
        Eventos.DiseñoTabla(Me.TablaVentasActivos)
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

    End Sub

    Private Sub CmdListar_Click(sender As Object, e As EventArgs) Handles CmdListar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.lstCliente.SelectText <> "" Then
            If Trim(Me.LstAnio.Text) <> "" Then
                If Me.TablaVentasActivos.Rows.Count > 0 Then
                    Me.TablaVentasActivos.Rows.Clear()
                    Crear_Filas(Trim(Me.LstAnio.Text), Me.lstCliente.SelectItem)
                Else
                    Crear_Filas(Trim(Me.LstAnio.Text), Me.lstCliente.SelectItem)
                End If

            Else
                RadMessageBox.Show("Debes seleccionar un Año para consultar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            End If
        Else
            RadMessageBox.Show("Debes seleccionar una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
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
        Return Datos
    End Function
    Private Sub Crear_Filas(ByVal Anio As Integer, ByVal Cliente As Integer)
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Negrita_verde.Font = New Font(Me.TablaVentasActivos.Font, FontStyle.Bold)
        Negrita_verde.BackColor = Color.LawnGreen
        Negrita_morado.Font = New Font(Me.TablaVentasActivos.Font, FontStyle.Bold)
        Negrita_morado.BackColor = Color.Khaki
        Dim Tpos As New List(Of Tipos)
        Dim Tipos_S As New List(Of Tipo_Suma)
        Dim Titulo As DataGridViewRow
        Dim sql As String = "  SELECT Id_Venta	, Cuenta,	Folio,	Cta_Depreciacion,	Resultado_Dep_1, Resultado_Dep_2, 	Resultado_Dep_3,
		        Resultado_Dep_4, Referencia, Cliente,   Num_Factura,Fecha_Factura, ImporteG, ImporteE,Iva,TotalF,	MOI,	Fecha_Adquisicion,
		       	Dep_Acumulada,	Valor_en_libros, Utilidad_Contable,	Perdida_Contable, MOIPD, 	Ultimo_MesPM,	Factor_UMPM,	Fecha_Adq_Fiscal,	INPC_FechaAdq,	Factor_Actualizacion,	Costo_Fiscal,	Precio_VentaF,	Utilidad_Fiscal,	Perdida_Fiscal, Tipo,	Anio,	Id_Empresa,	Sumas FROM dbo.VentaActivos
	            WHERE  Id_Empresa = " & Cliente & " and Anio  =" & Anio & "  ORDER BY Id_Venta  "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim Cuentas As DataSet = Eventos.Obtener_DS("  SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias   From Catalogo_de_Cuentas Where  Id_Empresa = " & Me.lstCliente.SelectItem & " AND Nivel1 IN (" & Ctas(Me.lstCliente.SelectItem) & " ) order by Alias ")
            If Cuentas.Tables(0).Rows.Count > 0 Then
                If Me.cta.Items.Count = 0 Then
                    For i As Integer = 0 To Cuentas.Tables(0).Rows.Count - 1
                        Me.cta.Items.Add(Trim(Cuentas.Tables(0).Rows(i)("Alias")))
                    Next
                End If
            End If

            Me.TablaVentasActivos.RowCount = ds.Tables(0).Rows.Count
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.TablaVentasActivos.RowCount - 1
            Me.Cursor = Cursors.AppStarting
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim Fila As DataGridViewRow = Me.TablaVentasActivos.Rows(i)

                Try
                    If ds.Tables(0).Rows(i)("Cuenta") <> "" Then
                        Fila.Cells(cta.Index).Value = Me.cta.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("Cuenta")), Me.cta))
                    End If
                Catch ex As Exception

                End Try
                Me.TablaVentasActivos.Item(CtaDepActivo.Index, i).Value = ds.Tables(0).Rows(i)("Cta_Depreciacion")


                Me.TablaVentasActivos.Item(ResulDep1.Index, i).Value = ds.Tables(0).Rows(i)("Resultado_Dep_1")
                Me.TablaVentasActivos.Item(ResulDep2.Index, i).Value = ds.Tables(0).Rows(i)("Resultado_Dep_2")
                Me.TablaVentasActivos.Item(ResulDep3.Index, i).Value = ds.Tables(0).Rows(i)("Resultado_Dep_3")
                Me.TablaVentasActivos.Item(ResulDep4.Index, i).Value = ds.Tables(0).Rows(i)("Resultado_Dep_4")
                Me.TablaVentasActivos.Item(Ref.Index, i).Value = ds.Tables(0).Rows(i)("Referencia")
                Me.TablaVentasActivos.Item(Client.Index, i).Value = ds.Tables(0).Rows(i)("Cliente")

                Me.TablaVentasActivos.Item(NumFactura.Index, i).Value = ds.Tables(0).Rows(i)("Num_Factura")
                Dim fecha As Object = ds.Tables(0).Rows(i)("Fecha_Adquisicion").ToString
                If fecha = "" Then
                    Me.TablaVentasActivos.Item(FechAdqF.Index, i).Value = Nothing
                Else
                    Me.TablaVentasActivos.Item(FechAdqF.Index, i).Value = fecha.ToString.Substring(0, 10)
                End If

                fecha = ds.Tables(0).Rows(i)("Fecha_Factura").ToString
                If fecha = "" Then
                    Me.TablaVentasActivos.Item(FechaFactura.Index, i).Value = Nothing
                Else
                    Me.TablaVentasActivos.Item(FechaFactura.Index, i).Value = fecha.ToString.Substring(0, 10)
                End If

                Me.TablaVentasActivos.Item(ImpGF.Index, i).Value = ds.Tables(0).Rows(i)("ImporteG")
                Me.TablaVentasActivos.Item(ImpExF.Index, i).Value = ds.Tables(0).Rows(i)("ImporteE")
                Me.TablaVentasActivos.Item(IvaF.Index, i).Value = ds.Tables(0).Rows(i)("IVA")
                Me.TablaVentasActivos.Item(TotalF.Index, i).Value = ds.Tables(0).Rows(i)("TotalF")
                Me.TablaVentasActivos.Item(MOIF.Index, i).Value = ds.Tables(0).Rows(i)("MOI")

                Me.TablaVentasActivos.Item(DepAcuF.Index, i).Value = ds.Tables(0).Rows(i)("Dep_Acumulada_Actual")
                Me.TablaVentasActivos.Item(ValorlibrosF.Index, i).Value = ds.Tables(0).Rows(i)("Valor_en_libros")
                Me.TablaVentasActivos.Item(UtilidadContable.Index, i).Value = ds.Tables(0).Rows(i)("Utilidad_Contable")
                Me.TablaVentasActivos.Item(PerdidaContable.Index, i).Value = ds.Tables(0).Rows(i)("Perdida_Contable")

                Me.TablaVentasActivos.Item(MoiPD.Index, i).Value = ds.Tables(0).Rows(i)("MoiPD")
                Me.TablaVentasActivos.Item(UltMes.Index, i).Value = ds.Tables(0).Rows(i)("Ultimo_MesPM")
                Me.TablaVentasActivos.Item(FactorUltm.Index, i).Value = ds.Tables(0).Rows(i)("Factor_UMPM")
                Me.TablaVentasActivos.Item(FechaAdq.Index, i).Value = ds.Tables(0).Rows(i)("Fecha_Adq_Fiscal")
                Me.TablaVentasActivos.Item(InpcFecha.Index, i).Value = ds.Tables(0).Rows(i)("INPC_FechaAdq")
                Me.TablaVentasActivos.Item(FactorActual.Index, i).Value = ds.Tables(0).Rows(i)("Factor_Actualizacion")
                Me.TablaVentasActivos.Item(CostoFinal.Index, i).Value = ds.Tables(0).Rows(i)("Costo_Fiscal")
                Me.TablaVentasActivos.Item(PrecioVentaF.Index, i).Value = ds.Tables(0).Rows(i)("Precio_VentaF")
                Me.TablaVentasActivos.Item(UtilidadF.Index, i).Value = ds.Tables(0).Rows(i)("Utilidad_Fiscal")
                Me.TablaVentasActivos.Item(PerdidaF.Index, i).Value = ds.Tables(0).Rows(i)("Perdida_Fiscal")

                Me.TablaVentasActivos.Item(Tipo.Index, i).Value = ds.Tables(0).Rows(i)("Tipo")
                Me.TablaVentasActivos.Item(Sumas.Index, i).Value = ds.Tables(0).Rows(i)("Sumas")

                If (Me.TablaVentasActivos.Item(Ref.Index, i).Value <> Nothing Or Me.TablaVentasActivos.Item(Ref.Index, i).Value.ToString <> "") And Me.TablaVentasActivos.Item(cta.Index, i).Value Is Nothing And Me.TablaVentasActivos.Item(Tipo.Index, i).Value < 40 Then
                    Titulo = Me.TablaVentasActivos.Rows(i)
                    Titulo.DefaultCellStyle = Negrita_verde
                ElseIf (Me.TablaVentasActivos.Item(Ref.Index, i).Value <> Nothing Or Me.TablaVentasActivos.Item(Ref.Index, i).Value.ToString <> "") And Me.TablaVentasActivos.Item(cta.Index, i).Value Is Nothing And Me.TablaVentasActivos.Item(Tipo.Index, i).Value >= 40 Then
                    Titulo = Me.TablaVentasActivos.Rows(i)
                    Titulo.DefaultCellStyle = Negrita_morado
                End If

                frm.Barra.Value = i
            Next
            frm.Close()
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow

        Else
            If RadMessageBox.Show("No existe plantilla de la Empresa " & Me.lstCliente.SelectText & " para el año " & Anio & " deseas crearla?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Calcular()
            Else
                Exit Sub
            End If
        End If


    End Sub
    Private Sub Calcular()
        Negrita_verde.Font = New Font(Me.TablaVentasActivos.Font, FontStyle.Bold)
        Negrita_verde.BackColor = Color.LawnGreen
        Negrita_morado.Font = New Font(Me.TablaVentasActivos.Font, FontStyle.Bold)
        Negrita_morado.BackColor = Color.Khaki
        Dim Tpos As New List(Of Tipos)
        Dim Tipos_S As New List(Of Tipo_Suma)
        Dim Titulo As DataGridViewRow
        Dim Posicion As Integer = 0
        ' Dim Cuentas As DataSet = Eventos.Obtener_DS("  SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias   From Catalogo_de_Cuentas Where Clasificacion ='AFI' AND Id_Empresa = " & Me.lstCliente.SelectItem & " AND (RFc <>'NULL' AND RFC <> '' AND RFC IS NOT NULL ) order by Alias ")
        Dim Cuentas As DataSet = Eventos.Obtener_DS("  SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias   From Catalogo_de_Cuentas Where  Id_Empresa = " & Me.lstCliente.SelectItem & " AND Nivel1 IN (" & Ctas(Me.lstCliente.SelectItem) & " ) order by Alias ")
        If Cuentas.Tables(0).Rows.Count > 0 Then
            If Me.cta.Items.Count = 0 Then
                For i As Integer = 0 To Cuentas.Tables(0).Rows.Count - 1
                    Me.cta.Items.Add(Trim(Cuentas.Tables(0).Rows(i)("Alias")))


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
                Me.TablaVentasActivos.RowCount = Me.TablaVentasActivos.RowCount + ds.Tables(0).Rows.Count

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Titulo = Me.TablaVentasActivos.Rows(Posicion)
                    Me.TablaVentasActivos.Item(Ref.Index, Posicion).Value = ds.Tables(0).Rows(i)("TI")
                    Me.TablaVentasActivos.Item(Tipo.Index, Posicion).Value = ds.Tables(0).Rows(i)("Id_Tipo")
                    Me.TablaVentasActivos.Item(Sumas.Index, Posicion).Value = T.Tipo
                    Titulo.DefaultCellStyle = Negrita_verde
                    Posicion += 1

                    Sql = "  SELECT Catalogo_de_Cuentas.Nivel1 + '-' + Catalogo_de_Cuentas.Nivel2 + '-' + Catalogo_de_Cuentas.Nivel3 + '-' + Catalogo_de_Cuentas.Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion AS Alias,
	                        Tipo_Activos.Tasa, Clave_Activos.Id_Clave,Clave_Activos.Descripcion,Tipo_Activos.Id_Tipo,Tipo_Activos.Descripcion AS Ti,Ctas_Activo_Cliente.Id_Res_dep1,Ctas_Activo_Cliente.Id_Res_dep2,Ctas_Activo_Cliente.Id_Res_dep3,Ctas_Activo_Cliente.Id_Res_dep4
	                        FROM     Ctas_Activo_Cliente 
	                        INNER JOIN Catalogo_de_Cuentas ON Ctas_Activo_Cliente.Id_cat_Cuentas = Catalogo_de_Cuentas.Id_cat_Cuentas 
	                        INNER JOIN Tipo_Activos ON Tipo_Activos.Id_Tipo = Ctas_Activo_Cliente.Id_Tipo 
	                        INNER JOIN Clave_Activos ON Tipo_Activos.Id_Clave = Clave_Activos.Id_Clave WHERE Ctas_Activo_Cliente.Id_Empresa =" & Me.lstCliente.SelectItem & " and  Tipo_Activos.Id_Tipo = " & ds.Tables(0).Rows(i)("Id_Tipo") & ""
                    Dim ds2 As DataSet = Eventos.Obtener_DS(Sql)
                    If ds2.Tables(0).Rows.Count > 0 Then
                        Me.TablaVentasActivos.RowCount = Me.TablaVentasActivos.RowCount + ds2.Tables(0).Rows.Count
                        For j As Integer = 0 To ds2.Tables(0).Rows.Count - 1

                            Dim Fila As DataGridViewRow = Me.TablaVentasActivos.Rows(Posicion)
                            Try
                                If ds2.Tables(0).Rows(j)("Alias") <> "" Then
                                    Fila.Cells(cta.Index).Value = Me.cta.Items(Obtener_Index(Trim(ds2.Tables(0).Rows(j)("Alias")), Me.cta))
                                    'Me.TablaVentasActivos.Item(Dep.Index, Posicion).Value = Dep_Anterior(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, Fila.Cells(cta.Index).Value.trim())
                                End If
                            Catch ex As Exception

                            End Try

                            Me.TablaVentasActivos.Item(Tipo.Index, Posicion).Value = ds2.Tables(0).Rows(j)("Id_Tipo")
                            Me.TablaVentasActivos.Item(Sumas.Index, Posicion).Value = ds2.Tables(0).Rows(j)("Id_Clave")
                            Me.TablaVentasActivos.Item(ResulDep1.Index, Posicion).Value = ds2.Tables(0).Rows(j)("Id_Res_dep1")
                            Me.TablaVentasActivos.Item(ResulDep2.Index, Posicion).Value = ds2.Tables(0).Rows(j)("Id_Res_dep2")
                            Me.TablaVentasActivos.Item(ResulDep3.Index, Posicion).Value = ds2.Tables(0).Rows(j)("Id_Res_dep3")
                            Me.TablaVentasActivos.Item(ResulDep4.Index, Posicion).Value = ds2.Tables(0).Rows(j)("Id_Res_dep4")
                            Posicion += 1
                        Next

                    End If

                Next

            End If
            Me.TablaVentasActivos.RowCount = Me.TablaVentasActivos.RowCount + 1

            ' Se insertan Los resumenes
            Titulo = Me.TablaVentasActivos.Rows(Posicion)
            Me.TablaVentasActivos.Item(Ref.Index, Posicion).Value = "Saldo"
            Me.TablaVentasActivos.Item(Tipo.Index, Posicion).Value = 40
            Me.TablaVentasActivos.Item(Sumas.Index, Posicion).Value = T.Tipo
            Titulo.DefaultCellStyle = Negrita_morado
            Posicion += 1



        Next

    End Sub
    Private Sub Guarda_Ctas()

    End Sub
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
    Private Sub TablaVentasActivos_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles TablaVentasActivos.CellDoubleClick
        If Me.TablaVentasActivos.CurrentCell.ColumnIndex = CtaDepActivo.Index Then
            If Me.TablaVentasActivos.Item(cta.Index, Me.TablaVentasActivos.CurrentRow.Index).Value <> Nothing Then
                Dim Cuentas As New List(Of Cuentas_MOI)
                Cuentas = Ctas_Dep(Me.lstCliente.SelectItem, Me.TablaVentasActivos.Item(Sumas.Index, Me.TablaVentasActivos.CurrentRow.Index).Value)
                If Cuentas.Count > 0 Then
                    Dim Combo As New DataGridViewComboBoxCell
                    For Each C In Cuentas
                        Combo.Items.Add(C.Cuenta)
                        Combo.FlatStyle = FlatStyle.Flat
                    Next
                    Me.TablaVentasActivos.Item(CtaDepActivo.Index, Me.TablaVentasActivos.CurrentRow.Index) = Combo.Clone
                End If
            End If
        End If
    End Sub
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

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        '  Me.TablaImportar.Rows.Insert(Me.TablaImportar.CurrentRow.Index + 1, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", Me.TablaImportar.Item(Ti.Index, Me.TablaImportar.CurrentRow.Index).Value.ToString)

    End Sub

    Private Sub CmdCalcular_Click(sender As Object, e As EventArgs) Handles CmdCalcular.Click
        'Crear Resumenes


        For i As Integer = 0 To Me.TablaVentasActivos.Rows.Count - 1
            Dim ee As New System.Windows.Forms.DataGridViewCellEventArgs(0, i)
            Me.TablaVentasActivos_CellEndEdit(sender, ee)
        Next

        CalculaResumen()

    End Sub
    Private Function CargarDatos(ByVal Campo As String, ByVal Cliente As Integer, ByVal Tipo As Integer, ByVal Cuenta As String)
        Dim Importe As Decimal = 0
        Dim sql As String = " SELECT sum(depreciaciones." & Campo & " ) AS Importe FROM Depreciaciones WHERE Id_Empresa = " & Cliente & " AND Tipo = " & Tipo & " AND Cuenta ='" & Cuenta & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Importe = ds.Tables(0).Rows(0)(0)

        End If

        Return Importe
    End Function
    Private Function Campo(ByVal Cliente As Integer, ByVal Tipo As Integer, ByVal Cuenta As String)
        Dim Fecha As String = ""
        Dim Sql As String = "SELECT DISTINCT convert(VARCHAR,Depreciaciones.Fecha_Adquisicion,103)AS Fecha  FROM Depreciaciones WHERE Id_Empresa = " & Cliente & " AND Tipo = " & Tipo & " AND Cuenta ='" & Cuenta & "' AND Fecha_Adquisicion IS NOT NULL   "
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Fecha = ds.Tables(0).Rows(0)(0)
        End If
        Return Fecha
    End Function
    Private Function DatosINPC(ByVal Cliente As Integer, ByVal tipo As Integer, ByVal Cuenta As String)
        Dim Fecha As String = ""
        Dim Sql As String = "SELECT DISTINCT  INPC_Mes  FROM Depreciaciones WHERE Id_Empresa = " & Cliente & " AND Tipo = " & tipo & " AND Cuenta ='" & Cuenta & "' AND Fecha_Adquisicion IS NOT NULL   "
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Fecha = ds.Tables(0).Rows(0)(0)
        End If
        Return Fecha
    End Function
    Private Sub TablaVentasActivos_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles TablaVentasActivos.CellEndEdit
        If Me.TablaVentasActivos.Item(cta.Index, Me.TablaVentasActivos.CurrentRow.Index).Value <> Nothing Then
            Me.TablaVentasActivos.Item(MOIF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value = CargarDatos("MOI", Me.lstCliente.SelectItem, Me.TablaVentasActivos.Item(Tipo.Index, Me.TablaVentasActivos.CurrentRow.Index).Value, Me.TablaVentasActivos.Item(cta.Index, Me.TablaVentasActivos.CurrentRow.Index).Value)
            Me.TablaVentasActivos.Item(DepAcuF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value = CargarDatos("Dep_Acumulada_Actual", Me.lstCliente.SelectItem, Me.TablaVentasActivos.Item(Tipo.Index, Me.TablaVentasActivos.CurrentRow.Index).Value, Me.TablaVentasActivos.Item(cta.Index, Me.TablaVentasActivos.CurrentRow.Index).Value)
            Me.TablaVentasActivos.Item(ValorlibrosF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value = Me.TablaVentasActivos.Item(MOIF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value - Me.TablaVentasActivos.Item(DepAcuF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value
            Me.TablaVentasActivos.Item(FechAdqF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value = Campo(Me.lstCliente.SelectItem, Me.TablaVentasActivos.Item(Tipo.Index, Me.TablaVentasActivos.CurrentRow.Index).Value, Me.TablaVentasActivos.Item(cta.Index, Me.TablaVentasActivos.CurrentRow.Index).Value)
            Me.TablaVentasActivos.Item(UltMes.Index, Me.TablaVentasActivos.CurrentRow.Index).Value = DatosINPC(Me.lstCliente.SelectItem, Me.TablaVentasActivos.Item(Tipo.Index, Me.TablaVentasActivos.CurrentRow.Index).Value, Me.TablaVentasActivos.Item(cta.Index, Me.TablaVentasActivos.CurrentRow.Index).Value)

            Me.TablaVentasActivos.Item(FechaAdq.Index, Me.TablaVentasActivos.CurrentRow.Index).Value = Me.TablaVentasActivos.Item(FechAdqF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value

            If Me.TablaVentasActivos.Item(IvaF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value = 0 Then
                If Me.TablaVentasActivos.Item(Folio.Index, Me.TablaVentasActivos.CurrentRow.Index).Value <> Nothing Then
                    BuscarFactura(Me.TablaVentasActivos.Item(Folio.Index, Me.TablaVentasActivos.CurrentRow.Index).Value.TRIM(), Me.TablaVentasActivos.CurrentRow.Index)
                End If
            End If
            Me.TablaVentasActivos.Item(IvaF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value = Convert.ToDecimal(Me.TablaVentasActivos.Item(ImpGF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value) * tasa
            Me.TablaVentasActivos.Item(PrecioVenta.Index, Me.TablaVentasActivos.CurrentRow.Index).Value = Convert.ToDecimal(Me.TablaVentasActivos.Item(ImpGF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value) + Convert.ToDecimal(Me.TablaVentasActivos.Item(ImpExF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value)
            Me.TablaVentasActivos.Item(UtilidadContable.Index, Me.TablaVentasActivos.CurrentRow.Index).Value = IIf(Me.TablaVentasActivos.Item(PrecioVenta.Index, Me.TablaVentasActivos.CurrentRow.Index).Value > Me.TablaVentasActivos.Item(ValorlibrosF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value, Me.TablaVentasActivos.Item(PrecioVenta.Index, Me.TablaVentasActivos.CurrentRow.Index).Value - Me.TablaVentasActivos.Item(ValorlibrosF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value, 0)
            Me.TablaVentasActivos.Item(PerdidaContable.Index, Me.TablaVentasActivos.CurrentRow.Index).Value = IIf(Me.TablaVentasActivos.Item(ValorlibrosF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value > Me.TablaVentasActivos.Item(PrecioVenta.Index, Me.TablaVentasActivos.CurrentRow.Index).Value, Me.TablaVentasActivos.Item(ValorlibrosF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value - Me.TablaVentasActivos.Item(PrecioVenta.Index, Me.TablaVentasActivos.CurrentRow.Index).Value, 0)
            Me.TablaVentasActivos.Item(MoiPD.Index, Me.TablaVentasActivos.CurrentRow.Index).Value = Me.TablaVentasActivos.Item(ValorlibrosF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value
            Dim fecha As String = ""
            If Me.TablaVentasActivos.Item(UltMes.Index, Me.TablaVentasActivos.CurrentRow.Index).Value <> Nothing Then
                fecha = Me.TablaVentasActivos.Item(UltMes.Index, Me.TablaVentasActivos.CurrentRow.Index).Value
                'Eventos.ObtenerValorDB("INPC", "Importe", " datepart(month,fecha)=" & IIf(Len(MF.ToString) = 1, "0" & MF, MF) & " AND datepart(year,fecha)=" & Me.LstAnio.Text.Trim() & "", True)
                Me.TablaVentasActivos.Item(FactorUltm.Index, Me.TablaVentasActivos.CurrentRow.Index).Value = Eventos.ObtenerValorDB("INPC", "Importe", " datepart(month,fecha)=" & fecha.Substring(3, 2) & " AND datepart(year,fecha)=" & Me.LstAnio.Text.Trim() & "", True)
            End If
            If Me.TablaVentasActivos.Item(FechaAdq.Index, Me.TablaVentasActivos.CurrentRow.Index).Value <> Nothing Then
                fecha = Me.TablaVentasActivos.Item(FechaAdq.Index, Me.TablaVentasActivos.CurrentRow.Index).Value
                'Eventos.ObtenerValorDB("INPC", "Importe", " datepart(month,fecha)=" & IIf(Len(MF.ToString) = 1, "0" & MF, MF) & " AND datepart(year,fecha)=" & Me.LstAnio.Text.Trim() & "", True)
                Me.TablaVentasActivos.Item(InpcFecha.Index, Me.TablaVentasActivos.CurrentRow.Index).Value = Eventos.ObtenerValorDB("INPC", "Importe", " datepart(month,fecha)=" & fecha.Substring(3, 2) & " AND datepart(year,fecha)=" & Me.LstAnio.Text.Trim() & "", True)
            End If
            Try
                If Me.TablaVentasActivos.Item(ImpGF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value + Me.TablaVentasActivos.Item(ImpExF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value > 0 Then
                    Dim ImpInpc As Decimal = Me.TablaVentasActivos.Item(FactorUltm.Index, Me.TablaVentasActivos.CurrentRow.Index).Value / Me.TablaVentasActivos.Item(InpcFecha.Index, Me.TablaVentasActivos.CurrentRow.Index).Value
                    Me.TablaVentasActivos.Item(FactorActual.Index, Me.TablaVentasActivos.CurrentRow.Index).Value = Math.Truncate((IIf(ImpInpc < 1, 1, ImpInpc)) * 10000) / 10000
                    Me.TablaVentasActivos.Item(CostoFinal.Index, Me.TablaVentasActivos.CurrentRow.Index).Value = Math.Truncate((Me.TablaVentasActivos.Item(MoiPD.Index, Me.TablaVentasActivos.CurrentRow.Index).Value * Me.TablaVentasActivos.Item(FactorActual.Index, Me.TablaVentasActivos.CurrentRow.Index).Value) * 100) / 100
                    Me.TablaVentasActivos.Item(PrecioVentaF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value = Me.TablaVentasActivos.Item(ImpGF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value + Me.TablaVentasActivos.Item(ImpExF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value
                    Me.TablaVentasActivos.Item(UtilidadFisca.Index, Me.TablaVentasActivos.CurrentRow.Index).Value = IIf(Me.TablaVentasActivos.Item(PrecioVentaF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value > Me.TablaVentasActivos.Item(CostoFinal.Index, Me.TablaVentasActivos.CurrentRow.Index).Value, Me.TablaVentasActivos.Item(PrecioVentaF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value - Me.TablaVentasActivos.Item(CostoFinal.Index, Me.TablaVentasActivos.CurrentRow.Index).Value, 0)
                    Me.TablaVentasActivos.Item(PerdidaFiscal.Index, Me.TablaVentasActivos.CurrentRow.Index).Value = IIf(Me.TablaVentasActivos.Item(CostoFinal.Index, Me.TablaVentasActivos.CurrentRow.Index).Value > Me.TablaVentasActivos.Item(PrecioVentaF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value, Me.TablaVentasActivos.Item(CostoFinal.Index, Me.TablaVentasActivos.CurrentRow.Index).Value - Me.TablaVentasActivos.Item(PrecioVentaF.Index, Me.TablaVentasActivos.CurrentRow.Index).Value, 0)

                End If
            Catch ex As Exception

            End Try

        End If
    End Sub
    Private Sub BuscarFactura(ByVal UUID As String, ByVal I As Integer)
        Dim sql As String = "SELECT Xml_Sat.Imp_Grabado , Xml_Sat.Imp_Exento , Xml_Sat.IVA_real ,Xml_Sat.Total_Real,Xml_Sat.Folio   FROM Xml_Sat WHERE Id_Empresa = " & Me.lstCliente.SelectItem & " AND UUID ='" & UUID & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaVentasActivos.Item(ImpGF.Index, I).Value = IIf(IsDBNull(ds.Tables(0).Rows(0)(0)), 0, ds.Tables(0).Rows(0)(0))
            Me.TablaVentasActivos.Item(ImpExF.Index, I).Value = IIf(IsDBNull(ds.Tables(0).Rows(0)(1)), 0, ds.Tables(0).Rows(0)(1))
            Me.TablaVentasActivos.Item(IvaF.Index, I).Value = IIf(IsDBNull(ds.Tables(0).Rows(0)(2)), 0, ds.Tables(0).Rows(0)(2))
            Me.TablaVentasActivos.Item(NumFactura.Index, I).Value = IIf(IsDBNull(ds.Tables(0).Rows(0)(4)), "", ds.Tables(0).Rows(0)(4))
        End If

    End Sub
    Private Class Titulos_Resumen
        Public Property Titulo As String
        Public Property Tipo As Integer
        Public Property Suma As Integer
    End Class
    Private Sub CalculaResumen()
        Dim Titulos As New List(Of Titulos_Resumen)
        Dim Titulo As DataGridViewRow
        Dim Moi As Decimal = 0
        Dim Moi2 As Decimal = 0
        Dim ImpGra As Decimal = 0
        Dim ImpExx As Decimal = 0
        Dim Depact As Decimal = 0
        Dim Tlibros As Decimal = 0
        Dim PrecioV As Decimal = 0
        Dim Utilidad As Decimal = 0
        Dim Perdida As Decimal = 0
        Dim Perdida2 As Decimal = 0
        Dim Utilidad2 As Decimal = 0
        Dim Posicion As Integer = 0
        For j As Integer = 0 To Me.TablaVentasActivos.Rows.Count - 1
            Titulo = Me.TablaVentasActivos.Rows(j)
            If Titulo.DefaultCellStyle.BackColor = Color.LawnGreen Then
                Titulos.Add(New Titulos_Resumen() With {.Titulo = Me.TablaVentasActivos.Item(Ref.Index, j).Value.ToString.Trim(), .Tipo = Me.TablaVentasActivos.Item(Tipo.Index, j).Value, .Suma = Me.TablaVentasActivos.Item(Sumas.Index, j).Value})
            End If
        Next
        Me.TablaResumen.RowCount = Titulos.Count + 1

        For Each Titul In Titulos
            Moi = 0
            Moi2 = 0
            ImpGra = 0
            Utilidad = 0
            ImpExx = 0
            Depact = 0
            Tlibros = 0
            PrecioV = 0
            Perdida = 0
            Perdida2 = 0
            Utilidad2 = 0
            Me.TablaResumen.Item(DescripR.Index, Posicion).Value = Titul.Titulo


            For j As Integer = 0 To Me.TablaVentasActivos.Rows.Count - 1
                If Me.TablaVentasActivos.Item(Tipo.Index, j).Value = Titul.Tipo Then
                    Moi = Moi + Me.TablaVentasActivos.Item(MOIF.Index, j).Value
                    Depact = Depact + Me.TablaVentasActivos.Item(DepAcuF.Index, j).Value
                    ImpGra = ImpGra + Me.TablaVentasActivos.Item(ImpGF.Index, j).Value
                    ImpExx = ImpExx + Me.TablaVentasActivos.Item(ImpExF.Index, j).Value
                    Tlibros = Tlibros + Me.TablaVentasActivos.Item(ValorlibrosF.Index, j).Value
                    PrecioV = PrecioV + Me.TablaVentasActivos.Item(PrecioVenta.Index, j).Value
                    Utilidad = Utilidad + Me.TablaVentasActivos.Item(UtilidadContable.Index, j).Value
                    Perdida = Perdida + Me.TablaVentasActivos.Item(PerdidaContable.Index, j).Value

                    Moi2 = Moi2 + Me.TablaVentasActivos.Item(MoiPD.Index, j).Value
                    Utilidad2 = Utilidad2 + Me.TablaVentasActivos.Item(UtilidadFisca.Index, j).Value
                    Perdida2 = Perdida2 + Me.TablaVentasActivos.Item(PerdidaFiscal.Index, j).Value
                End If
            Next

            For j As Integer = 0 To Me.TablaVentasActivos.Rows.Count - 1
                If Me.TablaVentasActivos.Item(Sumas.Index, j).Value = Titul.Suma And Me.TablaVentasActivos.Item(Tipo.Index, j).Value = 40 Then

                    Me.TablaVentasActivos.Item(MOIF.Index, j).Value = Moi
                    Me.TablaVentasActivos.Item(DepAcuF.Index, j).Value = Depact
                    Me.TablaVentasActivos.Item(ImpGF.Index, j).Value = ImpGra
                    Me.TablaVentasActivos.Item(ImpExF.Index, j).Value = ImpExx
                    Me.TablaVentasActivos.Item(ValorlibrosF.Index, j).Value = Tlibros
                    Me.TablaVentasActivos.Item(PrecioVenta.Index, j).Value = PrecioV
                    Me.TablaVentasActivos.Item(UtilidadContable.Index, j).Value = Utilidad
                    Me.TablaVentasActivos.Item(PerdidaContable.Index, j).Value = Perdida
                    Me.TablaVentasActivos.Item(UtilidadFisca.Index, j).Value = Utilidad2
                    Me.TablaVentasActivos.Item(PerdidaFiscal.Index, j).Value = Perdida2
                End If
            Next
            Me.TablaResumen.Item(UtilidadC.Index, Posicion).Value = Utilidad
            Me.TablaResumen.Item(PerdidaC.Index, Posicion).Value = Perdida
            Me.TablaResumen.Item(UtilidadF.Index, Posicion).Value = Utilidad2
            Me.TablaResumen.Item(PerdidaF.Index, Posicion).Value = Perdida2

            Posicion += 1

        Next
        Dim Sal As Decimal = 0
        Dim dif As Decimal = 0
        Me.TablaResumen.Item(DescripR.Index, Posicion).Value = "Total"

        For j As Integer = 1 To Me.TablaResumen.Columns.Count - 1
            For i As Integer = 0 To Me.TablaResumen.Rows.Count - 2
                Sal += Me.TablaResumen.Item(j, i).Value
            Next
            Me.TablaResumen.Item(j, Posicion).Value = Sal
            Sal = 0
        Next
        ' Posicion += 1
    End Sub

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.lstCliente.SelectText <> "" Then
            If RadMessageBox.Show("Se actualizara la informacion de la Empresa  " & Me.lstCliente.SelectText & " para el año " & anio & " esto es correcto?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
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
End Class