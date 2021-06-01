Imports Telerik.WinControls
Imports System.IO
Public Class PolizasMasivas
    Dim Activo As Boolean
    Dim mes = Now.Date.Month.ToString
    Dim anio = Str(DateTime.Now.Year)
    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.Clear()
        End If
    End Sub

    Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
        Dim sql As String = ""
        Me.CmdLimpiar.PerformClick()
        If Me.lstCliente.SelectText <> "" Then

            If Me.Tabla.Rows.Count > 0 Then
                Me.Tabla.Rows.Clear()
            End If
            Activo = True



            If Me.ComboMes.Text = "*" Then
                    sql = " Polizas.id_anio = '" & Trim(Me.comboAño.Text) & "' "
                Else
                    sql = " Polizas.id_anio = '" & Trim(Me.comboAño.Text) & "' and Polizas.id_mes= '" & Trim(Me.ComboMes.Text) & "'"
                End If
                Cargar_Polizas(sql, Me.lstCliente.SelectItem)


            Dim Tabla As String = ""
            Dim Id As String = ""
            Dim origen As String = ""

            Tabla = "Detalle_Polizas"
            Id = "ID_poliza"
            origen = "Polizas"

            Activo = False

        End If
    End Sub
    Private Sub Cargar_Polizas(ByVal consulta As String, ByVal cliente As Integer)
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim Datos As String = "select  DISTINCT A.ID_anio, A.ID_mes, A.Clave, A.Num_Pol , A.Poliza,A.Aplicar_Poliza,B.ID_dia,B.Fecha_captura, B.Concepto,C.Importe from "
        Datos &= " (SELECT Polizas.ID_anio, "
        Datos &= " Polizas.ID_mes, Tipos_Poliza_Sat.Clave+'-'+ Tipos_Poliza_Sat.Nombre as Clave, "
        Datos &= " Convert(int,Polizas.Num_Pol) as Num_Pol , "
        Datos &= " Convert (BIGINT ,Polizas.ID_anio + + Polizas.ID_mes + + Tipos_Poliza_Sat.clave + + REPLACE(STR(rtrim(Polizas.Num_Pol), 6), SPACE(1), '0')) as Poliza,"
        Datos &= " CASE WHEN Polizas.Aplicar_Poliza =1 THEN 'SI' ELSE 'NO' END AS Aplicar_Poliza"
        Datos &= " FROM     Polizas "
        Datos &= " INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa "
        Datos &= " INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat"
        Datos &= " WHERE  " & consulta & " and  (Empresa.Id_Empresa = " & cliente & ")) as A  "
        Datos &= " LEFT JOIN ( SELECT Polizas.ID_dia,  Polizas.Fecha_captura, Polizas.Concepto, "
        Datos &= " Polizas.ID_anio + + Polizas.ID_mes + + Tipos_Poliza_Sat.clave + + REPLACE(STR(rtrim(Polizas.Num_Pol), 6),SPACE(1), '0')  as Poliza"
        Datos &= "  FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa "
        Datos &= "  INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat "
        Datos &= "  WHERE  " & consulta & "  and  (Empresa.Id_Empresa = " & cliente & ")  "
        Datos &= " ) AS B ON B.Poliza = A.Poliza"
        Datos &= "  LEFT JOIN (  SELECT  SUM( Detalle_Polizas.Cargo)AS Importe ,  "
        Datos &= " Polizas.ID_anio + + Polizas.ID_mes + + Tipos_Poliza_Sat.clave + + REPLACE(STR(rtrim(Polizas.Num_Pol), 6), SPACE(1), '0')  as Poliza "
        Datos &= " FROM     Polizas INNER JOIN Empresa ON    Empresa.Id_Empresa = Polizas.Id_Empresa "
        Datos &= " INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat INNER JOIN Detalle_Polizas  ON Detalle_Polizas.ID_poliza  = Polizas.ID_poliza "
        Datos &= " INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa WHERE    (Empresa.Id_Empresa = " & cliente & ") "
        Datos &= " GROUP  BY Polizas.ID_anio , Polizas.ID_mes , rtrim(Polizas.Num_Pol) , Tipos_Poliza_Sat.clave ) AS C "
        Datos &= "  ON C.Poliza = A.Poliza order by Poliza"

        Dim posicion As Integer
        Dim sql As String = "select  DISTINCT * from (SELECT Polizas.ID_anio, Polizas.ID_mes, Tipos_Poliza_Sat.Clave+'-'+ Tipos_Poliza_Sat.Nombre as Clave, Convert (int ,Polizas.Num_Pol) as Num_Pol, Polizas.ID_anio + + Polizas.ID_mes + + rtrim(Polizas.Num_Pol) + + Tipos_Poliza_Sat.clave as Poliza
                                     FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa INNER JOIN
                                    Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat WHERE " & consulta & " and  (Empresa.Id_Empresa = " & cliente & ")) as Tabla order by Poliza"
        Dim ds As DataSet = Eventos.Obtener_DS(Datos)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.RowCount = ds.Tables(0).Rows.Count
            Me.lblRegistros.Text = "Total de Polizas: " & ds.Tables(0).Rows.Count
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.Tabla.RowCount - 1
            Me.Cursor = Cursors.AppStarting
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Me.Tabla.Item(An.Index, i).Value = Trim(ds.Tables(0).Rows(i)("ID_anio"))
                Me.Tabla.Item(MS.Index, i).Value = Trim(ds.Tables(0).Rows(i)("ID_mes"))
                Me.Tabla.Item(Tpol.Index, i).Value = Trim(ds.Tables(0).Rows(i)("Clave"))
                Me.Tabla.Item(Np.Index, i).Value = Trim(ds.Tables(0).Rows(i)("Num_Pol"))
                Me.Tabla.Item(ImpPol.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(i)("Importe")), 0, ds.Tables(0).Rows(i)("Importe"))
                Me.Tabla.Item(Dp.Index, i).Value = Trim(ds.Tables(0).Rows(i)("ID_dia"))
                Me.Tabla.Item(Des.Index, i).Value = Trim(ds.Tables(0).Rows(i)("Concepto"))
                Me.Tabla.Item(Calculada.Index, i).Value = Trim(ds.Tables(0).Rows(i)("Poliza"))
                frm.Barra.Value = i
            Next
            frm.Close()
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow
        Else
            Me.CmdLimpiar.PerformClick()
        End If
    End Sub
    Private Sub ChkTodos_CheckedChanged(sender As Object, e As EventArgs) Handles ChkTodos.CheckedChanged
        If Me.ChkTodos.Checked = True Then
            For i As Integer = 0 To Me.Tabla.Rows.Count - 1
                Me.Tabla.Item(Seleccion.Index, i).Value = True
            Next
        Else
            For i As Integer = 0 To Me.Tabla.Rows.Count - 1
                Me.Tabla.Item(Seleccion.Index, i).Value = False
            Next
        End If
    End Sub

    Private Sub PolizasMasivas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_Listas()
        Dim i As Integer
        For i = DateTime.Now.Year To DateTime.Now.Year - 10 Step -1
            If i >= 2006 Then
                Me.comboAño.Items.Add(Str(i))
            End If
        Next
        Me.comboAño.Items.Add("*")
        Me.comboAño.Text = anio
        Me.ComboMes.Items.Add("*")
        If Len(mes) < 2 Then
            mes = "0" & mes
        End If
        Eventos.DiseñoTablaEnca(Me.Tabla)
        Me.ComboMes.Text = mes
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

    Private Sub CmdPdf_Click(sender As Object, e As EventArgs) Handles CmdPdf.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        For i As Integer = 0 To Me.Tabla.Rows.Count - 1
            If Me.Tabla.Item(Seleccion.Index, i).Value = True Then
                PolizaPDF(i)
            End If
        Next
        RadMessageBox.Show("Proceso Terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
    End Sub
    Private Sub PolizaPDF(ByVal P As Integer)
        Dim dia As String
        Dim DT As New DataTable
        With DT
            .Columns.Add("Id_Poliza")
            .Columns.Add("Anio")
            .Columns.Add("Mes")
            .Columns.Add("Dia")
            .Columns.Add("Numero")
            .Columns.Add("Tipo")
            .Columns.Add("Concepto")
        End With
        Try
            DT.Rows.Add(Me.Tabla.Item(Calculada.Index, P).Value, Me.Tabla.Item(An.Index, P).Value, Eventos.MesEnletra(Me.Tabla.Item(MS.Index, P).Value), Me.Tabla.Item(Dp.Index, P).Value, Me.Tabla.Item(Np.Index, P).Value.ToString.PadLeft(6, "0"), Me.Tabla.Item(Tpol.Index, P).Value, Me.Tabla.Item(Des.Index, P).Value)
            dia = Me.Tabla.Item(Dp.Index, P).Value
        Catch ex As Exception

        End Try



        Dim DT2 As New DataTable
        With DT2
            .Columns.Add("Id_Poliza")
            .Columns.Add("Cuenta")
            .Columns.Add("DescMov")
            .Columns.Add("Importe", Type.GetType("System.Decimal"))
            .Columns.Add("NombreCuenta")
            .Columns.Add("TipoTrans")
            .Columns.Add("RFC")
            .Columns.Add("Monto", Type.GetType("System.Decimal"))
            .Columns.Add("Fecha")
            .Columns.Add("UUID")
            .Columns.Add("BancoO")
            .Columns.Add("CuentaO")
            .Columns.Add("BancoD")
            .Columns.Add("CuentaD")
            .Columns.Add("Cargo", Type.GetType("System.Decimal"))
            .Columns.Add("Abono", Type.GetType("System.Decimal"))
            .Columns.Add("Dia")
        End With


#Region "Polizas Contaf"

        Dim Cargos As String = "SELECT * FROM (
                                        SELECT Polizas.ID_anio, Polizas.ID_mes, Tipos_Poliza_Sat.Clave, Polizas.Num_Pol,Polizas.ID_dia, Detalle_Polizas.Cuenta,  '1'   AS Movimiento,Detalle_Polizas.Cargo AS Importe,
                                           rtrim(Catalogo_de_Cuentas.Descripcion ) AS Nom_Cuenta,
                                        Polizas.Concepto, Polizas.Fecha , 
                                        Polizas.ID_anio + + Polizas.ID_mes + + Tipos_Poliza_Sat.clave + + REPLACE(STR(rtrim(Polizas.Num_Pol), 6), SPACE(1), '0')   as Poliza,Polizas.Id_Poliza,Detalle_Polizas.id_item
                                        FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                        INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                        INNER JOIN Detalle_Polizas  ON Polizas.Id_Poliza = Detalle_Polizas.Id_Poliza
                                        INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                        WHERE    (Empresa.Id_Empresa = " & Me.lstCliente.SelectItem & ") AND Detalle_Polizas.Cargo <>0) AS tabla 
                                        WHERE Poliza ='" & Trim(Me.Tabla.Item(Calculada.Index, P).Value) & "' ORDER BY  Id_Poliza, id_item"
            Dim car As DataSet = Eventos.Obtener_DS(Cargos)
            If car.Tables(0).Rows.Count > 0 Then


            For j As Integer = 0 To car.Tables(0).Rows.Count - 1
                Dim madre As String = ""
                Dim Nombre As String = ""
                Dim Cadena As String = car.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & "-" & car.Tables(0).Rows(j)(5).ToString.Substring(4, 4) & "-" & car.Tables(0).Rows(j)(5).ToString.Substring(8, 4) & "-" & car.Tables(0).Rows(j)(5).ToString.Substring(12, 4)

                If car.Tables(0).Rows(j)(5).ToString.Substring(12, 4) = "0000" And car.Tables(0).Rows(j)(5).ToString.Substring(8, 4) = "0000" And car.Tables(0).Rows(j)(5).ToString.Substring(4, 4) <> "0000" Then ' Nivel2
                    madre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Nivel1 ", " Nivel1 = " & car.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '0000' and Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    Nombre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Descripcion", " Nivel1 = " & car.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '0000' and Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    DT2.Rows.Add(Trim(car.Tables(0).Rows(j)(11)), madre, Nothing, Nothing, Nombre, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                ElseIf car.Tables(0).Rows(j)(5).ToString.Substring(12, 4) = "0000" And car.Tables(0).Rows(j)(5).ToString.Substring(8, 4) <> "0000" And car.Tables(0).Rows(j)(5).ToString.Substring(4, 4) <> "0000" Then ' Nivel3
                    madre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Nivel1", " Nivel1 = " & car.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '0000' and Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    Nombre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Descripcion", " Nivel1 = " & car.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '0000' and Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    DT2.Rows.Add(Trim(car.Tables(0).Rows(j)(11)), madre, Nothing, Nothing, Nombre, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    madre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Nivel1 + '-' + Nivel2", " Nivel1 = " & car.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '" & car.Tables(0).Rows(j)(5).ToString.Substring(4, 4) & "' and Nivel3 = '0000' and  Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    Nombre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Descripcion", " Nivel1 = " & car.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '" & car.Tables(0).Rows(j)(5).ToString.Substring(4, 4) & "' and Nivel3 = '0000' and Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    DT2.Rows.Add(Trim(car.Tables(0).Rows(j)(11)), madre, Nothing, Nothing, Nombre, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                ElseIf car.Tables(0).Rows(j)(5).ToString.Substring(12, 4) <> "0000" And car.Tables(0).Rows(j)(5).ToString.Substring(8, 4) <> "0000" And car.Tables(0).Rows(j)(5).ToString.Substring(4, 4) <> "0000" Then ' Nivel4
                    madre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Nivel1  ", " Nivel1 = " & car.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '0000' and Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    Nombre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Descripcion", " Nivel1 = " & car.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '0000' and Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    DT2.Rows.Add(Trim(car.Tables(0).Rows(j)(11)), madre, Nothing, Nothing, Nombre, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    madre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Nivel1 + '-' + Nivel2", " Nivel1 = " & car.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '" & car.Tables(0).Rows(j)(5).ToString.Substring(4, 4) & "' and Nivel3 = '0000' and  Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    Nombre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Descripcion", " Nivel1 = " & car.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '" & car.Tables(0).Rows(j)(5).ToString.Substring(4, 4) & "' and Nivel3 = '0000' and Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    DT2.Rows.Add(Trim(car.Tables(0).Rows(j)(11)), madre, Nothing, Nothing, Nombre, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    madre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Nivel1 + '-' + Nivel2 + '-'+ Nivel3", " Nivel1 = " & car.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '" & car.Tables(0).Rows(j)(5).ToString.Substring(4, 4) & "' and Nivel3 = '" & car.Tables(0).Rows(j)(5).ToString.Substring(8, 4) & "' and Nivel4='0000' and  Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    Nombre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Descripcion", " Nivel1 = " & car.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '" & car.Tables(0).Rows(j)(5).ToString.Substring(4, 4) & "' and Nivel3 = '" & car.Tables(0).Rows(j)(5).ToString.Substring(8, 4) & "' and Nivel4='0000'  and Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    DT2.Rows.Add(Trim(car.Tables(0).Rows(j)(11)), madre, Nothing, Nothing, Nombre, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                End If

                DT2.Rows.Add(Trim(car.Tables(0).Rows(j)(11)), Trim(Cadena), Trim(car.Tables(0).Rows(j)(9)), Trim(car.Tables(0).Rows(j)(7)), Trim(car.Tables(0).Rows(j)(8)), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, car.Tables(0).Rows(j)(7), Nothing, dia)

                'Buscar facturas
                Dim Facturas As String = " SELECT Facturas.ID_anio, Facturas.ID_mes,  Polizas.Num_Pol,Tipos_Poliza_Sat.Clave,Polizas.ID_dia, Facturas.RFC_Emisor, Facturas.Fecha_Comprobante, 
                                                        Facturas.Importe, Facturas.Folio_Fiscal,Polizas.Id_Poliza 
                                                        FROM     Facturas INNER JOIN   Polizas ON Facturas.Id_Poliza = Polizas.Id_Poliza
                                                        INNER JOIN Tipos_Poliza_Sat ON Tipos_Poliza_Sat.Id_Tipo_Pol_Sat = Polizas.Id_Tipo_Pol_Sat 
                                                        WHERE   (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.Id_Poliza = '" & Trim(car.Tables(0).Rows(j)(12)) & "'  "
                Dim Fact As DataSet = Eventos.Obtener_DS(Facturas)
                If Fact.Tables(0).Rows.Count > 0 Then

                    For H As Integer = 0 To Fact.Tables(0).Rows.Count - 1
                        DT2.Rows.Add(Trim(car.Tables(0).Rows(j)(11)), Nothing, Nothing, Nothing, Nothing, "Comprobante", Trim(Fact.Tables(0).Rows(H)("RFC_Emisor")), Trim(Fact.Tables(0).Rows(H)("Importe")), Trim(Fact.Tables(0).Rows(H)("Fecha_Comprobante")), Trim(Fact.Tables(0).Rows(H)("Folio_Fiscal")), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    Next
                End If
                'Buscar Contabilidad Electronica
                'Efectivo
                Dim Efectivo As String = " SELECT Conta_E_Sistema.Anio, Conta_E_Sistema.Mes, Conta_E_Sistema.Tipo, Polizas.Num_Pol, Conta_E_Sistema.RFC_Ce, 
                                                        Conta_E_Sistema.Fecha_Mov, Conta_E_Sistema.Importe FROM     Polizas INNER JOIN  Conta_E_Sistema ON Polizas.Id_Poliza = 
                                                        Conta_E_Sistema.Id_Poliza
                                                        WHERE (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.Id_Poliza = '" & Trim(car.Tables(0).Rows(j)(12)) & "' AND Conta_E_Sistema.Tipo_CE = 'P'"
                Dim Efect As DataSet = Eventos.Obtener_DS(Efectivo)
                If Efect.Tables(0).Rows.Count > 0 Then
                    For H As Integer = 0 To Fact.Tables(0).Rows.Count - 1
                        DT2.Rows.Add(Trim(car.Tables(0).Rows(j)(11)), Nothing, Nothing, Nothing, Nothing, "Efectivo", Trim(Efect.Tables(0).Rows(H)("RFC_Ce")), Trim(Efect.Tables(0).Rows(H)("Importe")), Trim(Efect.Tables(0).Rows(H)("Fecha_Mov")), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    Next
                End If
                'Transferencia
                Dim Transferencia As String = " SELECT Conta_E_Sistema.Anio, Conta_E_Sistema.Mes, Conta_E_Sistema.Tipo, Polizas.Num_Pol, Conta_E_Sistema.RFC_Ce,Conta_E_Sistema.No_banco,  Conta_E_Sistema.Cuenta_Origen ,  
                                                            Conta_E_Sistema.Fecha_Mov, Conta_E_Sistema.Importe, Conta_E_Sistema.Banco_Destino , Conta_E_Sistema.Cuenta_Destino FROM     Polizas INNER JOIN  Conta_E_Sistema ON Polizas.Id_Poliza = 
                                                            Conta_E_Sistema.Id_Poliza
                                                            WHERE (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.Id_Poliza = '" & Trim(car.Tables(0).Rows(j)(12)) & "' AND Conta_E_Sistema.Tipo_CE = 'T'"
                Dim trans As DataSet = Eventos.Obtener_DS(Transferencia)
                If trans.Tables(0).Rows.Count > 0 Then

                    For E As Integer = 0 To trans.Tables(0).Rows.Count - 1
                        DT2.Rows.Add(Trim(car.Tables(0).Rows(j)(11)), Nothing, Nothing, Nothing, Nothing, "Transferencia", Trim(trans.Tables(0).Rows(E)("RFC_Ce")), Trim(trans.Tables(0).Rows(E)("Importe")), Trim(trans.Tables(0).Rows(E)("Fecha_Mov")), Nothing, Trim(Eventos.ObtenerValorDB("Bancos", "Nombre", "Clave = '" & trans.Tables(0).Rows(E)("No_banco") & "'", True)), Trim(trans.Tables(0).Rows(E)("Cuenta_Origen")), Trim(Eventos.ObtenerValorDB("Bancos", "Nombre", "Clave = '" & trans.Tables(0).Rows(E)("Banco_Destino") & "'", True)), Trim(trans.Tables(0).Rows(E)("Cuenta_Destino")), Nothing, Nothing, Nothing)
                    Next
                End If
                'Cheque
                Dim Cheque As String = " Select Conta_E_Sistema.Anio, Conta_E_Sistema.Mes, Conta_E_Sistema.Tipo, Polizas.Num_Pol, Conta_E_Sistema.RFC_Ce, Conta_E_Sistema.No_Cheque, Conta_E_Sistema.No_banco, Conta_E_Sistema.Cuenta_Origen,
                                                            Conta_E_Sistema.Fecha_Mov, Conta_E_Sistema.Importe FROM     Polizas INNER JOIN  Conta_E_Sistema ON Polizas.Id_Poliza = 
                                                            Conta_E_Sistema.Id_Poliza
                                                            WHERE (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.Id_Poliza = '" & Trim(car.Tables(0).Rows(j)(12)) & "' AND Conta_E_Sistema.Tipo_CE = 'H'"
                Dim Chec As DataSet = Eventos.Obtener_DS(Cheque)
                If Chec.Tables(0).Rows.Count > 0 Then

                    For E As Integer = 0 To Chec.Tables(0).Rows.Count - 1
                        DT2.Rows.Add(Trim(car.Tables(0).Rows(j)(11)), Nothing, Nothing, Nothing, Nothing, "Cheque", Trim(Chec.Tables(0).Rows(E)("RFC_Ce")), Trim(Chec.Tables(0).Rows(E)("Importe")), Trim(Chec.Tables(0).Rows(E)("Fecha_Mov")), Nothing, Trim(Eventos.ObtenerValorDB("Bancos", "Nombre", "Clave = '" & Chec.Tables(0).Rows(E)("No_banco") & "'", True)), Trim(Chec.Tables(0).Rows(E)("Cuenta_Origen")), Nothing, Nothing, Nothing, Nothing, Nothing)
                    Next
                End If

            Next

        End If
        ' Se cargan los Abonos
        Dim Abonos As String = "SELECT * FROM (
                                            SELECT Polizas.ID_anio, Polizas.ID_mes, Tipos_Poliza_Sat.Clave, Polizas.Num_Pol,Polizas.ID_dia, Detalle_Polizas.Cuenta,  '2'   AS Movimiento,Detalle_Polizas.abono AS Importe,
                                               rtrim(Catalogo_de_Cuentas.Descripcion ) AS Nom_Cuenta,
                                            Polizas.Concepto, Polizas.Fecha   , 
                                            Polizas.ID_anio + + Polizas.ID_mes + + Tipos_Poliza_Sat.clave + + REPLACE(STR(rtrim(Polizas.Num_Pol), 6), SPACE(1), '0')   as Poliza,Polizas.Id_Poliza,Detalle_Polizas.id_item
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas  ON Polizas.Id_Poliza = Detalle_Polizas.Id_Poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE    (Empresa.Id_Empresa = " & Me.lstCliente.SelectItem & ") AND Detalle_Polizas.abono  <> 0) AS tabla 
                                            WHERE Poliza ='" & Trim(Me.Tabla.Item(Calculada.Index, P).Value) & "' ORDER BY  Id_Poliza, id_item "
        Dim Ab As DataSet = Eventos.Obtener_DS(Abonos)
        If Ab.Tables(0).Rows.Count > 0 Then

            For j As Integer = 0 To Ab.Tables(0).Rows.Count - 1
                Dim madre As String = ""
                Dim Nombre As String = ""
                Dim Cadena As String = Ab.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & "-" & Ab.Tables(0).Rows(j)(5).ToString.Substring(4, 4) & "-" & Ab.Tables(0).Rows(j)(5).ToString.Substring(8, 4) & "-" & Ab.Tables(0).Rows(j)(5).ToString.Substring(12, 4)

                If Ab.Tables(0).Rows(j)(5).ToString.Substring(12, 4) = "0000" And Ab.Tables(0).Rows(j)(5).ToString.Substring(8, 4) = "0000" And Ab.Tables(0).Rows(j)(5).ToString.Substring(4, 4) <> "0000" Then ' Nivel2
                    madre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Nivel1 ", " Nivel1 = " & Ab.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '0000' and Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    Nombre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Descripcion", " Nivel1 = " & Ab.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '0000' and Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    DT2.Rows.Add(Trim(Ab.Tables(0).Rows(j)(11)), madre, Nothing, Nothing, Nombre, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                ElseIf Ab.Tables(0).Rows(j)(5).ToString.Substring(12, 4) = "0000" And Ab.Tables(0).Rows(j)(5).ToString.Substring(8, 4) <> "0000" And Ab.Tables(0).Rows(j)(5).ToString.Substring(4, 4) <> "0000" Then ' Nivel3
                    madre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Nivel1", " Nivel1 = " & Ab.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '0000' and Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    Nombre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Descripcion", " Nivel1 = " & Ab.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '0000' and Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    DT2.Rows.Add(Trim(Ab.Tables(0).Rows(j)(11)), madre, Nothing, Nothing, Nombre, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    madre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Nivel1 + '-' + Nivel2", " Nivel1 = " & Ab.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '" & Ab.Tables(0).Rows(j)(5).ToString.Substring(4, 4) & "' and Nivel3 = '0000' and  Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    Nombre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Descripcion", " Nivel1 = " & Ab.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '" & Ab.Tables(0).Rows(j)(5).ToString.Substring(4, 4) & "' and Nivel3 = '0000' and Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    DT2.Rows.Add(Trim(Ab.Tables(0).Rows(j)(11)), madre, Nothing, Nothing, Nombre, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                ElseIf Ab.Tables(0).Rows(j)(5).ToString.Substring(12, 4) <> "0000" And Ab.Tables(0).Rows(j)(5).ToString.Substring(8, 4) <> "0000" And Ab.Tables(0).Rows(j)(5).ToString.Substring(4, 4) <> "0000" Then ' Nivel4
                    madre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Nivel1  ", " Nivel1 = " & Ab.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '0000' and Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    Nombre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Descripcion", " Nivel1 = " & Ab.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '0000' and Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    DT2.Rows.Add(Trim(Ab.Tables(0).Rows(j)(11)), madre, Nothing, Nothing, Nombre, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    madre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Nivel1 + '-'+ Nivel2", " Nivel1 = " & Ab.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '" & Ab.Tables(0).Rows(j)(5).ToString.Substring(4, 4) & "' and Nivel3 = '0000' and  Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    Nombre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Descripcion", " Nivel1 = " & Ab.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '" & Ab.Tables(0).Rows(j)(5).ToString.Substring(4, 4) & "' and Nivel3 = '0000' and Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    DT2.Rows.Add(Trim(Ab.Tables(0).Rows(j)(11)), madre, Nothing, Nothing, Nombre, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    madre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Nivel1 + '-'+ Nivel2 + '-'+ Nivel3", " Nivel1 = " & Ab.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '" & Ab.Tables(0).Rows(j)(5).ToString.Substring(4, 4) & "' and Nivel3 = '" & Ab.Tables(0).Rows(j)(5).ToString.Substring(8, 4) & "' and Nivel4='0000' and  Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    Nombre = Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Descripcion", " Nivel1 = " & Ab.Tables(0).Rows(j)(5).ToString.Substring(0, 4) & " and Nivel2 = '" & Ab.Tables(0).Rows(j)(5).ToString.Substring(4, 4) & "' and Nivel3 = '" & Ab.Tables(0).Rows(j)(5).ToString.Substring(8, 4) & "' and Nivel4='0000'  and Id_Empresa = '" & Me.lstCliente.SelectItem & "'", True)
                    DT2.Rows.Add(Trim(Ab.Tables(0).Rows(j)(11)), madre, Nothing, Nothing, Nombre, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                End If



                DT2.Rows.Add(Trim(Ab.Tables(0).Rows(j)(11)), Trim(Cadena), Trim(Ab.Tables(0).Rows(j)(9)), Trim(Ab.Tables(0).Rows(j)(7)), Trim(Ab.Tables(0).Rows(j)(8)), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Ab.Tables(0).Rows(j)(7), dia)


                'Buscar facturas
                Dim Facturas As String = " SELECT Facturas.ID_anio, Facturas.ID_mes,  Polizas.Num_Pol,Tipos_Poliza_Sat.Clave,Polizas.ID_dia, Facturas.RFC_Emisor, Facturas.Fecha_Comprobante, 
                                                        Facturas.Importe, Facturas.Folio_Fiscal,Polizas.Id_Poliza 
                                                        FROM     Facturas INNER JOIN   Polizas ON Facturas.Id_Poliza = Polizas.Id_Poliza
                                                        INNER JOIN Tipos_Poliza_Sat ON Tipos_Poliza_Sat.Id_Tipo_Pol_Sat = Polizas.Id_Tipo_Pol_Sat 
                                                        WHERE   (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.Id_Poliza = '" & Trim(Ab.Tables(0).Rows(j)(12)) & "'  "
                Dim Fact As DataSet = Eventos.Obtener_DS(Facturas)
                If Fact.Tables(0).Rows.Count > 0 Then

                    For H As Integer = 0 To Fact.Tables(0).Rows.Count - 1
                        DT2.Rows.Add(Trim(Ab.Tables(0).Rows(j)(11)), Nothing, Nothing, Nothing, Nothing, "Comprobante", Trim(Fact.Tables(0).Rows(H)("RFC_Emisor")), Trim(Fact.Tables(0).Rows(H)("Importe")), Trim(Fact.Tables(0).Rows(H)("Fecha_Comprobante")), Trim(Fact.Tables(0).Rows(H)("Folio_Fiscal")), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    Next
                End If
                'Buscar Contabilidad Electronica
                'Efectivo
                Dim Efectivo As String = " SELECT Conta_E_Sistema.Anio, Conta_E_Sistema.Mes, Conta_E_Sistema.Tipo, Polizas.Num_Pol, Conta_E_Sistema.RFC_Ce, 
                                                        Conta_E_Sistema.Fecha_Mov, Conta_E_Sistema.Importe FROM     Polizas INNER JOIN  Conta_E_Sistema ON Polizas.Id_Poliza = 
                                                        Conta_E_Sistema.Id_Poliza
                                                        WHERE (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.Id_Poliza = '" & Trim(Ab.Tables(0).Rows(j)(12)) & "' AND Conta_E_Sistema.Tipo_CE = 'P'"
                Dim Efect As DataSet = Eventos.Obtener_DS(Efectivo)
                If Efect.Tables(0).Rows.Count > 0 Then
                    For H As Integer = 0 To Fact.Tables(0).Rows.Count - 1
                        DT2.Rows.Add(Trim(Ab.Tables(0).Rows(j)(11)), Nothing, Nothing, Nothing, Nothing, "Efectivo", Trim(Efect.Tables(0).Rows(H)("RFC_Ce")), Trim(Efect.Tables(0).Rows(H)("Importe")), Trim(Efect.Tables(0).Rows(H)("Fecha_Mov")), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    Next
                End If
                'Transferencia
                Dim Transferencia As String = " SELECT Conta_E_Sistema.Anio, Conta_E_Sistema.Mes, Conta_E_Sistema.Tipo, Polizas.Num_Pol, Conta_E_Sistema.RFC_Ce,Conta_E_Sistema.No_banco,  Conta_E_Sistema.Cuenta_Origen ,  
                                                            Conta_E_Sistema.Fecha_Mov, Conta_E_Sistema.Importe, Conta_E_Sistema.Banco_Destino , Conta_E_Sistema.Cuenta_Destino FROM     Polizas INNER JOIN  Conta_E_Sistema ON Polizas.Id_Poliza = 
                                                            Conta_E_Sistema.Id_Poliza
                                                            WHERE (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.Id_Poliza = '" & Trim(Ab.Tables(0).Rows(j)(12)) & "' AND Conta_E_Sistema.Tipo_CE = 'T'"
                Dim trans As DataSet = Eventos.Obtener_DS(Transferencia)
                If trans.Tables(0).Rows.Count > 0 Then

                    For E As Integer = 0 To trans.Tables(0).Rows.Count - 1
                        DT2.Rows.Add(Trim(Ab.Tables(0).Rows(j)(11)), Nothing, Nothing, Nothing, Nothing, "Transferencia", Trim(trans.Tables(0).Rows(E)("RFC_Ce")), Trim(trans.Tables(0).Rows(E)("Importe")), Trim(trans.Tables(0).Rows(E)("Fecha_Mov")), Nothing, Trim(Eventos.ObtenerValorDB("Bancos", "Nombre", "Clave = '" & trans.Tables(0).Rows(E)("No_banco") & "'", True)), Trim(trans.Tables(0).Rows(E)("Cuenta_Origen")), Trim(Eventos.ObtenerValorDB("Bancos", "Nombre", "Clave = '" & trans.Tables(0).Rows(E)("Banco_Destino") & "'", True)), Trim(trans.Tables(0).Rows(E)("Cuenta_Destino")), Nothing, Nothing, Nothing)

                    Next
                End If
                'Cheque
                Dim Cheque As String = " SELECT Conta_E_Sistema.Anio, Conta_E_Sistema.Mes, Conta_E_Sistema.Tipo, Polizas.Num_Pol, Conta_E_Sistema.RFC_Ce,Conta_E_Sistema.No_Cheque,Conta_E_Sistema.No_banco,  Conta_E_Sistema.Cuenta_Origen ,  
                                                            Conta_E_Sistema.Fecha_Mov, Conta_E_Sistema.Importe FROM     Polizas INNER JOIN  Conta_E_Sistema ON Polizas.Id_Poliza = 
                                                            Conta_E_Sistema.Id_Poliza
                                                            WHERE (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.Id_Poliza = '" & Trim(Ab.Tables(0).Rows(j)(12)) & "' AND Conta_E_Sistema.Tipo_CE = 'H'"
                Dim Chec As DataSet = Eventos.Obtener_DS(Cheque)
                If Chec.Tables(0).Rows.Count > 0 Then

                    For E As Integer = 0 To Chec.Tables(0).Rows.Count - 1
                        DT2.Rows.Add(Trim(Ab.Tables(0).Rows(j)(11)), Nothing, Nothing, Nothing, Nothing, "Cheque", Trim(Chec.Tables(0).Rows(E)("RFC_Ce")), Trim(Chec.Tables(0).Rows(E)("Importe")), Trim(Chec.Tables(0).Rows(E)("Fecha_Mov")), Nothing, Trim(Eventos.ObtenerValorDB("Bancos", "Nombre", "Clave = '" & Chec.Tables(0).Rows(E)("No_banco") & "'", True)), Trim(Chec.Tables(0).Rows(E)("Cuenta_Origen")), Nothing, Nothing, Nothing, Nothing, Nothing)
                    Next
                End If
            Next
        End If
#End Region

        Try


            Dim Cr As New ReportePolizaPDF
            Cr.Database.Tables("Poliza").SetDataSource(DT)
            Cr.Database.Tables("DetallePoliza").SetDataSource(DT2)
            Dim Reporte As New ReporteBalanzaGeneral
            Reporte.CrvBalanza.ReportSource = Cr
            Dim Cliente As New CrystalDecisions.Shared.ParameterDiscreteValue()
            Dim Cl As New CrystalDecisions.Shared.ParameterValues()
            Cliente.Value = Me.lstCliente.SelectText
            Cl.Add(Cliente)
            Cr.DataDefinition.ParameterFields("Cliente").ApplyCurrentValues(Cl)
            Dim Clt As DataSet = Eventos.Obtener_DS("select * from Empresa where Id_Empresa = " & Me.lstCliente.SelectItem & " ")

            Cliente.Value = Clt.Tables(0).Rows(0)("Direccion")
            Cl.Add(Cliente)
            Cr.DataDefinition.ParameterFields("Direccion").ApplyCurrentValues(Cl)


            Dim rfc As String = Eventos.ObtenerValorDB("Empresa", "Reg_fed_causantes", " Id_Empresa = " & Me.lstCliente.SelectItem & "", True)

            ' Reporte.ShowDialog()

            Dim Archivo As String = "C:\Program Files\Contable\SetupProyectoContable\Polizas\PDF\" & rfc & "\" & Me.comboAño.Text.Trim() & "\" & Me.ComboMes.Text.Trim() & "\" & Me.Tabla.Item(Calculada.Index, P).Value & ".pdf"
            If Directory.Exists("C:\Program Files\Contable\SetupProyectoContable\Polizas\PDF\" & rfc & "\" & Me.comboAño.Text.Trim() & "\" & Me.ComboMes.Text.Trim() & "") = False Then
                ' si no existe la carpeta se crea

                Directory.CreateDirectory("C:\Program Files\Contable\SetupProyectoContable\Polizas\PDF\" & rfc & "\" & Me.comboAño.Text.Trim() & "\" & Me.ComboMes.Text.Trim() & "")


            End If
            If Directory.Exists(Archivo) Then
                My.Computer.FileSystem.DeleteFile(Archivo)
            End If
            Cr.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Archivo)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CmdAbrir_Click(sender As Object, e As EventArgs) Handles CmdAbrir.Click
        Dim rfc As String = Eventos.ObtenerValorDB("Empresa", "Reg_fed_causantes", " Id_Empresa = " & Me.lstCliente.SelectItem & "", True)
        Eventos.Abrir_Capeta("C:\Program Files\Contable\SetupProyectoContable\Polizas\PDF\" & rfc & "\" & Me.comboAño.Text.Trim() & "\" & Me.ComboMes.Text.Trim() & "")
    End Sub

End Class
