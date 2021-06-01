Imports Telerik.WinControls
Public Class Control_de_Polizas
    Public ConsultaInicial As String
    Public Frm As Form
    Private BindingSource1 As Windows.Forms.BindingSource = New BindingSource
    Private Original As Windows.Forms.BindingSource = New BindingSource
    Enum OpciondeFiltrado
        SIN_FILTRO = 0
        CADENA_QUE_COMIENCE_CON = 1
        CADENA_QUE_NO_COMIENCE_CON = 2
        CADENA_QUE_CONTENGA = 3
        CADENA_QUE_NO_CONTENGA = 4
        CADENA_IGUAL = 5
    End Enum

    Private Sub TabControl1_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles Generales.DrawItem
        Dim g As Graphics = e.Graphics
        Dim _TextBrush As Brush
        Dim _TabPage As TabPage = Generales.TabPages(e.Index)
        Dim _TabBounds As Rectangle = Generales.GetTabRect(e.Index)
        If (e.State = DrawItemState.Selected) Then
            _TextBrush = New SolidBrush(Color.Blue)
            g.FillRectangle(Brushes.Cyan, e.Bounds)
        Else
            _TextBrush = New System.Drawing.SolidBrush(e.ForeColor)
            e.DrawBackground()
        End If
        Dim _TabFont As New Font("Franklin Gothic Medium", 10.0, FontStyle.Bold, GraphicsUnit.Pixel)
        Dim _StringFlags As New StringFormat()
        _StringFlags.Alignment = StringAlignment.Center
        _StringFlags.LineAlignment = StringAlignment.Center
        g.DrawString(_TabPage.Text, _TabFont, _TextBrush, _TabBounds, New StringFormat(_StringFlags))
    End Sub
    Public Id_Empresa As Integer
    Public Tipo As String
    Dim Activo As Boolean
    Public Nuevo As Boolean
    Public concepto As String
    Public id_tipo As Integer
    Dim poliza_Sistema As String = ""
    Public Event Registro(ByVal clave As String)
    Dim mes = Now.Date.Month.ToString
    Dim anio = Str(DateTime.Now.Year)
    'Mover Catalogo
    Public _x, _y As Integer
    Public Movimiento As Boolean

    Private Sub Control_de_Polizas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Activo = True
        Eventos.DiseñoTabla(Me.TablaCheque)
        Eventos.DiseñoTabla(Me.TablaCuentas)
        Eventos.DiseñoTabla(Me.TablaDetalle)
        Eventos.DiseñoTabla(Me.TablaEfectivo)
        Eventos.DiseñoTabla(Me.TablaTrasnferencias)
        Eventos.DiseñoTabla(Me.TablaPol, False)
        Cargar_Listas()
        'AsignarEventos(Me)
        Limpiar()
        Dim i As Integer
        For i = DateTime.Now.Year To DateTime.Now.Year - 10 Step -1
            If i >= 2004 Then
                Me.comboAño.Items.Add(Str(i))
            End If
        Next
        Me.comboAño.Items.Add("*")
        Me.comboAño.Text = anio
        Me.ComboMes.Items.Add("*")
        If Len(mes) < 2 Then
            mes = "0" & mes
        End If
        Me.ComboMes.Text = mes
        Ventanas(False, False, False)
        Activo = False
        Me.lstCliente_cambio_item(My.Forms.Inicio.Clt.ToString, Nothing)
        Me.TablaCuentas.Columns(0).Visible = False
        Me.TablaCuentas.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        Me.TablaCuentas.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        Me.TablaCuentas.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    End Sub
    Private Sub Filtrado_Leave(sender As Object, e As EventArgs) Handles Filtrado.Leave
        If Me.Filtrado.Text = String.Empty Then
            Me.Filtrado.Text = Me.Filtrado.Tag
        End If
    End Sub

    Private Sub Filtrado_Enter(sender As Object, e As EventArgs) Handles Filtrado.Enter
        If Me.Filtrado.Text = Me.Filtrado.Tag Then
            Me.Filtrado.Text = String.Empty
        End If
    End Sub
    Private Sub Cargar_Polizas(ByVal consulta As String, ByVal cliente As Integer)
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim posicion As Integer
        Dim Datos As String = "select  DISTINCT A.ID_anio as Año, Convert(varchar,A.ID_mes) as Mes, A.Clave as Tipo, Convert(varchar,A.Num_Pol) as Número ,B.ID_dia as Día,B.Concepto as Descripción, A.Poliza , C.Importe ,A.Aplicar_Poliza as Aplicar  from "
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
        Datos &= " INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat INNER JOIN Detalle_Polizas  ON Detalle_Polizas.ID_poliza = Polizas.ID_poliza"
        Datos &= " INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa WHERE  " & consulta & " AND  (Empresa.Id_Empresa = " & cliente & ") "
        Datos &= " GROUP  BY Polizas.ID_anio , Polizas.ID_mes , rtrim(Polizas.Num_Pol) , Tipos_Poliza_Sat.clave ) AS C "
        Datos &= "  ON C.Poliza = A.Poliza order by Poliza"


        Dim sql As String = "select  DISTINCT * from (SELECT Polizas.ID_anio, Polizas.ID_mes, Tipos_Poliza_Sat.Clave+'-'+ Tipos_Poliza_Sat.Nombre as Clave, Convert (int ,Polizas.Num_Pol) as Num_Pol,Convert (BIGINT , Polizas.ID_anio + + Polizas.ID_mes +  + Tipos_Poliza_Sat.clave + + REPLACE(STR(rtrim(Polizas.Num_Pol), 6), SPACE(1), '0'))  as Poliza,CASE WHEN Polizas.Aplicar_Poliza =1 THEN 'SI' ELSE 'NO' END AS Aplicar_Poliza
                                     FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa INNER JOIN
                                    Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat WHERE " & consulta & " and  (Empresa.Id_Empresa = " & cliente & ")) as Tabla order by Poliza"
        Dim ds As DataSet = Eventos.Obtener_DS(Datos)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.lblRegistros.Text = "Total de Polizas: " & ds.Tables(0).Rows.Count
            Me.Cursor = Cursors.AppStarting
            Try
                'Me.TablaPol.DataSource = ds.Tables(0).DefaultView


                ConsultaInicial = sql
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.TablaPol.DataSource = ds.Tables(0).DefaultView
                    Me.TablaPol.Refresh()
                    Dim dv As DataView = DirectCast(Me.TablaPol.DataSource, DataView)
                    Dim dt As DataTable = dv.ToTable
                    BindingSource1.DataSource = Nothing
                    BindingSource1.DataSource = dt.DefaultView
                    Original.DataSource = dt.DefaultView
                    Me.TablaPol.Columns(7).DefaultCellStyle.Format = "C2"


                    Me.lblRegistros.Text = "Filtro: " & Me.TablaPol.RowCount & " Registros encontrados"

                End If

            Catch ex As Exception

            End Try
            Eventos.DiseñoTabla(Me.TablaPol, True)
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow
        Else
            Limpiar()
        End If

    End Sub
    Private Sub Aplicar_Filtro(ByVal Columna As String, ByVal Filtro As String)
        Dim Ret As Integer = Filtrar_DataGridView(Columna, Filtro, BindingSource1, CType(3, OpciondeFiltrado))
        If Ret = 0 Then
            Me.Filtrado.BackColor = Color.Red
        Else
            Me.Filtrado.BackColor = Color.White
        End If
        Me.lblRegistros.Text = "Filtro: " & Ret & " Registros encontrados"

    End Sub
    Function Filtrar_DataGridView(ByVal Columna As String, ByVal texto As String, ByVal BindingSource As BindingSource,
                                  Optional ByVal Opcion_Filtro As OpciondeFiltrado = Nothing) As Integer

        If BindingSource1.DataSource Is Nothing Then
            Return 0
        End If

        Try
            Dim filtro As Object = Nothing
            Select Case Opcion_Filtro
                Case OpciondeFiltrado.CADENA_QUE_COMIENCE_CON
                    filtro = "like '" & texto.Trim & "%'"
                Case OpciondeFiltrado.CADENA_QUE_NO_COMIENCE_CON
                    filtro = "Not like '" & texto.Trim & "%'"
                Case OpciondeFiltrado.CADENA_QUE_NO_CONTENGA
                    filtro = "Not like '%" & texto.Trim & "%'"
                Case OpciondeFiltrado.CADENA_QUE_CONTENGA
                    filtro = "like '%" & texto.Trim & "%'"
                Case OpciondeFiltrado.CADENA_IGUAL
                    filtro = "='" & texto.Trim & "'"
            End Select
            If Opcion_Filtro = OpciondeFiltrado.SIN_FILTRO Then
                filtro = Nothing
            End If
            If filtro <> Nothing Then
                filtro = "[" & Columna & "]" & filtro
            End If
            BindingSource.Filter = filtro
            Me.TablaPol.DataSource = BindingSource.DataSource
            Me.TablaPol.Refresh()

            Dim dv As DataView = DirectCast(Me.TablaPol.DataSource, DataView)
            Dim dt As DataTable = dv.ToTable
            BindingSource1.DataSource = Nothing
            BindingSource1.DataSource = dt.DefaultView
            Return BindingSource.Count

        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical)
        End Try

        Return 0

    End Function
    Private Sub Filtrado_TextChanged(sender As Object, e As EventArgs) Handles Filtrado.TextChanged
        If Me.Filtrado.Text <> Me.Filtrado.Tag And Me.Filtrado.Text <> String.Empty Then
            If Me.LBLF.Text <> "Estas filtrando por: " And Me.LBLF.Text <> "" Then
                Aplicar_Filtro(Me.LBLF.Text, Me.Filtrado.Text)
            End If
        End If
    End Sub



    Private Sub TablaPol_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles TablaPol.CellClick
        Try
            Me.Filtrado.Text = Me.Filtrado.Tag
            Me.LBLF.Text = Me.TablaPol.Columns(e.ColumnIndex).HeaderText
        Catch ex As Exception

        End Try

    End Sub
    Private Sub Filtrado_KeyUp(sender As Object, e As KeyEventArgs) Handles Filtrado.KeyUp
        If e.KeyCode = Keys.Back Then
            Dim sql As String = ""
            If Me.ComboMes.Text = "*" Then
                Sql = " Polizas.id_anio = '" & Trim(Me.comboAño.Text) & "' "
            Else
                Sql = " Polizas.id_anio = '" & Trim(Me.comboAño.Text) & "' and Polizas.id_mes= '" & Trim(Me.ComboMes.Text) & "'"
            End If
            Dim texto As String = Me.Filtrado.Text
            Dim Datos As String = "select  DISTINCT A.ID_anio as Año, A.ID_mes as Mes, A.Clave as Tipo, Convert(varchar,A.Num_Pol) as Número ,B.ID_dia as Día,B.Concepto as Descripción, A.Poliza , C.Importe ,A.Aplicar_Poliza as Aplicar  from "
            Datos &= " (SELECT Polizas.ID_anio, "
            Datos &= " Polizas.ID_mes, Tipos_Poliza_Sat.Clave+'-'+ Tipos_Poliza_Sat.Nombre as Clave, "
            Datos &= " Convert(int,Polizas.Num_Pol) as Num_Pol , "
            Datos &= " Convert (BIGINT ,Polizas.ID_anio + + Polizas.ID_mes + + Tipos_Poliza_Sat.clave + + REPLACE(STR(rtrim(Polizas.Num_Pol), 6), SPACE(1), '0')) as Poliza,"
            Datos &= " CASE WHEN Polizas.Aplicar_Poliza =1 THEN 'SI' ELSE 'NO' END AS Aplicar_Poliza"
            Datos &= " FROM     Polizas "
            Datos &= " INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa "
            Datos &= " INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat"
            Datos &= " WHERE  " & sql & " and  (Empresa.Id_Empresa = " & Me.lstCliente.SelectItem & ")) as A  "
            Datos &= " LEFT JOIN ( SELECT Polizas.ID_dia,  Polizas.Fecha_captura, Polizas.Concepto, "
            Datos &= " Polizas.ID_anio + + Polizas.ID_mes + + Tipos_Poliza_Sat.clave + + REPLACE(STR(rtrim(Polizas.Num_Pol), 6),SPACE(1), '0')  as Poliza"
            Datos &= "  FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa "
            Datos &= "  INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat "
            Datos &= "  WHERE  " & sql & "  and  (Empresa.Id_Empresa = " & Me.lstCliente.SelectItem & ")  "
            Datos &= " ) AS B ON B.Poliza = A.Poliza"
            Datos &= "  LEFT JOIN (  SELECT  SUM( Detalle_Polizas.Cargo)AS Importe ,  "
            Datos &= " Polizas.ID_anio + + Polizas.ID_mes + + Tipos_Poliza_Sat.clave + + REPLACE(STR(rtrim(Polizas.Num_Pol), 6), SPACE(1), '0')  as Poliza "
            Datos &= " FROM     Polizas INNER JOIN Empresa ON    Empresa.Id_Empresa = Polizas.Id_Empresa "
            Datos &= " INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat INNER JOIN Detalle_Polizas  ON Detalle_Polizas.ID_poliza = Polizas.ID_poliza"
            Datos &= " INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa WHERE  " & sql & " AND  (Empresa.Id_Empresa = " & Me.lstCliente.SelectItem & ") "
            Datos &= " GROUP  BY Polizas.ID_anio , Polizas.ID_mes , rtrim(Polizas.Num_Pol) , Tipos_Poliza_Sat.clave ) AS C "
            Datos &= "  ON C.Poliza = A.Poliza order by Poliza"
            Dim ds As DataSet = Eventos.Obtener_DS(Datos)
            If ds.Tables(0).Rows.Count > 0 Then
                Me.lblRegistros.Text = "Total de Polizas: " & ds.Tables(0).Rows.Count
                Me.Cursor = Cursors.AppStarting
                Try
                    'Me.TablaPol.DataSource = ds.Tables(0).DefaultView

                    ConsultaInicial = sql
                    If ds.Tables(0).Rows.Count > 0 Then
                        Me.TablaPol.DataSource = ds.Tables(0).DefaultView
                        Me.TablaPol.Refresh()
                        Dim dv As DataView = DirectCast(Me.TablaPol.DataSource, DataView)
                        Dim dt As DataTable = dv.ToTable
                        BindingSource1.DataSource = Nothing
                        BindingSource1.DataSource = dt.DefaultView
                        Original.DataSource = dt.DefaultView
                        Me.TablaPol.Columns(7).DefaultCellStyle.Format = "C2"


                        Me.lblRegistros.Text = "Filtro: " & Me.TablaPol.RowCount & " Registros encontrados"

                    End If

                Catch ex As Exception

                End Try
            End If

            Me.Filtrado.Text = texto
            If Me.Filtrado.Text <> Me.Filtrado.Tag And Me.Filtrado.Text <> String.Empty Then
                If Me.LBLF.Text <> "Estas filtrando por: " And Me.LBLF.Text <> "" Then
                    Aplicar_Filtro(Me.LBLF.Text, Me.Filtrado.Text)
                End If
            End If
        End If
    End Sub
    Public Class Datos
        Public Property Dia As String
        Public Property concepto As String
        Public Property Poliza As String
    End Class

    Private Sub Limpiar()
        Me.txtfechaPol.Text = ""
        Me.txtconcepto.Text = ""
        If Me.TablaDetalle.Rows.Count > 0 Then
            Me.TablaDetalle.Rows.Clear()
            Me.TablaDetalle.Rows.Add()
        Else
            Me.TablaDetalle.Rows.Add()
        End If
        If Me.TablaCuentas.Rows.Count > 0 Then

        End If
        Me.lblidpoliza.Text = ""
        Me.lblRegistros.Text = ""
        poliza_Sistema = ""
        Me.LstPolClientes.SelectText = ""
        Me.TxtNumpol.Text = ""
        Me.Contaelectronica.Enabled = False
        If Me.TablaCheque.Rows.Count > 0 Then
            Me.TablaCheque.Rows.Clear()
            Me.TablaCheque.Rows.Add()
        Else
            Me.TablaCheque.Rows.Add()
        End If
        If Me.TablaTrasnferencias.Rows.Count > 0 Then
            Me.TablaTrasnferencias.Rows.Clear()
            Me.TablaTrasnferencias.Rows.Add()
        Else
            Me.TablaTrasnferencias.Rows.Add()
        End If
        If Me.TablaEfectivo.Rows.Count > 0 Then
            Me.TablaEfectivo.Rows.Clear()
            Me.TablaEfectivo.Rows.Add()
        Else
            Me.TablaEfectivo.Rows.Add()
        End If
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
    Private Sub Calcula_total()
        Dim tcargo As Decimal = 0
        Dim tabono As Decimal = 0
        Dim tparcial As Decimal = 0
        For i As Integer = 0 To Me.TablaDetalle.RowCount - 1
            If Me.TablaDetalle.Item(Car.Index, i).Value = Nothing Then
                Me.TablaDetalle.Item(Car.Index, i).Value = 0
            End If
            If Me.TablaDetalle.Item(Abon.Index, i).Value = Nothing Then
                Me.TablaDetalle.Item(Abon.Index, i).Value = 0
            End If
            tcargo = tcargo + Me.TablaDetalle.Item(Car.Index, i).Value
            tabono = tabono + Me.TablaDetalle.Item(Abon.Index, i).Value

        Next
        Me.txtcargo.Text = Format(tcargo, "$#,###.#0")
        Me.txtabono.Text = Format(tabono, "$#,###.#0")
        If Me.txtcargo.Text <> Me.txtabono.Text Then
            Me.Label3.Text = "La Póliza no cuadra"
            Me.txtcargo.BackColor = Color.Red
            Me.txtabono.BackColor = Color.Red
            Me.Label3.ForeColor = Color.Crimson
            Me.Cmdguardar.Enabled = False
            Me.TxtTotal.Text = 0
            Me.Contaelectronica.Enabled = False
        Else
            Me.Label3.Text = ""
            Me.txtcargo.BackColor = Color.White
            Me.txtabono.BackColor = Color.White
            Me.Cmdguardar.Enabled = True
            Me.TxtTotal.Text = Me.txtabono.Text
            Me.Contaelectronica.Enabled = True
        End If


    End Sub
    Private Sub Ventanas(ByVal nuevo As Boolean, ByVal edit As Boolean, ByVal load As Boolean)

        If nuevo = True Then
            Me.CmdEliminar.Enabled = False
            Me.Cmdguardar.Enabled = True
            Me.CmdNuevo.Enabled = False
        ElseIf edit = True Then
            Me.CmdEliminar.Enabled = True
            Me.Cmdguardar.Enabled = True
            Me.CmdNuevo.Enabled = True
        ElseIf load = True Then
            Me.CmdEliminar.Enabled = False
            Me.Cmdguardar.Enabled = False
            Me.CmdNuevo.Enabled = True
        Else
            Me.CmdEliminar.Enabled = False
            Me.Cmdguardar.Enabled = False
            Me.CmdNuevo.Enabled = False
        End If


    End Sub
    Private Sub Eliminar(ByVal id_poliza As String, ByVal Tabla As String, ByVal Detalle As String, ByVal id As String, ByVal Contab As String)
        Dim Sql As String = ""

        Sql = "UPDATE dbo.Facturas SET " & id & " = NULL WHERE " & id & " = '" & id_poliza & "'"
        If Eventos.Comando_sql(Sql) >= 0 Then
        End If

        Sql = "DELETE FROM " & Contab & "  WHERE " & id & " = '" & id_poliza & "'"
        If Eventos.Comando_sql(Sql) >= 0 Then
        End If
        Try
            Sql = "UPDATE dbo.Xml_Complemento SET " & id & " = NULL WHERE " & id & " = '" & id_poliza & "'"
            If Eventos.Comando_sql(Sql) >= 0 Then
            End If
        Catch ex As Exception

        End Try
        Try
            Sql = "UPDATE dbo.Xml_SAT SET " & id & " = NULL WHERE " & id & " = '" & id_poliza & "'"
            If Eventos.Comando_sql(Sql) >= 0 Then
            End If
        Catch ex As Exception

        End Try
        'Se elimina la contabilidad electronica

        Sql = " DELETE From " & Detalle & " WHERE " & id & " = '" & id_poliza & "' "
        If Eventos.Comando_sql(Sql) > 0 Then
        End If

        Sql = "DELETE From " & Tabla & "  WHERE " & id & " = '" & id_poliza & "'"
        If Eventos.Comando_sql(Sql) > 0 Then
            'Eliminar las polizas
            Eventos.Insertar_usuariol("EliminaPolizas", Sql)
        End If




    End Sub
    Private Function Poliza_contabilizada(ByVal pol As String)
        Dim hacer As Boolean
        Dim sql As String = "Select Poliza_Contabilizada From Polizas where id_poliza =" & pol & " "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0) = True Then
                hacer = False
            Else
                hacer = True
            End If
        Else
            hacer = True
        End If
        Return hacer
    End Function
    Private Sub Creapoliza(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String, ByVal dia As String, ByVal numpol As Integer,
                         ByVal consecutivo As Integer, ByVal tipo As Integer, ByVal fecha As String,
                         ByVal concepto As String, ByVal movimiento As String)
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Polizas"
        sql &= "("
        sql &= " 	ID_poliza,      "
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
        sql &= " 	" & tipo & "," '@id_tipo_poliza, 
        sql &= " 	" & Eventos.Sql_hoy(fecha) & "," '@fecha,             
        sql &= " 	'" & concepto & "'," '@concepto,          
        sql &= " 	" & Me.lstCliente.SelectItem & "," '@Id_Empresa,        
        sql &= " 	'" & movimiento & "'," '@no_mov,            
        sql &= " 	" & Eventos.Sql_hoy() & "," '@fecha_captura,     
        sql &= " 	'A'," '@movto,             
        sql &= "  '" & Eventos.Usuario(My.Forms.Inicio.LblUsuario.Text) & "'," & Eventos.Bool2(Me.ChkAplicar.Checked) & "" '@usuario            
        sql &= " 	) "

        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
    Private Sub Crea_detalle_poliza(ByVal id_poliza As String, ByVal item As Integer, ByVal cargo As Decimal,
                                    ByVal Abono As Decimal, ByVal cuenta As String, ByVal cheque As String)
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Detalle_Polizas"
        sql &= "(   "
        sql &= " ID_poliza,      "
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

        End If
    End Sub

    Private Function CreaP(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String, ByVal dia As String, ByVal numpol As Integer,
                         ByVal consecutivo As Integer, ByVal tipo As Integer, ByVal fecha As String,
                         ByVal concepto As String, ByVal movimiento As String)
        Dim hacer As Boolean
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Polizas"
        sql &= "("
        sql &= " 	ID_poliza,"
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
        sql &= " 	" & Eventos.Sql_hoy() & "," '@fecha_captura,     
        sql &= " 	'A'," '@movto,             
        sql &= "  '" & Eventos.Usuario(My.Forms.Inicio.LblUsuario.Text) & "'," & Eventos.Bool2(Me.ChkAplicar.Checked) & "" '@usuario            
        sql &= " 	) "

        If Eventos.Comando_sql(sql) > 0 Then
            hacer = True
        Else
            hacer = False
        End If
        Return hacer
    End Function
    Private Sub Crea_detalle_P(ByVal id_poliza As String, ByVal item As Integer, ByVal cargo As Decimal,
                                    ByVal Abono As Decimal, ByVal cuenta As String, ByVal cheque As String)
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Detalle_Polizas"
        sql &= "(   "
        sql &= " ID_poliza,      "
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

        End If
    End Sub
    Private Sub Editar(ByVal Id_poliza As String, ByVal Tabla As String, ByVal Detalle As String, ByVal id As String)
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.lblidpoliza.Text = "Multipoliza" Then
            If Eventos.Comando_sql("Delete From " & Detalle & " where " & id & "='" & Id_poliza & "'") > 1 Then
                If Me.TablaDetalle.Rows.Count > 0 Then
                    Dim item As Integer = 1
                    For i As Integer = 0 To Me.TablaDetalle.Rows.Count - 1
                        If Id_poliza = Trim(Me.TablaDetalle.Item(Sel.Index, i).Value) Then
                            Crea_detalle_poliza(Id_poliza, item, Me.TablaDetalle.Item(Car.Index, i).Value,
                                           Me.TablaDetalle.Item(Abon.Index, i).Value, Me.TablaDetalle.Item(cta.Index, i).Value.ToString().Replace("-", ""), Me.TablaDetalle.Item(No_Cheque.Index, i).Value)
                            item = item + 1
                        End If
                    Next
                End If
            Else
                RadMessageBox.Show("No pudo actualizarce el Detalle de la poliza " & Id_poliza & "", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            End If
        Else

            Dim sql As String = "UPDATE dbo." & Tabla & " "
            sql &= " SET  "
            sql &= " 	Fecha = " & Eventos.Sql_hoy(Me.txtfechaPol.Text) & ","
            sql &= " 	Concepto = '" & Me.txtconcepto.Text & "',"
            sql &= " 	Num_Pol = '" & Me.TxtNumpol.Text.Trim() & "',"
            sql &= " 	Id_Empresa = " & Me.lstCliente.SelectItem & ","
            sql &= " 	No_Mov = '" & Me.TxtBanco.Text & "',"
            sql &= " 	Fecha_captura =" & Eventos.Sql_hoy(Me.txtfechaPol.Text) & ","
            sql &= " 	Movto = 'M',"
            sql &= " 	Usuario = '" & Eventos.Usuario(My.Forms.Inicio.LblUsuario.Text) & "' , Aplicar_Poliza = " & Eventos.Bool2(Me.ChkAplicar.Checked) & ""
            sql &= " 	  where " & id & " = '" & Id_poliza & "' "

            If Eventos.Comando_sql(sql) > 0 Then
                Eventos.Insertar_usuariol("Polizas_U", sql)
                If Eventos.Comando_sql("Delete From " & Detalle & " where " & id & "='" & Id_poliza & "'") > 1 Then
                    If Me.TablaDetalle.Rows.Count > 0 Then
                        Dim item As Integer = 1
                        For i As Integer = 0 To Me.TablaDetalle.Rows.Count - 1
                            If Id_poliza = Trim(Me.TablaDetalle.Item(Sel.Index, i).Value) Then
                                Crea_detalle_poliza(Id_poliza, item, Me.TablaDetalle.Item(Car.Index, i).Value,
                                               Me.TablaDetalle.Item(Abon.Index, i).Value, Me.TablaDetalle.Item(cta.Index, i).Value.ToString().Replace("-", ""), Me.TablaDetalle.Item(No_Cheque.Index, i).Value)
                                item = item + 1
                            End If
                        Next
                    End If
                Else
                    RadMessageBox.Show("No pudo actualizarce el Detalle de la poliza " & Id_poliza & "", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                End If
            Else
                RadMessageBox.Show("No se pudo guardar el registro, verifique la información ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                Exit Sub
            End If
        End If
    End Sub
    Private Function Calcula_poliza()
        Dim poliza As String = Eventos.Num_polizaS(Me.lstCliente.SelectItem, Me.LstPolClientes.SelectItem, Me.txtfechaPol.Text.ToString.Substring(6, 4), Me.txtfechaPol.Text.ToString.Substring(3, 2), Me.LstPolClientes.SelectItem)
        Return poliza
    End Function
    Private Function Calcula_PolizaS()
        Dim poliza As String = Eventos.Num_poliza(Me.lstCliente.SelectItem, Me.LstPolClientes.SelectItem, Me.txtfechaPol.Text.ToString.Substring(6, 4), Me.txtfechaPol.Text.ToString.Substring(3, 2), Me.LstPolClientes.SelectItem)
        Return poliza
    End Function
    Private Sub Cargar_datos(ByVal Id_Poliza As String, ByVal Tabla As String, ByVal ID As String, ByVal Origen As String)
        Dim Poliza As String = ""
        Dim contador As Integer = 1
        Dim consulta As String = "SELECT * FROM (
                                        SELECT " & Tabla & "." & ID & " ," & Tabla & ".Cuenta,Catalogo_de_Cuentas.Descripcion, " & Tabla & ".Cargo , " & Tabla & ".Abono," & Tabla & ".ID_item," & Tabla & ".No_cheque,
                                        " & Origen & ".ID_anio + + " & Origen & ".ID_mes +  + Tipos_Poliza_Sat.clave + + REPLACE(STR(rtrim(" & Origen & ".Num_Pol), 6), SPACE(1), '0')   as Poliza," & Origen & ".Aplicar_Poliza 
                                        FROM     " & Origen & " INNER JOIN Empresa ON    Empresa.Id_Empresa = " & Origen & ".Id_Empresa 
                                        INNER JOIN Tipos_Poliza_Sat ON " & Origen & ".Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                        INNER JOIN " & Tabla & "  ON " & Tabla & "." & ID & " = " & Origen & "." & ID & "
                                        INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = " & Tabla & ".cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                        WHERE    (Empresa.Id_Empresa = " & Me.lstCliente.SelectItem & ") ) AS tabla 
                                        WHERE Poliza ='" & Id_Poliza & "' ORDER BY  " & ID & ", id_item"


        Dim ds As DataSet = Obtener_DS(consulta)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaDetalle.RowCount = ds.Tables(0).Rows.Count
            'Insertar datos en tabla detalle
            Me.LblPolizasGuardar.Text = "Poliza:"
            For j As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Me.TablaDetalle.Item(Sel.Index, j).Value = Trim(ds.Tables(0).Rows(j)(0))
                Me.TablaDetalle.Item(cta.Index, j).Value = Trim(ds.Tables(0).Rows(j)(1).ToString().Substring(0, 4)) & "-" & Trim(ds.Tables(0).Rows(j)(1).ToString().Substring(4, 4)) & "-" & Trim(ds.Tables(0).Rows(j)(1)).ToString().Substring(8, 4) & "-" & Trim(ds.Tables(0).Rows(j)(1).ToString().Substring(12, 4))
                Me.TablaDetalle.Item(Nam.Index, j).Value = Trim(ds.Tables(0).Rows(j)(2))
                Me.TablaDetalle.Item(Car.Index, j).Value = Trim(ds.Tables(0).Rows(j)(3))
                Me.TablaDetalle.Item(Abon.Index, j).Value = Trim(ds.Tables(0).Rows(j)(4))
                Me.TablaDetalle.Item(Item.Index, j).Value = Trim(ds.Tables(0).Rows(j)(5))
                Me.ChkAplicar.Checked = Trim(ds.Tables(0).Rows(j)("Aplicar_Poliza"))
                Me.TablaDetalle.Item(No_Cheque.Index, j).Value = Trim(IIf(IsDBNull(ds.Tables(0).Rows(j)(6)) = True, "", ds.Tables(0).Rows(j)(6)))
                Me.TablaDetalle.Item(Madre.Index, j).Value = Buscar_Madre(Trim(Me.TablaDetalle.Item(cta.Index, j).Value.ToString().Replace("-", "")))
                If j = 0 Then
                    Poliza = Trim(ds.Tables(0).Rows(j)(0))
                Else
                    If Poliza <> Trim(ds.Tables(0).Rows(j)(0)) Then
                        Poliza = Trim(ds.Tables(0).Rows(j)(0))
                        contador = contador + 1
                    End If
                End If
            Next

            If contador = 1 Then
                Me.lblidpoliza.Text = Trim(Me.TablaDetalle.Item(Sel.Index, 0).Value)
            ElseIf contador > 1 Then
                Me.lblidpoliza.Text = "Multipoliza"
            End If
        Else
            Activo = True
            Limpiar()
            Activo = False
            Exit Sub
        End If
    End Sub
    Private Sub Calulaimporte(ByVal tabla As String, ByVal id As String, ByVal origen As String)
        For i As Integer = 0 To Me.TablaPol.Rows.Count - 1

            Dim consulta As String = "SELECT * FROM (  SELECT  SUM( " & tabla & ".Cargo)AS Importe ,  
                                        " & origen & ".ID_anio + + " & origen & ".ID_mes + + Tipos_Poliza_Sat.clave + + REPLACE(STR(rtrim(" & origen & ".Num_Pol), 6), SPACE(1), '0')  as Poliza FROM     " & origen & " INNER JOIN Empresa ON    Empresa.Id_Empresa = " & origen & ".Id_Empresa 
                                        INNER JOIN Tipos_Poliza_Sat ON " & origen & ".Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat INNER JOIN " & tabla & "  ON " & tabla & "." & id & " = " & origen & "." & id & "
                                        INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = " & tabla & ".cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa WHERE    (Empresa.Id_Empresa = " & Me.lstCliente.SelectItem & ")  
                                        GROUP  BY " & origen & ".ID_anio , " & origen & ".ID_mes , rtrim(" & origen & ".Num_Pol) , Tipos_Poliza_Sat.clave ) AS tabla 
                                        WHERE Poliza ='" & Me.TablaPol.Item(7, i).Value & "'  "
            Dim ds As DataSet = Eventos.Obtener_DS(consulta)
            If ds.Tables(0).Rows.Count > 0 Then
                Me.TablaPol.Item(6, i).Value = ds.Tables(0).Rows(0)(0)
            Else
                Me.TablaPol.Item(6, i).Value = 0
            End If

        Next
    End Sub
    Private Sub TablaDetalle_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles TablaDetalle.RowsRemoved
        Calcula_total()
    End Sub
    Private Sub TablaDetalle_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles TablaDetalle.CellValueChanged
        If Activo = False Then
            Dim col As Integer = 0
            If Me.TablaDetalle.Rows.Count > 0 Then
                If Me.TablaDetalle.Item(Car.Index, Me.TablaDetalle.CurrentRow.Index).Value > 0 Then
                    Me.TablaDetalle.Item(Car.Index, Me.TablaDetalle.CurrentRow.Index).Value = Me.TablaDetalle.Item(Car.Index, Me.TablaDetalle.CurrentRow.Index).Value / 1
                End If
                If Me.TablaDetalle.Item(Abon.Index, Me.TablaDetalle.CurrentRow.Index).Value > 0 Then
                    Me.TablaDetalle.Item(Abon.Index, Me.TablaDetalle.CurrentRow.Index).Value = Me.TablaDetalle.Item(Abon.Index, Me.TablaDetalle.CurrentRow.Index).Value / 1
                End If
                Calcula_total()
            End If
        End If
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
    Private Sub TablaCuentas_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles TablaCuentas.CellMouseDoubleClick
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim fila_detalle As Integer = Me.TablaDetalle.CurrentRow.Index
        Dim fila_cuenta As Integer = Me.TablaCuentas.CurrentRow.Index
        If Es_Hija(Trim(Me.TablaCuentas.Item(1, fila_cuenta).Value.ToString.Replace("-", ""))) Then
            Me.TablaDetalle.Item(cta.Index, fila_detalle).Value = Me.TablaCuentas.Item(1, fila_cuenta).Value
            ' Me.TablaDetalle.Item(cta.Index, fila_detalle).Value = Me.TablaCuentas.Item(1, fila_cuenta).Value.ToString.Replace("-", "")
        Else
            RadMessageBox.Show("La cuenta " & Trim(Me.TablaCuentas.Item(2, fila_cuenta).Value) & " no puede ser asignada", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If

        Me.TablaDetalle.Item(Nam.Index, fila_detalle).Value = Me.TablaCuentas.Item(2, fila_cuenta).Value
        If poliza_Sistema <> "" Then
            Me.TablaDetalle.Item(Sel.Index, fila_detalle).Value = poliza_Sistema

        End If
        Me.TablaDetalle.Item(Madre.Index, fila_detalle).Value = Buscar_Madre(Me.TablaDetalle.Item(cta.Index, fila_detalle).Value.ToString.Replace("-", ""))
    End Sub
    Private Sub TablaDetalle_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TablaDetalle.KeyPress
        If (Me.TablaDetalle.Focused) Then
            If e.KeyChar = ChrW(Keys.Enter) Then
                If Me.TablaDetalle.CurrentRow.Index = 0 Then
                    Me.TablaDetalle.Rows.Insert(Me.TablaDetalle.CurrentRow.Index + 1, 1)
                Else
                    Me.TablaDetalle.Rows.Add()
                End If

                Try
                    Me.TablaDetalle.Item(Sel.Index, Me.TablaDetalle.CurrentRow.Index + 1).Value = Me.TablaDetalle.Item(Sel.Index, Me.TablaDetalle.CurrentRow.Index).Value
                Catch ex As Exception

                End Try


            End If

        End If
    End Sub


    Private Sub TablaCuentas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TablaCuentas.Click
        If TablaCuentas.RowCount > 0 Then
            RaiseEvent Registro(TablaCuentas.Item(0, TablaCuentas.CurrentCell.RowIndex).Value.ToString)
            Me.LblFiltro.Text = Me.TablaCuentas.Columns(Me.TablaCuentas.CurrentCell.ColumnIndex).HeaderText
        End If
    End Sub
    Private Sub cmdActualizar_Click(sender As Object, e As EventArgs) Handles cmdActualizar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.lstCliente.SelectText <> "" Then
            Dim sql As String = "SELECT  	Id_cat_Cuentas, convert(VARCHAR,	Nivel1 + '-' + Nivel2 + '-'+ Nivel3 + '-' + Nivel4 ,103) AS Cuenta,	Descripcion FROM dbo.Catalogo_de_Cuentas 
                inner join Empresa on Empresa.Id_Empresa =  catalogo_de_cuentas.Id_Empresa  
                where Catalogo_de_Cuentas.Id_Empresa = " & Me.lstCliente.SelectItem & "  Order by cuenta"
            Eventos.LlenarDataGrid_DS(Eventos.Obtener_DS(sql), Me.TablaCuentas)
            Me.TablaCuentas.Columns(0).Visible = False

        Else
            RadMessageBox.Show("Debes seleccionar una Empresa .", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
            Exit Sub
        End If
    End Sub
    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub
    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        If Me.TablaPol.Columns.Count > 0 Then
            Me.TablaPol.Columns.Clear()
            Limpiar()
        End If
    End Sub
    Private Sub lstCliente_cambio_item(value As String, texto As String) Handles lstCliente.Cambio_item
        If Activo = False Then
            If Me.lstCliente.SelectText <> "" Then
                'convert(VARCHAR,	Cuenta,103) AS Cuenta
                Dim sql As String = "SELECT  	Id_cat_Cuentas, nivel1 + '-'+ nivel2 + '-'+ nivel3 + '-'+ nivel4  as Cuenta,	Descripcion FROM dbo.Catalogo_de_Cuentas 
                inner join Empresa on Empresa.Id_Empresa =  catalogo_de_cuentas.Id_Empresa  
                where Catalogo_de_Cuentas.Id_Empresa = " & Me.lstCliente.SelectItem & "  Order by cuenta"
                Eventos.LlenarDataGrid_DS(Eventos.Obtener_DS(sql), Me.TablaCuentas)
                Me.TablaCuentas.Columns(0).Visible = False
                'For i As Integer = 0 To Me.TablaCuentas.Rows.Count - 1
                '    If Me.TablaCuentas.Item(1, i).Value <> Nothing Then
                '        Me.TablaCuentas.Item(1, i).Value = Me.TablaCuentas.Item(1, i).Value.ToString.PadRight(16, "0").Substring(0, 4) & "-" & Me.TablaCuentas.Item(1, i).Value.ToString.PadRight(16, "0").Substring(4, 4) & "-" & Me.TablaCuentas.Item(1, i).Value.ToString.PadRight(16, "0").Substring(8, 4) & "-" & Me.TablaCuentas.Item(1, i).Value.ToString.PadRight(16, "0").Substring(12, 4)
                '    End If
                'Next
                Activo = True
                Me.LstPolClientes.Cargar(" Select  Id_Tipo_Pol_Sat, clave +'-'+ Nombre as Nombre from Tipos_Poliza_Sat where Id_Empresa= " & Me.lstCliente.SelectItem & " ")
                Me.LstPolClientes.SelectText = ""
                Activo = False

            End If
        End If
    End Sub
    Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
        Dim sql As String = ""
        Limpiar()
        If Me.lstCliente.SelectText <> "" Then
            If Me.TablaDetalle.Rows.Count > 0 Then
                Me.TablaDetalle.Rows.Clear()
            End If
            If Me.TablaPol.Columns.Count > 0 Then
                Me.TablaPol.Columns.Clear()
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

            Ventanas(False, False, True)
            Activo = False

        End If
    End Sub


    Private Sub CmdNuevo_Click(sender As Object, e As EventArgs) Handles CmdNuevo.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.lstCliente.SelectText <> "" Then
            Nuevo = True
            Activo = True
            Limpiar()
            Me.Contaelectronica.Enabled = True
            If Me.ComboMes.Text.Trim() = "*" Then
                Me.LstUUIDs.Cargar("SELECT UUID,UUID FROM Xml_Sat WHERE Anio_Contable = " & Me.comboAño.Text.Trim() & " AND   Id_Empresa = " & Me.lstCliente.SelectItem & " AND (ID_poliza IS NULL OR ID_poliza = '') 
                                UNION SELECT UUID,UUID FROM Xml_Complemento  WHERE Anio_Contable = " & Me.comboAño.Text.Trim() & " AND   Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                AND (ID_poliza IS NULL OR ID_poliza = '')")
            Else
                Me.LstUUIDs.Cargar("SELECT UUID,UUID FROM Xml_Sat WHERE Anio_Contable = " & Me.comboAño.Text.Trim() & " AND Mes_Contable = " & Me.ComboMes.Text.Trim() & " AND Id_Empresa = " & Me.lstCliente.SelectItem & " AND (ID_poliza IS NULL OR ID_poliza = '') 
                                UNION SELECT UUID,UUID FROM Xml_Complemento  WHERE Anio_Contable = " & Me.comboAño.Text.Trim() & " AND Mes_Contable = " & Me.ComboMes.Text.Trim() & " AND Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                AND (ID_poliza IS NULL OR ID_poliza = '')")
            End If
            Me.LstUUIDs.SelectText = ""

            Me.LstRFC.Cargar("SELECT DISTINCT RFC_Emisor,RFC_Emisor FROM Xml_Sat WHERE   Id_Empresa = " & Me.lstCliente.SelectItem & " And Emitidas=0")
            Me.LstRFC.SelectText = ""
            Ventanas(True, False, False)
            Activo = False
        Else
            RadMessageBox.Show("Debes seleccionar una Empresa .", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
            Exit Sub
        End If
    End Sub
    Private Sub HabilitaContabilidadElectronica()

    End Sub
    Private Sub Cmdguardar_Click(sender As Object, e As EventArgs) Handles Cmdguardar.Click
        If Nuevo = True Then

            Dim poliza As String = Calcula_PolizaS()
            Dim P() As String = Split(poliza, "-")
            Dim consecutivo As Integer = P(1)
            If CreaP(poliza, Me.txtfechaPol.Text.ToString.Substring(6, 4), Me.txtfechaPol.Text.ToString.Substring(3, 2), Me.txtfechaPol.Text.ToString.Substring(0, 2), Me.TxtNumpol.Text,
                           consecutivo, Me.LstPolClientes.SelectItem, Me.txtfechaPol.Text,
                           Trim(Me.txtconcepto.Text), Me.TxtBanco.Text) = True Then
                If Me.TablaDetalle.Rows.Count > 0 Then
                    For i As Integer = 0 To Me.TablaDetalle.Rows.Count - 1
                        Crea_detalle_P(poliza, i + 1, Me.TablaDetalle.Item(Car.Index, i).Value,
                                       Me.TablaDetalle.Item(Abon.Index, i).Value, Me.TablaDetalle.Item(cta.Index, i).Value.ToString().Replace("-", ""), Me.TablaDetalle.Item(No_Cheque.Index, i).Value)
                    Next
                End If
                Contabilidad(poliza, Me.txtfechaPol.Text.ToString.Substring(6, 4), Me.txtfechaPol.Text.ToString.Substring(3, 2), Me.txtfechaPol.Text.ToString.Trim(), Me.LstRFC.SelectText)
                Me.Cmd_Procesar.PerformClick()
            End If
        Else
            ' Checar si es de una sola o son varis polizas
            Dim contador As Integer = 0
            Dim pol As String = ""
            Dim polizas(0) As String

            For i As Integer = 0 To Me.TablaDetalle.Rows.Count - 1
                If i > 0 Then
                    If pol <> Trim(Me.TablaDetalle.Item(Sel.Index, i).Value) Then
                        contador = contador + 1
                        pol = Trim(Me.TablaDetalle.Item(Sel.Index, i).Value)
                    End If
                Else
                    pol = Trim(Me.TablaDetalle.Item(Sel.Index, i).Value)
                End If
            Next
            ReDim polizas(contador)
            contador = 0
            For i As Integer = 0 To Me.TablaDetalle.Rows.Count - 1
                If i > 0 Then
                    If pol <> Trim(Me.TablaDetalle.Item(Sel.Index, i).Value) Then
                        contador = contador + 1
                        pol = Trim(Me.TablaDetalle.Item(Sel.Index, i).Value)
                        polizas(contador) = pol
                    End If
                Else
                    pol = Trim(Me.TablaDetalle.Item(Sel.Index, i).Value)
                    polizas(contador) = pol
                End If
            Next
            For Each pol In polizas
                Editar(Me.lblidpoliza.Text, "Polizas", "Detalle_Polizas", "ID_poliza")
            Next
        End If
    End Sub
    Private Sub CmdEliminar_Click(sender As Object, e As EventArgs) Handles CmdEliminar.Click
        If Me.lblidpoliza.Text <> "" Then
            Eliminar(Me.lblidpoliza.Text, "Polizas", "Detalle_Polizas", "ID_poliza", "Conta_E_Sistema")
        End If
        Me.Cmd_Procesar.PerformClick()
    End Sub

    Private Sub TablaPol_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles TablaPol.CellDoubleClick
        Dim Tabla As String = ""
        Dim Id As String = ""
        Dim origen As String = ""

        Tabla = "Detalle_Polizas"
        Id = "ID_poliza"
        origen = "Polizas"


        Cargar_datos(Trim(Me.TablaPol.Item(6, Me.TablaPol.CurrentRow.Index).Value), Tabla, Id, origen)
        For I As Integer = 0 To Me.TablaDetalle.Rows.Count - 1
            Me.TablaDetalle.Item(Car.Index, I).Value *= 1
            Me.TablaDetalle.Item(Abon.Index, I).Value *= 1
        Next
        Cargar_contabiliada_electronica(DebuelvePolizas(), IIf(origen = "Polizas", 2, 1))
        Me.LstPolClientes.SelectText = Trim(Me.TablaPol.Item(2, Me.TablaPol.CurrentRow.Index).Value)
        Me.TxtNumpol.Text = Trim(Me.TablaPol.Item(3, Me.TablaPol.CurrentRow.Index).Value)
        Me.txtconcepto.Text = Trim(Me.TablaPol.Item(5, Me.TablaPol.CurrentRow.Index).Value)
        Me.txtfechaPol.Text = Trim(Me.TablaPol.Item(4, Me.TablaPol.CurrentRow.Index).Value) & "/" & Trim(Me.TablaPol.Item(1, Me.TablaPol.CurrentRow.Index).Value) & "/" & Trim(Me.TablaPol.Item(0, Me.TablaPol.CurrentRow.Index).Value)
        Ventanas(False, True, False)
        Nuevo = False
    End Sub
    Private Function Buscar_Madre(ByVal Cta As String)
        Dim Madre As String = ""
        If Cta.Substring(12, 4) = "0000" And Cta.Substring(8, 4) > "0000" Then ' Segundo nivel
            Dim Cuenta As String = " select Descripcion from Catalogo_de_Cuentas where Nivel1 = '" & Cta.Substring(0, 4) & "' AND Nivel2 = '" & Cta.Substring(4, 4) & "' AND Nivel3 = '0000' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
            Dim ds As DataSet = Eventos.Obtener_DS(Cuenta)
            If ds.Tables(0).Rows.Count > 0 Then
                Madre = Trim(ds.Tables(0).Rows(0)(0))
            End If
        ElseIf Cta.Substring(12, 4) > "0000" And Cta.Substring(8, 4) > "0000" Then 'Tercer Nivel
            Dim Cuenta As String = " select Descripcion from Catalogo_de_Cuentas where Nivel1 = '" & Cta.Substring(0, 4) & "' AND Nivel2 = '" & Cta.Substring(4, 4) & "' AND Nivel3 = '" & Cta.Substring(8, 4) & "' AND Nivel4 = '0000' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
            Dim ds As DataSet = Eventos.Obtener_DS(Cuenta)
            If ds.Tables(0).Rows.Count > 0 Then
                Madre = Trim(ds.Tables(0).Rows(0)(0))
            End If
        ElseIf Cta.Substring(4, 4) > "0000" And Cta.Substring(8, 4) = "0000" And Cta.Substring(12, 4) = "0000" Then 'Primer Nivel
            Dim Cuenta As String = " select Descripcion from Catalogo_de_Cuentas where Nivel1 = '" & Cta.Substring(0, 4) & "' AND Nivel2 > '0000' AND Nivel3='0000' AND Nivel4 = '0000' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
            Dim ds As DataSet = Eventos.Obtener_DS(Cuenta)
            If ds.Tables(0).Rows.Count > 0 Then
                Madre = Trim(ds.Tables(0).Rows(0)(0))
            End If
        Else

        End If
        Return Madre
    End Function



    Private Sub Contabilidad(ByVal Poliza As String, ByVal Anio As String, ByVal Mes As String, ByVal Fecha As String, ByVal RFC As String)
        If Buscafactura(Me.LstUUIDs.SelectText, "C") = True Then
            'Se inserta la Factura
            Inserta_Comprobante_Fiscal(Poliza, Anio, Mes, RFC, Fecha, Me.LstUUIDs.SelectText, "Factura " & Trim(RFC) & " C", Me.TxtTotal.Text)
        Else
            'Se Edita la Factura
            Edita_Factura(Me.LstUUIDs.SelectText, "C", Poliza)
        End If
        If Me.TablaEfectivo.Item(ImpEf.Index, 0).Value > 0 Then
            ' Insertar registro contabiidad electronica efectivo
            Inserta_Comprobante_Fiscal_Efectivo(Poliza, Anio, Mes, RFC, Me.LstPolClientes.SelectText.ToString.Substring(0, 3), Fecha, "", "", "", "", Me.TablaEfectivo.Item(ImpEf.Index, 0).Value)
        End If
        If Me.TablaTrasnferencias.Item(ImpT.Index, 0).Value > 0 Then
            ' Insertar registro contabiidad electronica Transferencia
            Dim BO As String = Me.TablaTrasnferencias.Item(BancoOrigen.Index, 0).Value
            Dim Bd As String = Me.TablaTrasnferencias.Item(Bancodestino.Index, 0).Value
            Inserta_Comprobante_Fiscal_Transf(Poliza, Anio, Mes, RFC, Me.LstPolClientes.SelectText.ToString.Substring(0, 3), Fecha, "", BO, Me.TablaTrasnferencias.Item(CuentaO.Index, 0).Value, Me.LstUUIDs.SelectText, Me.TablaTrasnferencias.Item(ImpT.Index, 0).Value, Bd, Me.TablaTrasnferencias.Item(CtaBD.Index, 0).Value)
        End If
        If Me.TablaCheque.Item(ImpC.Index, 0).Value > 0 Then
            Dim BO As String = Me.TablaCheque.Item(BancosCheques.Index, 0).Value
            Inserta_Comprobante_Fiscal_Cheque(Poliza, Anio, Mes, RFC, Me.LstPolClientes.SelectText.ToString.Substring(0, 3), Fecha, Me.TablaCheque.Item(NoCheque.Index, 0).Value, BO, Me.TablaCheque.Item(CuentaC.Index, 0).Value, Me.LstUUIDs.SelectText, Me.TablaCheque.Item(ImpC.Index, 0).Value)
        End If
    End Sub
    Private Function DebuelvePolizas()

        Dim Tabla As Hashtable = New Hashtable
        Dim dato As String
        Try
            For i As Integer = 0 To Me.TablaDetalle.Rows.Count - 1
                dato = Me.TablaDetalle.Item(Sel.Index, i).Value
                If Not Tabla.ContainsKey(dato) Then : Tabla.Add(dato, 1)

                Else : Tabla(dato) = CType(Tabla(dato), Integer) + 1 : End If
            Next

        Catch ex As Exception

        End Try
        Return Tabla
    End Function
    Private Sub Cargar_contabiliada_electronica(ByVal Polizas As Hashtable, ByVal Tipo As Integer)
        Dim Sql As String = ""
        Dim ds As DataSet
        If Me.TablaEfectivo.RowCount > 1 Then
            Me.TablaEfectivo.Rows.Clear()
        End If
        If Me.TablaTrasnferencias.RowCount > 1 Then
            Me.TablaTrasnferencias.Rows.Clear()
        End If
        If Me.TablaCheque.RowCount > 1 Then
            Me.TablaCheque.Rows.Clear()
        End If
        For Each Poliza As DictionaryEntry In Polizas


            Sql = " select RFC_Ce from  Conta_E_Sistema where ID_poliza = '" & Poliza.Key.ToString() & "' "
            ds = Eventos.Obtener_DS(Sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Me.LstRFC.SelectText = ds.Tables(0).Rows(0)("RFC_Ce")
            End If
            ds.Clear()
            Sql = " select Importe from  Conta_E_Sistema where ID_poliza = '" & Poliza.Key.ToString() & "' and Tipo_CE = 'P'"
            ds = Eventos.Obtener_DS(Sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Me.TablaEfectivo.RowCount = ds.Tables(0).Rows.Count
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.TablaEfectivo.Item(ImpEf.Index, Me.TablaEfectivo.RowCount - 1).Value = ds.Tables(0).Rows(i)("Importe")
                Next
            End If
            ds.Clear()
            Sql = " select Importe,No_Banco , Cuenta_Origen, Banco_Destino, Cuenta_Destino,Fecha_Mov from  Conta_E_Sistema where ID_poliza  = '" & Poliza.Key.ToString() & "' and Tipo_CE = 'T'"
            ds = Eventos.Obtener_DS(Sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Me.TablaTrasnferencias.RowCount = ds.Tables(0).Rows.Count
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.TablaTrasnferencias.Item(ImpT.Index, Me.TablaTrasnferencias.RowCount - 1).Value = ds.Tables(0).Rows(i)("Importe")
                    Me.TablaTrasnferencias.Item(BancoOrigen.Index, Me.TablaTrasnferencias.RowCount - 1).Value = ds.Tables(0).Rows(i)("No_Banco")
                    Me.TablaTrasnferencias.Item(CuentaO.Index, Me.TablaTrasnferencias.RowCount - 1).Value = ds.Tables(0).Rows(i)("Cuenta_Origen")
                    Me.TablaTrasnferencias.Item(Bancodestino.Index, Me.TablaTrasnferencias.RowCount - 1).Value = ds.Tables(0).Rows(i)("Banco_Destino")
                    Me.TablaTrasnferencias.Item(CtaBD.Index, Me.TablaTrasnferencias.RowCount - 1).Value = ds.Tables(0).Rows(i)("Cuenta_Destino")
                    Me.TablaTrasnferencias.Item(Fechat.Index, Me.TablaTrasnferencias.RowCount - 1).Value = ds.Tables(0).Rows(i)("Fecha_Mov")

                Next
            End If
            ds.Clear()
            Sql = " select Importe,No_Banco , Cuenta_Origen, No_Cheque, Cuenta_Destino,Fecha_Mov from  Conta_E_Sistema where ID_poliza = '" & Poliza.Key.ToString() & "' and Tipo_CE = 'H'"
            ds = Eventos.Obtener_DS(Sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Me.TablaCheque.RowCount = ds.Tables(0).Rows.Count
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.TablaCheque.Item(ImpC.Index, Me.TablaCheque.RowCount - 1).Value = ds.Tables(0).Rows(i)("Importe")
                    Me.TablaCheque.Item(BancosCheques.Index, Me.TablaCheque.RowCount - 1).Value = ds.Tables(0).Rows(i)("No_Banco")
                    Me.TablaCheque.Item(CuentaC.Index, Me.TablaCheque.RowCount - 1).Value = ds.Tables(0).Rows(i)("Cuenta_Origen")
                    Me.TablaCheque.Item(NoCheque.Index, Me.TablaCheque.RowCount - 1).Value = ds.Tables(0).Rows(i)("No_Cheque")
                    Me.TablaCheque.Item(FechaC.Index, Me.TablaCheque.RowCount - 1).Value = ds.Tables(0).Rows(i)("Fecha_Mov")
                Next
            End If



        Next
    End Sub
    Private Function Buscafactura(ByVal Folio_Fiscal As String, ByVal detaclle As String)
        Dim hacer As Boolean
        Dim sql As String = "select * from Facturas where Folio_Fiscal = '" & Folio_Fiscal & "' and Detalle_Comp_Electronico ='" & detaclle & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hacer = False
        Else
            hacer = True
        End If
        Return hacer
    End Function
    Private Sub Inserta_Comprobante_Fiscal(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
                          ByVal Rfc_Emisor As String, ByVal fecha As String,
                          ByVal Folio_Fiscal As String, ByVal Referencia As String, ByVal Importe As Decimal)
        Dim sql As String = "INSERT INTO dbo.Facturas"
        sql &= " 	(                   "
        sql &= " 	ID_anio,                    "
        sql &= " 	ID_mes,                     "
        sql &= " 	ID_poliza,                  "
        sql &= " 	RFC_Emisor,                 "
        sql &= " 	Folio_Fiscal,               "
        sql &= " 	Referencia,                 "
        sql &= " 	Importe,                "
        sql &= " 	Fecha_Comprobante,          "
        sql &= " 	Detalle_Comp_Electronico,Id_Empresa"
        sql &= "    )                         "
        sql &= " VALUES "
        sql &= "(                             "
        sql &= " '" & anio & "',	" '@id_anio,                   
        sql &= " '" & mes & "'," '@id_mes,                    
        sql &= " '" & id_poliza & "'," '@id_poliza,                 
        sql &= " '" & Rfc_Emisor & "'," '@rfc_emisor,                
        sql &= " '" & Folio_Fiscal & "'," '@folio_fiscal,              
        sql &= " '" & Referencia & "'," '@referencia,                
        sql &= " " & Importe & "	," '@importe,                   
        sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_comprobante,         
        sql &= " 'C'," & Me.lstCliente.SelectItem & "" '@detalle_comp_electronico   
        sql &= " )"
        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
    Private Sub Edita_Factura(ByVal Folio_Fiscal As String, ByVal detaclle As String, ByVal Poliza As String)
        Dim sql As String = " UPDATE dbo.Facturas
                        SET ID_poliza = '" & Poliza & "'
                        WHERE Folio_Fiscal = '" & Folio_Fiscal & "' and Detalle_Comp_Electronico ='" & detaclle & "' "
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("EditaFacturas", sql)

        End If
    End Sub
    Private Sub Inserta_Comprobante_Fiscal_Efectivo(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
                           ByVal Rfc_Emisor As String, ByVal tipo As String, ByVal fecha As String,
                           ByVal No_cheque As String, ByVal no_banco As String, ByVal cuenta_origen As String, ByVal Referencia As String, ByVal Importe As Decimal)
        Dim sql As String = "  INSERT INTO dbo.Conta_E_Sistema
	(     anio,    mes,    tipo,       RFC_Ce,
    No_Cheque,    No_Banco,    Cuenta_Origen,    Fecha_Mov,    Importe,
    ID_poliza,    Tipo_CE	) VALUES	("

        sql &= " '" & anio & "',	" '@id_anio,                   
        sql &= " '" & mes & "'," '@id_mes,     
        sql &= " '" & tipo & "'," '@tipo    

        sql &= " '" & Rfc_Emisor & "'," '@rfc_ce,                
        sql &= " '" & No_cheque & "'," '@no_cheque,  
        sql &= " '" & no_banco & "'," '@no_banco,  
        sql &= " '" & cuenta_origen & "'," '@cuenta_origen,  
        sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_mov,    
        sql &= " " & Importe & "	," '@importe,                    
        sql &= " '" & id_poliza & "', " '@id_poliza,  
        sql &= " 'P' " '@tipo_ce, 
        sql &= " )"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InsertarCeE", sql)
        End If
    End Sub
    Private Sub Inserta_Comprobante_Fiscal_Transf(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
                           ByVal Rfc_Emisor As String, ByVal tipo As String, ByVal fecha As String,
                           ByVal No_cheque As String, ByVal no_banco As String, ByVal cuenta_origen As String, ByVal Referencia As String, ByVal Importe As Decimal, ByVal bancoD As String, ByVal cuentaD As String)
        Dim sql As String = "  INSERT INTO dbo.Conta_E_Sistema
	(
    anio,    mes,    tipo,       RFC_Ce,
    No_Cheque,    No_Banco,    Cuenta_Origen,    Fecha_Mov,    Importe,
    ID_poliza,    Tipo_CE,Banco_Destino,Cuenta_Destino	) VALUES	("

        sql &= " '" & anio & "',	" '@id_anio,                   
        sql &= " '" & mes & "'," '@id_mes,     
        sql &= " '" & tipo & "'," '@tipo    

        sql &= " '" & Rfc_Emisor & "'," '@rfc_ce,                
        sql &= " '" & No_cheque & "'," '@no_cheque,  
        sql &= " '" & no_banco & "'," '@no_banco,  
        sql &= " '" & cuenta_origen & "'," '@cuenta_origen,  
        sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_mov,    
        sql &= " " & Importe & "	," '@importe,                    
        sql &= " '" & id_poliza & "', " '@id_poliza,  
        sql &= " 'T','" & Trim(bancoD) & "', '" & Trim(cuentaD.Replace("/", "")) & "' " '@tipo_ce, 
        sql &= " )"
        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
    Private Sub Inserta_Comprobante_Fiscal_Cheque(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
                           ByVal Rfc_Emisor As String, ByVal tipo As String, ByVal fecha As String,
                           ByVal No_cheque As String, ByVal no_banco As String, ByVal cuenta_origen As String, ByVal Referencia As String, ByVal Importe As Decimal)
        Dim sql As String = "  INSERT INTO dbo.Conta_E_Sistema
	(
    anio,    mes,    tipo,      RFC_Ce,
    No_Cheque,    No_Banco,    Cuenta_Origen,    Fecha_Mov,    Importe,
    ID_poliza,    Tipo_CE	) VALUES	("
        sql &= " '" & anio & "',	" '@id_anio,                   
        sql &= " '" & mes & "'," '@id_mes,     
        sql &= " '" & tipo & "'," '@tipo    

        sql &= " '" & Rfc_Emisor & "'," '@rfc_ce,                
        sql &= " '" & No_cheque & "'," '@no_cheque,  
        sql &= " '" & no_banco & "'," '@no_banco,  
        sql &= " '" & cuenta_origen & "'," '@cuenta_origen,  
        sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_mov,    
        sql &= " " & Importe & "	," '@importe,                    
        sql &= " '" & id_poliza & "', " '@id_poliza,  
        sql &= " 'H' " '@tipo_ce, 
        sql &= " )"
        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
    Private Sub Liberar_Proceso(ByVal i As Integer)

        Dim contador As Integer = 0

        'Calcula la diferencia en el registro


        Me.TxtDiferencia.Text = Math.Round(Calcula_diferencia(Convert.ToDecimal(Me.TxtTotal.Text),
                                                         Me.TablaEfectivo.Item(ImpEf.Index, i).Value, Me.TablaCheque.Item(ImpC.Index, i).Value, Me.TablaTrasnferencias.Item(ImpT.Index, i).Value), 2)
        If Convert.ToDecimal(Me.TxtDiferencia.Text) <> 0 Then
            Me.TxtDiferencia.BackColor = Color.Red
        Else
            Me.TxtDiferencia.BackColor = Color.Green
        End If

        If Convert.ToDecimal(Me.TxtTotal.Text) > 0 Then
            Try

                If Me.TablaTrasnferencias.Item(ImpT.Index, i).Value > 0 And Me.TablaCheque.Item(ImpC.Index, i).Value = 0 Then ' Bloqueo transferencia

                    If Me.TablaTrasnferencias.Item(BancoOrigen.Index, i).Value = Nothing Or Me.TablaTrasnferencias.Item(Bancodestino.Index, i).Value = Nothing Or Me.TablaTrasnferencias.Item(CuentaO.Index, i).Value = Nothing Or Me.TablaTrasnferencias.Item(CtaBD.Index, i).Value = Nothing Or Me.TablaTrasnferencias.Item(Fechat.Index, i).Value = Nothing Then
                        Me.TablaTrasnferencias.Item(Aplic.Index, i).Value = False
                        Me.Cmdguardar.Enabled = False
                    Else
                        Me.TablaTrasnferencias.Item(Aplic.Index, i).Value = True
                        Me.Cmdguardar.Enabled = True
                    End If


                ElseIf Me.TablaCheque.Item(ImpC.Index, i).Value > 0 And Me.TablaTrasnferencias.Item(ImpT.Index, i).Value = 0 Then ' Bloqueo cheques
                    If Me.TablaCheque.Item(BancosCheques.Index, i).Value = Nothing Or Me.TablaCheque.Item(CuentaC.Index, i).Value = Nothing Or Me.TablaCheque.Item(NoCheque.Index, i).Value = Nothing Or Me.TablaCheque.Item(FechaC.Index, i).Value = Nothing Then
                        Me.TablaCheque.Item(Aplic.Index, i).Value = False
                        Me.Cmdguardar.Enabled = False
                    Else
                        Me.TablaCheque.Item(Aplic.Index, i).Value = True
                        Me.Cmdguardar.Enabled = True
                    End If
                ElseIf Me.TablaCheque.Item(ImpC.Index, i).Value > 0 And Me.TablaTrasnferencias.Item(ImpT.Index, i).Value > 0 Then ' AMbos
                    If Me.TablaTrasnferencias.Item(BancoOrigen.Index, i).Value = Nothing Or Me.TablaTrasnferencias.Item(Bancodestino.Index, i).Value = Nothing Or Me.TablaTrasnferencias.Item(CuentaO.Index, i).Value = Nothing Or Me.TablaTrasnferencias.Item(CtaBD.Index, i).Value = Nothing Or Me.TablaTrasnferencias.Item(Fechat.Index, i).Value = Nothing Then
                        Me.TablaTrasnferencias.Item(Aplic.Index, i).Value = False

                    Else
                        Me.TablaTrasnferencias.Item(Aplic.Index, i).Value = True

                    End If

                    If Me.TablaCheque.Item(BancosCheques.Index, i).Value = Nothing Or Me.TablaCheque.Item(CuentaC.Index, i).Value = Nothing Or Me.TablaCheque.Item(NoCheque.Index, i).Value = Nothing Or Me.TablaCheque.Item(FechaC.Index, i).Value = Nothing Then
                        Me.TablaCheque.Item(Aplic.Index, i).Value = False

                    Else
                        Me.TablaCheque.Item(Aplic.Index, i).Value = True

                    End If

                    If Me.TablaCheque.Item(Aplic.Index, i).Value = True And Me.TablaTrasnferencias.Item(Aplic.Index, i).Value = True Then
                        Me.Cmdguardar.Enabled = True
                    Else
                        Me.Cmdguardar.Enabled = False
                    End If

                Else
                    Me.TablaEfectivo.Item(Aplic.Index, i).Value = True
                End If

                If Me.LstUUIDs.SelectText = "" Then
                    Me.Cmdguardar.Enabled = False
                Else
                    Me.Cmdguardar.Enabled = True
                End If
            Catch ex As Exception

            End Try
            'End If
        End If

    End Sub
    Private Function Calcula_diferencia(ByVal total As Decimal, ByVal monto_Efectivo As Decimal, ByVal monto_cheque As Decimal, ByVal monto_Transferencia As Decimal)
        Dim Diferencia As Decimal = 0
        Diferencia = total - (monto_Efectivo + monto_cheque + monto_Transferencia)
        Return Diferencia
    End Function

    Private Sub TxtDiferencia_TextChanged(sender As Object, e As EventArgs) Handles TxtDiferencia.TextChanged
        If Convert.ToDecimal(TxtTotal.Text) > 0 Then
            If Convert.ToDecimal(Me.TxtDiferencia.Text) = 0 Then
                Me.Cmdguardar.Enabled = True
            Else
                Me.Cmdguardar.Enabled = False
            End If
        End If

    End Sub

    Private Sub TablaDetalle_KeyDown(sender As Object, e As KeyEventArgs) Handles TablaDetalle.KeyDown
        If e.KeyCode = Keys.C AndAlso e.Modifiers = Keys.Shift Then
            If Me.Catalogo.Visible = True Then
                Me.Catalogo.Visible = False
            Else
                Me.Catalogo.Visible = True
            End If
        End If
    End Sub

    Private Sub TablaCheque_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles TablaCheque.CellEndEdit
        Liberar_Proceso(0)
    End Sub

    Private Sub TablaEfectivo_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles TablaEfectivo.CellEndEdit
        Liberar_Proceso(0)
    End Sub

    Private Sub TablaTrasnferencias_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles TablaTrasnferencias.CellEndEdit
        Liberar_Proceso(0)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Eventos.Abrir_form(VerificadorPolizas)
    End Sub

    Private Sub CmdPdf_Click(sender As Object, e As EventArgs) Handles CmdPdf.Click
        Eventos.Abrir_form(PolizasMasivas)
    End Sub



    Private Sub TxtFiltro_TextChanged(sender As Object, e As EventArgs) Handles TxtFiltro.TextChanged
        If Me.lstCliente.SelectText <> "" Then
            If Me.LblFiltro.Text <> "" Then
                Dim sql As String = "SELECT  	Id_cat_Cuentas, Nivel1 +'-'+ Nivel2 +'-'+ Nivel3 +'-'+ Nivel4 AS Cuenta,	Descripcion FROM dbo.Catalogo_de_Cuentas inner join Empresa on Empresa.Id_Empresa =  catalogo_de_cuentas.Id_Empresa  
             where " & Me.LblFiltro.Text & " like '%" & Me.TxtFiltro.Text & "%' and Catalogo_de_Cuentas.Id_Empresa = " & Me.lstCliente.SelectItem & " Order by cuenta "
                Eventos.LlenarDataGrid_DS(Eventos.Obtener_DS(sql), Me.TablaCuentas)
                Me.TablaCuentas.Columns(0).Visible = False

            End If
        End If
    End Sub



    Private Sub TablaDetalle_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles TablaDetalle.CellEndEdit
        Me.TablaDetalle.Item(Car.Index, e.RowIndex).Value *= 1
        Me.TablaDetalle.Item(Abon.Index, e.RowIndex).Value *= 1
    End Sub

End Class
