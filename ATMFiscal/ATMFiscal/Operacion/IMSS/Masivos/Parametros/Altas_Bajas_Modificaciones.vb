Imports Telerik.WinControls
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.ComponentModel
Imports System.Math
Public Class Altas_Bajas_Modificaciones
    Dim Subd As String = ""
    Private Sub CmdBuscarFact_Click(sender As Object, e As EventArgs) Handles CmdBuscarFact.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim SQL As String = "SELECT Personal_Clientes.Id_Persona_Cliente, Personal_Clientes.ID_matricula, Personal_Clientes.Nombres, Personal_Clientes.Ap_paterno, Personal_Clientes.Ap_materno, Personal_Clientes.Registro_Patronal, "
        SQL &= " Personal_Clientes.Dig_Verificador_RP, Personal_Clientes.No_IMSS, Personal_Clientes.Dig_Verif_IMSS, Personal_Clientes.Salario_Base, Personal_Clientes.Tipo_Trabajador,"
        SQL &= " Personal_Clientes.Tipo_Salario, Personal_Clientes.Sem_Jornada_Reducida, Personal_Clientes.Unidad_Medicina_Familiar, Personal_Clientes.Guia, "
        SQL &= " Personal_Clientes.CURP,   convert(datetime,Personal_Clientes.Fecha_alta ,103) AS Fecha_alta, convert(datetime,Personal_Clientes.Fecha_Baja ,103) AS Fecha_Baja  "
        SQL &= " FROM     Personal_Clientes INNER JOIN Empresa ON Personal_Clientes.Id_Empresa = Empresa.Id_Empresa where   Personal_Clientes.Id_Empresa = " & Me.lstCliente.SelectItem & " AND Fecha_Baja IS NULL "
        Dim ds As DataSet = Obtener_DS(SQL)
        If ds.Tables(0).Rows.Count > 0 Then

            If Me.TablasAltasBajasReingresos.SelectedIndex = 0 Then
                Me.TablaAltas.Columns(FechaMov.Index).DefaultCellStyle.BackColor = Color.LawnGreen
                Me.TablaAltas.RowCount = ds.Tables(0).Rows.Count
                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = ds.Tables(0).Rows.Count - 1
                Me.Cursor = Cursors.AppStarting
                For j As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim Fila As DataGridViewRow = Me.TablaAltas.Rows(j)

                    Me.TablaAltas.Item(Id.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Id_Persona_Cliente")) = True, "", ds.Tables(0).Rows(j)("Id_Persona_Cliente"))
                    Me.TablaAltas.Item(Mat.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("ID_matricula")) = True, "", ds.Tables(0).Rows(j)("ID_matricula"))
                    Me.TablaAltas.Item(Nomb.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Nombres")) = True, "", ds.Tables(0).Rows(j)("Nombres"))
                    Me.TablaAltas.Item(Appaterno.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Ap_paterno")) = True, "", ds.Tables(0).Rows(j)("Ap_paterno"))
                    Me.TablaAltas.Item(Apmaterno.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Ap_materno")) = True, "", ds.Tables(0).Rows(j)("Ap_materno"))
                    Me.TablaAltas.Item(RegistroPatronal.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Registro_Patronal")) = True, "", ds.Tables(0).Rows(j)("Registro_Patronal"))
                    Me.TablaAltas.Item(DigVerificadorRP.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Dig_Verificador_RP")) = True, "", ds.Tables(0).Rows(j)("Dig_Verificador_RP"))
                    Me.TablaAltas.Item(NoIMSS.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("No_IMSS")) = True, "", ds.Tables(0).Rows(j)("No_IMSS"))
                    Me.TablaAltas.Item(DigVerifIMSS.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Dig_Verif_IMSS")) = True, "", ds.Tables(0).Rows(j)("Dig_Verif_IMSS"))
                    Me.TablaAltas.Item(SalarioBase.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Salario_Base")) = True, "", ds.Tables(0).Rows(j)("Salario_Base"))
                    Me.TablaAltas.Item(TipoTrabajador.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Tipo_Trabajador")) = True, "", ds.Tables(0).Rows(j)("Tipo_Trabajador"))
                    Me.TablaAltas.Item(TipoSalario.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Tipo_Salario")) = True, "", ds.Tables(0).Rows(j)("Tipo_Salario"))
                    Me.TablaAltas.Item(SemJornadaReducida.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Sem_Jornada_Reducida")) = True, "", ds.Tables(0).Rows(j)("Sem_Jornada_Reducida"))
                    Me.TablaAltas.Item(UnidadMedicinaFamiliar.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Unidad_Medicina_Familiar")) = True, "", ds.Tables(0).Rows(j)("Unidad_Medicina_Familiar"))
                    Me.TablaAltas.Item(Gui.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Guia")) = True, "", ds.Tables(0).Rows(j)("Guia"))
                    Me.TablaAltas.Item(CUR.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("CURP")) = True, "", ds.Tables(0).Rows(j)("CURP"))
                    Me.TablaAltas.Item(Fechaalta.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Fecha_alta")) = True, "", ds.Tables(0).Rows(j)("Fecha_alta"))
                    Me.TablaAltas.Item(Fecha_Baja.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Fecha_Baja")) = True, "", ds.Tables(0).Rows(j)("Fecha_Baja"))

                    frm.Barra.Value = j
                Next

                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Me.Cursor = Cursors.Arrow
            ElseIf Me.TablasAltasBajasReingresos.SelectedIndex = 1 Then
                Me.TablaBajas.Columns(FechaB.Index).DefaultCellStyle.BackColor = Color.LawnGreen
                Me.TablaBajas.Columns(Clau.Index).DefaultCellStyle.BackColor = Color.LawnGreen
                Me.TablaBajas.RowCount = ds.Tables(0).Rows.Count
                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = ds.Tables(0).Rows.Count - 1
                Me.Cursor = Cursors.AppStarting
                For j As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim Fila As DataGridViewRow = Me.TablaBajas.Rows(j)

                    Me.TablaBajas.Item(IDB.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Id_Persona_Cliente")) = True, "", ds.Tables(0).Rows(j)("Id_Persona_Cliente"))
                    Me.TablaBajas.Item(Matb.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("ID_matricula")) = True, "", ds.Tables(0).Rows(j)("ID_matricula"))
                    Me.TablaBajas.Item(NombB.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Nombres")) = True, "", ds.Tables(0).Rows(j)("Nombres"))
                    Me.TablaBajas.Item(APB.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Ap_paterno")) = True, "", ds.Tables(0).Rows(j)("Ap_paterno"))
                    Me.TablaBajas.Item(AMB.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Ap_materno")) = True, "", ds.Tables(0).Rows(j)("Ap_materno"))
                    Me.TablaBajas.Item(RPB.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Registro_Patronal")) = True, "", ds.Tables(0).Rows(j)("Registro_Patronal"))
                    Me.TablaBajas.Item(DVRPB.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Dig_Verificador_RP")) = True, "", ds.Tables(0).Rows(j)("Dig_Verificador_RP"))
                    Me.TablaBajas.Item(NIMMSB.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("No_IMSS")) = True, "", ds.Tables(0).Rows(j)("No_IMSS"))
                    Me.TablaBajas.Item(DVNIMSSB.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Dig_Verif_IMSS")) = True, "", ds.Tables(0).Rows(j)("Dig_Verif_IMSS"))
                    Me.TablaBajas.Item(SBB.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Salario_Base")) = True, "", ds.Tables(0).Rows(j)("Salario_Base"))
                    Me.TablaBajas.Item(TTB.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Tipo_Trabajador")) = True, "", ds.Tables(0).Rows(j)("Tipo_Trabajador"))
                    Me.TablaBajas.Item(TSB.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Tipo_Salario")) = True, "", ds.Tables(0).Rows(j)("Tipo_Salario"))
                    Me.TablaBajas.Item(SJRB.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Sem_Jornada_Reducida")) = True, "", ds.Tables(0).Rows(j)("Sem_Jornada_Reducida"))
                    Me.TablaBajas.Item(UMFB.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Unidad_Medicina_Familiar")) = True, "", ds.Tables(0).Rows(j)("Unidad_Medicina_Familiar"))
                    Me.TablaBajas.Item(GB.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Guia")) = True, "", ds.Tables(0).Rows(j)("Guia"))
                    Me.TablaBajas.Item(CB.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("CURP")) = True, "", ds.Tables(0).Rows(j)("CURP"))
                    Me.TablaBajas.Item(Fechaalta.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Fecha_alta")) = True, "", ds.Tables(0).Rows(j)("Fecha_alta"))
                    Me.TablaBajas.Item(Fecha_Baja.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Fecha_Baja")) = True, "", ds.Tables(0).Rows(j)("Fecha_Baja"))

                    frm.Barra.Value = j
                Next

                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Me.Cursor = Cursors.Arrow

            ElseIf Me.TablasAltasBajasReingresos.SelectedIndex = 2 Then
                Me.TablaReingreso.Columns(Fechar.Index).DefaultCellStyle.BackColor = Color.LawnGreen
                Me.TablaReingreso.RowCount = ds.Tables(0).Rows.Count
                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = ds.Tables(0).Rows.Count - 1
                Me.Cursor = Cursors.AppStarting
                For j As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim Fila As DataGridViewRow = Me.TablaReingreso.Rows(j)

                    Me.TablaReingreso.Item(idr.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Id_Persona_Cliente")) = True, "", ds.Tables(0).Rows(j)("Id_Persona_Cliente"))
                    Me.TablaReingreso.Item(Matr.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("ID_matricula")) = True, "", ds.Tables(0).Rows(j)("ID_matricula"))
                    Me.TablaReingreso.Item(Nombr.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Nombres")) = True, "", ds.Tables(0).Rows(j)("Nombres"))
                    Me.TablaReingreso.Item(APR.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Ap_paterno")) = True, "", ds.Tables(0).Rows(j)("Ap_paterno"))
                    Me.TablaReingreso.Item(AMR.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Ap_materno")) = True, "", ds.Tables(0).Rows(j)("Ap_materno"))
                    Me.TablaReingreso.Item(RPR.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Registro_Patronal")) = True, "", ds.Tables(0).Rows(j)("Registro_Patronal"))
                    Me.TablaReingreso.Item(DVRPR.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Dig_Verificador_RP")) = True, "", ds.Tables(0).Rows(j)("Dig_Verificador_RP"))
                    Me.TablaReingreso.Item(NIR.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("No_IMSS")) = True, "", ds.Tables(0).Rows(j)("No_IMSS"))
                    Me.TablaReingreso.Item(DVIR.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Dig_Verif_IMSS")) = True, "", ds.Tables(0).Rows(j)("Dig_Verif_IMSS"))
                    Me.TablaReingreso.Item(SBR.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Salario_Base")) = True, "", ds.Tables(0).Rows(j)("Salario_Base"))
                    Me.TablaReingreso.Item(TTR.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Tipo_Trabajador")) = True, "", ds.Tables(0).Rows(j)("Tipo_Trabajador"))
                    Me.TablaReingreso.Item(TSR.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Tipo_Salario")) = True, "", ds.Tables(0).Rows(j)("Tipo_Salario"))
                    Me.TablaReingreso.Item(SJRR.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Sem_Jornada_Reducida")) = True, "", ds.Tables(0).Rows(j)("Sem_Jornada_Reducida"))
                    Me.TablaReingreso.Item(UMFR.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Unidad_Medicina_Familiar")) = True, "", ds.Tables(0).Rows(j)("Unidad_Medicina_Familiar"))
                    Me.TablaReingreso.Item(GR.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Guia")) = True, "", ds.Tables(0).Rows(j)("Guia"))
                    Me.TablaReingreso.Item(CR.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("CURP")) = True, "", ds.Tables(0).Rows(j)("CURP"))
                    Me.TablaReingreso.Item(FAR.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Fecha_alta")) = True, "", ds.Tables(0).Rows(j)("Fecha_alta"))
                    Me.TablaReingreso.Item(FBR.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Fecha_Baja")) = True, "", ds.Tables(0).Rows(j)("Fecha_Baja"))

                    frm.Barra.Value = j
                Next

                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Me.Cursor = Cursors.Arrow

            End If

        End If

    End Sub

    Private Sub Altas_Bajas_Modificaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_clientes()
        Eventos.DiseñoTabla(Me.tabla)
        Eventos.DiseñoTabla(Me.TablaAltas)
        Eventos.DiseñoTabla(Me.TablaBajas)
        Eventos.DiseñoTabla(Me.TablaReingreso)
    End Sub
    Private Sub Cargar_clientes()
        Me.lstCliente.Cargar("SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                                FROM     Empresa INNER JOIN
                                Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                                Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                                Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                                Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                                WHERE  (Usuarios.Usuario LIKE '%" & Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = Inicio.Clt
        Dim sql As String = "SELECT Clave FROM Sub_Delegaciones_IMSS INNER JOIN Clientes_Delegaciones ON Clientes_Delegaciones.Id_Sub_Delegacion_IMSS = Sub_Delegaciones_IMSS.Id_Sub_Delegacion_IMSS WHERE Id_Empresa = " & Me.lstCliente.SelectItem & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Subd = ds.Tables(0).Rows(0)(0)
        Else
            Subd = "00"
        End If

    End Sub

    Private Sub CmdSalirF_Click(sender As Object, e As EventArgs) Handles CmdSalirF.Click
        Me.Close()
    End Sub

    Private Sub CmdProcesar_Click(sender As Object, e As EventArgs) Handles CmdProcesar.Click
        If Me.TablasAltasBajasReingresos.SelectedIndex = 0 Then
            Genera_TXTAlta()
        ElseIf Me.TablasAltasBajasReingresos.SelectedIndex = 1 Then
            Genera_TXTBaja()
        ElseIf Me.TablasAltasBajasReingresos.SelectedIndex = 2 Then
            Genera_TXTReingreso()
        End If

    End Sub
    Private Sub Genera_TXTAlta()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim fila As String = Nothing
        Dim strStreamW As Stream = Nothing
        Dim strStreamWriter As StreamWriter = Nothing
        Dim texto As String = Nothing
        ' Donde guardamos los paths (la direccion de mi sukiri) de los archivos que vamos a estar utilizando ..
        Dim PathArchivo As String
        Dim nuevo_archivo As String = ""
        Dim Ordinal As String = ""



        Dim ID_matricula As String = ""
        Dim Ap_paterno As String = ""
        Dim Ap_materno As String = ""
        Dim Nombres As String = ""
        Dim Registro_Patronal As String = ""
        Dim Dig_Verificador_RP As String = ""
        Dim No_IMSS As String = ""
        Dim Dig_Verif_IMSS As String = ""
        Dim Salario_Base As String = ""
        Dim Tipo_Trabajador As String = ""
        Dim Tipo_Salario As String = ""
        Dim Sem_Jornada_Reducida As String = ""
        Dim Fecha_Movimiento As String = ""
        Dim Unidad_Medicina_Familiar As String = ""
        Dim Tipo_Movimiento As String = ""
        Dim Guia As String = ""
        Dim CURP As String = ""
        Dim Id_Empresa As String = ""
        Dim Id_Formato As String = ""
        Dim Fecha_alta As String = ""
        Dim Fecha_Baja As String = ""

        Dim rfcNombre As String = Eventos.ObtenerValorDB("Emoresa", "RFC", " Id_Empresa= " & Me.lstCliente.SelectItem & "", 1)

        Try
            If Directory.Exists("C:\Program Files\Contable\SetupProyectoContable\Altas\" & rfcNombre & "") = False Then ' si no existe la carpeta se crea
                Directory.CreateDirectory("C:\Program Files\Contable\SetupProyectoContable\Altas\" & rfcNombre & "")
            End If
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            PathArchivo = "C:\Program Files\Contable\SetupProyectoContable\Altas\" & rfcNombre & "\" & rfcNombre & "-Reingresos.txt" ' Se determina el nombre del archivo para la tranferencia concatenando la fecha actual
            ' asignar el valor de la cuenta

            If File.Exists(PathArchivo) Then

                File.Delete(PathArchivo)
                strStreamW = File.Create(PathArchivo)
                strStreamWriter = New StreamWriter(strStreamW, System.Text.Encoding.Default)

                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = Me.TablaAltas.Rows.Count - 1
                Me.Cursor = Cursors.AppStarting
                For i As Integer = 0 To Me.TablaAltas.Rows.Count - 1
                    If Me.TablaAltas.Item(SelA.Index, i).Value = True Then


                        ID_matricula = IIf(IsNothing(Me.TablaAltas.Item(Mat.Index, i).Value), "", Me.TablaAltas.Item(Mat.Index, i).Value).ToString.PadRight(10, " ")
                        Ap_paterno = UCase(IIf(IsNothing(Me.TablaAltas.Item(Appaterno.Index, i).Value), "", Me.TablaAltas.Item(Appaterno.Index, i).Value).ToString.PadRight(27, " "))
                        Ap_materno = UCase(IIf(IsNothing(Me.TablaAltas.Item(Apmaterno.Index, i).Value), "", Me.TablaAltas.Item(Apmaterno.Index, i).Value).ToString.PadRight(27, " "))
                        Nombres = UCase(IIf(IsNothing(Me.TablaAltas.Item(Nomb.Index, i).Value), "", Me.TablaAltas.Item(Nomb.Index, i).Value).ToString.PadRight(27, " "))
                        Registro_Patronal = IIf(IsNothing(Me.TablaAltas.Item(RegistroPatronal.Index, i).Value), "", Me.TablaAltas.Item(RegistroPatronal.Index, i).Value).ToString.PadRight(10, " ")
                        Dig_Verificador_RP = IIf(IsNothing(Me.TablaAltas.Item(DigVerificadorRP.Index, i).Value), "", Me.TablaAltas.Item(DigVerificadorRP.Index, i).Value).ToString.PadRight(1, " ")
                        No_IMSS = IIf(IsNothing(Me.TablaAltas.Item(NoIMSS.Index, i).Value), "", Me.TablaAltas.Item(NoIMSS.Index, i).Value).ToString.PadRight(10, " ")
                        Dig_Verif_IMSS = IIf(IsNothing(Me.TablaAltas.Item(DigVerifIMSS.Index, i).Value), "", Me.TablaAltas.Item(DigVerifIMSS.Index, i).Value).ToString.PadRight(1, " ")

                        Dim a As Decimal = 0
                        Dim b As Integer = 0
                        a = Math.Round(IIf(IsNothing(Me.TablaAltas.Item(SalarioBase.Index, i).Value), 0, Me.TablaAltas.Item(SalarioBase.Index, i).Value), 2, MidpointRounding.ToEven)
                        b = Math.Round(IIf(IsNothing(Me.TablaAltas.Item(SalarioBase.Index, i).Value), 0, Me.TablaAltas.Item(SalarioBase.Index, i).Value), 0, MidpointRounding.ToEven)
                        'If (a - b) = 0.00 Then
                        '    Salario_Base = b.ToString.Replace(".", "").PadLeft(6, "0")
                        'Else
                        '    Salario_Base = a.ToString.Replace(".", "").PadLeft(6, "0")
                        'End If
                        Salario_Base = a.ToString.Replace(".", "").PadLeft(6, "0")

                        Tipo_Trabajador = IIf(IsNothing(Me.TablaAltas.Item(TipoTrabajador.Index, i).Value), "", Me.TablaAltas.Item(TipoTrabajador.Index, i).Value).ToString.PadRight(1, " ")
                        Tipo_Salario = IIf(IsNothing(Me.TablaAltas.Item(TipoSalario.Index, i).Value), "", Me.TablaAltas.Item(TipoSalario.Index, i).Value).ToString.PadRight(1, " ")
                        Sem_Jornada_Reducida = IIf(IsNothing(Me.TablaAltas.Item(SemJornadaReducida.Index, i).Value), "", Me.TablaAltas.Item(SemJornadaReducida.Index, i).Value).ToString.PadRight(1, " ")
                        Fecha_Movimiento = IIf(IsNothing(Me.TablaAltas.Item(FechaMov.Index, i).Value), "", Me.TablaAltas.Item(FechaMov.Index, i).Value).ToString.PadRight(8, " ").Replace("/", "")
                        Unidad_Medicina_Familiar = IIf(IsNothing(Me.TablaAltas.Item(UnidadMedicinaFamiliar.Index, i).Value), "", Me.TablaAltas.Item(UnidadMedicinaFamiliar.Index, i).Value).ToString.PadRight(3, "0")
                        Tipo_Movimiento = "08"

                        Guia = IIf(IsNothing(Me.TablaAltas.Item(Gui.Index, i).Value), "", Me.TablaAltas.Item(Gui.Index, i).Value).ToString.PadLeft(3, "0")
                        CURP = IIf(IsNothing(Me.TablaAltas.Item(CUR.Index, i).Value), "", Me.TablaAltas.Item(CUR.Index, i).Value).ToString.PadRight(18, " ")
                        Id_Empresa = IIf(IsNothing(Me.TablaAltas.Item(Mat.Index, i).Value), "", Me.TablaAltas.Item(Mat.Index, i).Value).ToString.PadRight(10, " ")
                        Id_Formato = "9"
                        Fecha_alta = IIf(IsNothing(Me.TablaAltas.Item(Mat.Index, i).Value), "", Me.TablaAltas.Item(Mat.Index, i).Value).ToString.PadRight(10, " ")
                        Fecha_Baja = IIf(IsNothing(Me.TablaAltas.Item(Mat.Index, i).Value), "", Me.TablaAltas.Item(Mat.Index, i).Value).ToString.PadRight(10, " ")

                        Dim cadena As String = ""
                        cadena = Registro_Patronal & Dig_Verificador_RP & No_IMSS & Dig_Verif_IMSS & Ap_paterno & Ap_materno & Nombres & Salario_Base & "000000" & Tipo_Trabajador & Tipo_Salario & Sem_Jornada_Reducida & Fecha_Movimiento & Unidad_Medicina_Familiar & "  " & Tipo_Movimiento & Subd & Guia & ID_matricula & " " & CURP & Id_Formato
                        strStreamWriter.WriteLine(cadena)
                    End If
                    frm.Barra.Value = i
                Next


                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Me.Cursor = Cursors.Arrow
                strStreamWriter.Flush()
                strStreamWriter.Close()
            Else
                strStreamW = File.Create(PathArchivo)
                strStreamWriter = New StreamWriter(strStreamW, System.Text.Encoding.Default)

                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = Me.TablaAltas.Rows.Count - 1
                Me.Cursor = Cursors.AppStarting
                For i As Integer = 0 To Me.TablaAltas.Rows.Count - 1
                    If Me.TablaAltas.Item(SelA.Index, i).Value = True Then
                        ID_matricula = IIf(IsNothing(Me.TablaAltas.Item(Mat.Index, i).Value), "", Me.TablaAltas.Item(Mat.Index, i).Value).ToString.PadRight(10, " ")
                        Ap_paterno = UCase(IIf(IsNothing(Me.TablaAltas.Item(Appaterno.Index, i).Value), "", Me.TablaAltas.Item(Appaterno.Index, i).Value).ToString.PadRight(27, " "))
                        Ap_materno = UCase(IIf(IsNothing(Me.TablaAltas.Item(Apmaterno.Index, i).Value), "", Me.TablaAltas.Item(Apmaterno.Index, i).Value).ToString.PadRight(27, " "))
                        Nombres = UCase(IIf(IsNothing(Me.TablaAltas.Item(Nomb.Index, i).Value), "", Me.TablaAltas.Item(Nomb.Index, i).Value).ToString.PadRight(27, " "))
                        Registro_Patronal = IIf(IsNothing(Me.TablaAltas.Item(RegistroPatronal.Index, i).Value), "", Me.TablaAltas.Item(RegistroPatronal.Index, i).Value).ToString.PadRight(10, " ")
                        Dig_Verificador_RP = IIf(IsNothing(Me.TablaAltas.Item(DigVerificadorRP.Index, i).Value), "", Me.TablaAltas.Item(DigVerificadorRP.Index, i).Value).ToString.PadRight(1, " ")
                        No_IMSS = IIf(IsNothing(Me.TablaAltas.Item(NoIMSS.Index, i).Value), "", Me.TablaAltas.Item(NoIMSS.Index, i).Value).ToString.PadRight(10, " ")
                        Dig_Verif_IMSS = IIf(IsNothing(Me.TablaAltas.Item(DigVerifIMSS.Index, i).Value), "", Me.TablaAltas.Item(DigVerifIMSS.Index, i).Value).ToString.PadRight(1, " ")

                        Dim a As Decimal = 0
                        Dim b As Integer = 0
                        a = Math.Round(IIf(IsNothing(Me.TablaAltas.Item(SalarioBase.Index, i).Value), 0, Me.TablaAltas.Item(SalarioBase.Index, i).Value), 2, MidpointRounding.ToEven)
                        b = Math.Round(IIf(IsNothing(Me.TablaAltas.Item(SalarioBase.Index, i).Value), 0, Me.TablaAltas.Item(SalarioBase.Index, i).Value), 0, MidpointRounding.ToEven)
                        'If (a - b) = 0.00 Then
                        '    Salario_Base = b.ToString.Replace(".", "").PadLeft(6, "0")
                        'Else
                        '    Salario_Base = a.ToString.Replace(".", "").PadLeft(6, "0")
                        'End If
                        Salario_Base = a.ToString.Replace(".", "").PadLeft(6, "0")

                        Tipo_Trabajador = IIf(IsNothing(Me.TablaAltas.Item(TipoTrabajador.Index, i).Value), "", Me.TablaAltas.Item(TipoTrabajador.Index, i).Value).ToString.PadRight(1, " ")
                        Tipo_Salario = IIf(IsNothing(Me.TablaAltas.Item(TipoSalario.Index, i).Value), "", Me.TablaAltas.Item(TipoSalario.Index, i).Value).ToString.PadRight(1, " ")
                        Sem_Jornada_Reducida = IIf(IsNothing(Me.TablaAltas.Item(SemJornadaReducida.Index, i).Value), "", Me.TablaAltas.Item(SemJornadaReducida.Index, i).Value).ToString.PadRight(1, " ")
                        Fecha_Movimiento = IIf(IsNothing(Me.TablaAltas.Item(FechaMov.Index, i).Value), "", Me.TablaAltas.Item(FechaMov.Index, i).Value).ToString.PadRight(8, " ").Replace("/", "")
                        Unidad_Medicina_Familiar = IIf(IsNothing(Me.TablaAltas.Item(UnidadMedicinaFamiliar.Index, i).Value), "", Me.TablaAltas.Item(UnidadMedicinaFamiliar.Index, i).Value).ToString.PadRight(3, "0")
                        Tipo_Movimiento = "08"
                        Guia = IIf(IsNothing(Me.TablaAltas.Item(Gui.Index, i).Value), "", Me.TablaAltas.Item(Gui.Index, i).Value).ToString.PadLeft(3, "0")
                        CURP = IIf(IsNothing(Me.TablaAltas.Item(CUR.Index, i).Value), "", Me.TablaAltas.Item(CUR.Index, i).Value).ToString.PadRight(18, " ")
                        Id_Empresa = IIf(IsNothing(Me.TablaAltas.Item(Mat.Index, i).Value), "", Me.TablaAltas.Item(Mat.Index, i).Value).ToString.PadRight(10, " ")
                        Id_Formato = "9"
                        Fecha_alta = IIf(IsNothing(Me.TablaAltas.Item(Mat.Index, i).Value), "", Me.TablaAltas.Item(Mat.Index, i).Value).ToString.PadRight(10, " ")
                        Fecha_Baja = IIf(IsNothing(Me.TablaAltas.Item(Mat.Index, i).Value), "", Me.TablaAltas.Item(Mat.Index, i).Value).ToString.PadRight(10, " ")

                        Dim cadena As String = ""
                        cadena = Registro_Patronal & Dig_Verificador_RP & No_IMSS & Dig_Verif_IMSS & Ap_paterno & Ap_materno & Nombres & Salario_Base & "000000" & Tipo_Trabajador & Tipo_Salario & Sem_Jornada_Reducida & Fecha_Movimiento & Unidad_Medicina_Familiar & "  " & Tipo_Movimiento & Subd & Guia & ID_matricula & " " & CURP & Id_Formato
                        strStreamWriter.WriteLine(cadena)
                    End If
                    frm.Barra.Value = i
                Next


                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Me.Cursor = Cursors.Arrow
                strStreamWriter.Flush()
                strStreamWriter.Close()
            End If
        Catch ex As Exception
            MsgBox("Error al Guardar la informacion en el archivo. " & ex.ToString, MsgBoxStyle.Critical, Application.ProductName)
            strStreamWriter.Close() ' cerramos
        End Try
    End Sub

    Private Sub Genera_TXTBaja()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim fila As String = Nothing
        Dim strStreamW As Stream = Nothing
        Dim strStreamWriter As StreamWriter = Nothing
        Dim texto As String = Nothing
        ' Donde guardamos los paths (la direccion de mi sukiri) de los archivos que vamos a estar utilizando ..
        Dim PathArchivo As String
        Dim nuevo_archivo As String = ""
        Dim Ordinal As String = ""



        Dim ID_matricula As String = ""
        Dim Ap_paterno As String = ""
        Dim Ap_materno As String = ""
        Dim Nombres As String = ""
        Dim Registro_Patronal As String = ""
        Dim Dig_Verificador_RP As String = ""
        Dim No_IMSS As String = ""
        Dim Dig_Verif_IMSS As String = ""
        Dim Salario_Base As String = ""
        Dim Tipo_Trabajador As String = ""
        Dim Tipo_Salario As String = ""
        Dim Sem_Jornada_Reducida As String = ""
        Dim Fecha_Movimiento As String = ""
        Dim Unidad_Medicina_Familiar As String = ""
        Dim Tipo_Movimiento As String = ""
        Dim Guia As String = ""
        Dim CURP As String = ""
        Dim Clausula As String = ""
        Dim Id_Empresa As String = ""
        Dim Id_Formato As String = ""
        Dim Fecha_alta As String = ""
        Dim Fecha_Baja As String = ""

        Dim rfcNombre As String = Eventos.ObtenerValorDB("Empresa", "RFC", " ID_CLIENTE= " & Me.lstCliente.SelectItem & "", 1)

        Try
            If Directory.Exists("C:\Program Files\Contable\SetupProyectoContable\Altas\" & rfcNombre & "") = False Then ' si no existe la carpeta se crea
                Directory.CreateDirectory("C:\Program Files\Contable\SetupProyectoContable\Altas\" & rfcNombre & "")
            End If
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            PathArchivo = "C:\Program Files\Contable\SetupProyectoContable\Altas\" & rfcNombre & "\" & rfcNombre & "-Bajas.txt" ' Se determina el nombre del archivo para la tranferencia concatenando la fecha actual
            ' asignar el valor de la cuenta

            If File.Exists(PathArchivo) Then

                File.Delete(PathArchivo)
                strStreamW = File.Create(PathArchivo)
                strStreamWriter = New StreamWriter(strStreamW, System.Text.Encoding.Default)

                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = Me.TablaBajas.Rows.Count - 1
                Me.Cursor = Cursors.AppStarting
                For i As Integer = 0 To Me.TablaBajas.Rows.Count - 1
                    If Me.TablaBajas.Item(SelB.Index, i).Value = True And Candados(TablaBajas, i) Then


                        ID_matricula = IIf(IsNothing(Me.TablaBajas.Item(Matb.Index, i).Value), "", Me.TablaBajas.Item(Matb.Index, i).Value).ToString.PadRight(10, " ")
                        Ap_paterno = UCase(IIf(IsNothing(Me.TablaBajas.Item(APB.Index, i).Value), "", Me.TablaBajas.Item(APB.Index, i).Value).ToString.PadRight(27, " "))
                        Ap_materno = UCase(IIf(IsNothing(Me.TablaBajas.Item(AMB.Index, i).Value), "", Me.TablaBajas.Item(AMB.Index, i).Value).ToString.PadRight(27, " "))
                        Nombres = UCase(IIf(IsNothing(Me.TablaBajas.Item(NombB.Index, i).Value), "", Me.TablaBajas.Item(NombB.Index, i).Value).ToString.PadRight(27, " "))
                        Registro_Patronal = IIf(IsNothing(Me.TablaBajas.Item(RPB.Index, i).Value), "", Me.TablaBajas.Item(RPB.Index, i).Value).ToString.PadRight(10, " ")
                        Dig_Verificador_RP = IIf(IsNothing(Me.TablaBajas.Item(DVRPB.Index, i).Value), "", Me.TablaBajas.Item(DVRPB.Index, i).Value).ToString.PadRight(1, " ")
                        No_IMSS = IIf(IsNothing(Me.TablaBajas.Item(NIMMSB.Index, i).Value), "", Me.TablaBajas.Item(NIMMSB.Index, i).Value).ToString.PadRight(10, " ")
                        Dig_Verif_IMSS = IIf(IsNothing(Me.TablaBajas.Item(DVNIMSSB.Index, i).Value), "", Me.TablaBajas.Item(DVNIMSSB.Index, i).Value).ToString.PadRight(1, " ")
                        Dim a As Decimal = 0
                        Dim b As Integer = 0
                        a = Math.Round(IIf(IsNothing(Me.TablaBajas.Item(SBB.Index, i).Value), 0, Me.TablaBajas.Item(SBB.Index, i).Value), 2, MidpointRounding.ToEven)
                        b = Math.Round(IIf(IsNothing(Me.TablaBajas.Item(SBB.Index, i).Value), 0, Me.TablaBajas.Item(SBB.Index, i).Value), 0, MidpointRounding.ToEven)
                        'If (a - b) = 0.00 Then
                        '    Salario_Base = b.ToString.Replace(".", "").PadLeft(6, "0")
                        'Else
                        '    Salario_Base = a.ToString.Replace(".", "").PadLeft(6, "0")
                        'End If
                        Salario_Base = a.ToString.Replace(".", "").PadLeft(6, "0")
                        Tipo_Trabajador = IIf(IsNothing(Me.TablaBajas.Item(TTB.Index, i).Value), "", Me.TablaBajas.Item(TTB.Index, i).Value).ToString.PadRight(1, " ")
                        Tipo_Salario = IIf(IsNothing(Me.TablaBajas.Item(TSB.Index, i).Value), "", Me.TablaBajas.Item(TSB.Index, i).Value).ToString.PadRight(1, " ")
                        Sem_Jornada_Reducida = IIf(IsNothing(Me.TablaBajas.Item(SJRB.Index, i).Value), "", Me.TablaBajas.Item(SJRB.Index, i).Value).ToString.PadRight(1, " ")
                        Fecha_Movimiento = IIf(IsNothing(Me.TablaBajas.Item(FechaB.Index, i).Value), "", Me.TablaBajas.Item(FechaB.Index, i).Value).ToString.PadRight(8, " ")
                        Unidad_Medicina_Familiar = IIf(IsNothing(Me.TablaBajas.Item(UMFB.Index, i).Value), "", Me.TablaBajas.Item(UMFB.Index, i).Value).ToString.PadRight(3, " ")
                        Tipo_Movimiento = "02"
                        Guia = IIf(IsNothing(Me.TablaBajas.Item(GB.Index, i).Value), "", Me.TablaBajas.Item(GB.Index, i).Value).ToString.PadLeft(3, "0")
                        CURP = IIf(IsNothing(Me.TablaBajas.Item(CB.Index, i).Value), "", Me.TablaBajas.Item(CB.Index, i).Value).ToString.PadRight(18, " ")
                        Clausula = IIf(IsNothing(Me.TablaBajas.Item(Clau.Index, i).Value), "1", Me.TablaBajas.Item(Clau.Index, i).Value).ToString.PadRight(1, " ")
                        Id_Empresa = Me.lstCliente.SelectItem
                        Id_Formato = "9"
                        Fecha_alta = IIf(IsNothing(Me.TablaBajas.Item(Mat.Index, i).Value), "", Me.TablaBajas.Item(Mat.Index, i).Value).ToString.PadRight(10, " ")
                        Fecha_Baja = IIf(IsNothing(Me.TablaBajas.Item(Mat.Index, i).Value), "", Me.TablaBajas.Item(Mat.Index, i).Value).ToString.PadRight(10, " ")

                        Dim cadena As String = ""
                        cadena = Registro_Patronal & Dig_Verificador_RP & No_IMSS & Dig_Verif_IMSS & Ap_paterno & Ap_materno & Nombres & "               " & Fecha_Movimiento & "     " & Tipo_Movimiento & Subd & Guia & ID_matricula & Clausula & "                  " & Id_Formato
                        strStreamWriter.WriteLine(cadena)
                        Baja_personal(Me.TablaBajas.Item(IDB.Index, i).Value, Me.TablaBajas.Item(FechaB.Index, i).Value)
                    End If
                    frm.Barra.Value = i
                Next

                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Me.Cursor = Cursors.Arrow
                strStreamWriter.Flush()
                strStreamWriter.Close()
            Else
                strStreamW = File.Create(PathArchivo)
                strStreamWriter = New StreamWriter(strStreamW, System.Text.Encoding.Default)

                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = Me.TablaBajas.Rows.Count - 1
                Me.Cursor = Cursors.AppStarting
                For i As Integer = 0 To Me.TablaBajas.Rows.Count - 1
                    If Me.TablaBajas.Item(SelB.Index, i).Value = True And Candados(TablaBajas, i) Then
                        ID_matricula = IIf(IsNothing(Me.TablaBajas.Item(Matb.Index, i).Value), "", Me.TablaBajas.Item(Matb.Index, i).Value).ToString.PadRight(10, " ")
                        Ap_paterno = UCase(IIf(IsNothing(Me.TablaBajas.Item(APB.Index, i).Value), "", Me.TablaBajas.Item(APB.Index, i).Value).ToString.PadRight(27, " "))
                        Ap_materno = UCase(IIf(IsNothing(Me.TablaBajas.Item(AMB.Index, i).Value), "", Me.TablaBajas.Item(AMB.Index, i).Value).ToString.PadRight(27, " "))
                        Nombres = UCase(IIf(IsNothing(Me.TablaBajas.Item(NombB.Index, i).Value), "", Me.TablaBajas.Item(NombB.Index, i).Value).ToString.PadRight(27, " "))
                        Registro_Patronal = IIf(IsNothing(Me.TablaBajas.Item(RPB.Index, i).Value), "", Me.TablaBajas.Item(RPB.Index, i).Value).ToString.PadRight(10, " ")
                        Dig_Verificador_RP = IIf(IsNothing(Me.TablaBajas.Item(DVRPB.Index, i).Value), "", Me.TablaBajas.Item(DVRPB.Index, i).Value).ToString.PadRight(1, " ")
                        No_IMSS = IIf(IsNothing(Me.TablaBajas.Item(NIMMSB.Index, i).Value), "", Me.TablaBajas.Item(NIMMSB.Index, i).Value).ToString.PadRight(10, " ")
                        Dig_Verif_IMSS = IIf(IsNothing(Me.TablaBajas.Item(DVNIMSSB.Index, i).Value), "", Me.TablaBajas.Item(DVNIMSSB.Index, i).Value).ToString.PadRight(1, " ")

                        Dim a As Decimal = 0
                        Dim b As Integer = 0
                        a = Math.Round(IIf(IsNothing(Me.TablaBajas.Item(SBB.Index, i).Value), 0, Me.TablaBajas.Item(SBB.Index, i).Value), 2, MidpointRounding.ToEven)
                        b = Math.Round(IIf(IsNothing(Me.TablaBajas.Item(SBB.Index, i).Value), 0, Me.TablaBajas.Item(SBB.Index, i).Value), 0, MidpointRounding.ToEven)
                        'If (a - b) = 0.00 Then
                        '    Salario_Base = b.ToString.Replace(".", "").PadLeft(6, "0")
                        'Else
                        '    Salario_Base = a.ToString.Replace(".", "").PadLeft(6, "0")
                        'End If
                        Salario_Base = a.ToString.Replace(".", "").PadLeft(6, "0")
                        Tipo_Trabajador = IIf(IsNothing(Me.TablaBajas.Item(TTB.Index, i).Value), "", Me.TablaBajas.Item(TTB.Index, i).Value).ToString.PadRight(1, " ")
                        Tipo_Salario = IIf(IsNothing(Me.TablaBajas.Item(TSB.Index, i).Value), "", Me.TablaBajas.Item(TSB.Index, i).Value).ToString.PadRight(1, " ")
                        Sem_Jornada_Reducida = IIf(IsNothing(Me.TablaBajas.Item(SJRB.Index, i).Value), "", Me.TablaBajas.Item(SJRB.Index, i).Value).ToString.PadRight(1, " ")
                        Fecha_Movimiento = IIf(IsNothing(Me.TablaBajas.Item(FechaB.Index, i).Value), "", Me.TablaBajas.Item(FechaB.Index, i).Value).ToString.PadRight(8, " ")
                        Unidad_Medicina_Familiar = IIf(IsNothing(Me.TablaBajas.Item(UMFB.Index, i).Value), "", Me.TablaBajas.Item(UMFB.Index, i).Value).ToString.PadRight(3, " ")
                        Tipo_Movimiento = "02"
                        Guia = IIf(IsNothing(Me.TablaBajas.Item(GB.Index, i).Value), "", Me.TablaBajas.Item(GB.Index, i).Value).ToString.PadLeft(3, "0")
                        CURP = IIf(IsNothing(Me.TablaBajas.Item(CB.Index, i).Value), "", Me.TablaBajas.Item(CB.Index, i).Value).ToString.PadRight(18, " ")
                        Id_Empresa = Me.lstCliente.SelectItem
                        Id_Formato = "9"
                        Fecha_alta = IIf(IsNothing(Me.TablaBajas.Item(Mat.Index, i).Value), "", Me.TablaBajas.Item(Mat.Index, i).Value).ToString.PadRight(10, " ")
                        Fecha_Baja = IIf(IsNothing(Me.TablaBajas.Item(Mat.Index, i).Value), "", Me.TablaBajas.Item(Mat.Index, i).Value).ToString.PadRight(10, " ")

                        Dim cadena As String = ""
                        cadena = Registro_Patronal & Dig_Verificador_RP & No_IMSS & Dig_Verif_IMSS & Ap_paterno & Ap_materno & Nombres & "               " & Tipo_Trabajador & Tipo_Salario & Sem_Jornada_Reducida & Fecha_Movimiento & Unidad_Medicina_Familiar & "  " & Tipo_Movimiento & Subd & Guia & ID_matricula & " " & CURP & Id_Formato
                        strStreamWriter.WriteLine(cadena)
                        'Actualiza el registro con la baja
                        Baja_personal(Me.TablaBajas.Item(IDB.Index, i).Value, Me.TablaBajas.Item(FechaB.Index, i).Value)
                    End If
                    frm.Barra.Value = i
                Next

                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Me.Cursor = Cursors.Arrow
                strStreamWriter.Flush()
                strStreamWriter.Close()
            End If
        Catch ex As Exception
            MsgBox("Error al Guardar la informacion en el archivo. " & ex.ToString, MsgBoxStyle.Critical, Application.ProductName)
            strStreamWriter.Close() ' cerramos
        End Try
    End Sub
    Private Sub Genera_TXTReingreso()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim fila As String = Nothing
        Dim strStreamW As Stream = Nothing
        Dim strStreamWriter As StreamWriter = Nothing
        Dim texto As String = Nothing
        ' Donde guardamos los paths (la direccion de mi sukiri) de los archivos que vamos a estar utilizando ..
        Dim PathArchivo As String
        Dim nuevo_archivo As String = ""
        Dim Ordinal As String = ""



        Dim ID_matricula As String = ""
        Dim Ap_paterno As String = ""
        Dim Ap_materno As String = ""
        Dim Nombres As String = ""
        Dim Registro_Patronal As String = ""
        Dim Dig_Verificador_RP As String = ""
        Dim No_IMSS As String = ""
        Dim Dig_Verif_IMSS As String = ""
        Dim Salario_Base As String = ""
        Dim Tipo_Trabajador As String = ""
        Dim Tipo_Salario As String = ""
        Dim Sem_Jornada_Reducida As String = ""
        Dim Fecha_Movimiento As String = ""
        Dim Unidad_Medicina_Familiar As String = ""
        Dim Tipo_Movimiento As String = ""
        Dim Guia As String = ""
        Dim CURP As String = ""
        Dim Clausula As String = ""
        Dim Id_Empresa As String = ""
        Dim Id_Formato As String = ""
        Dim Fecha_alta As String = ""
        Dim Fecha_Baja As String = ""

        Dim rfcNombre As String = Eventos.ObtenerValorDB("Empresa", "RFC", " ID_CLIENTE= " & Me.lstCliente.SelectItem & "", 1)

        Try
            If Directory.Exists("C:\Program Files\Contable\SetupProyectoContable\Altas\" & rfcNombre & "") = False Then ' si no existe la carpeta se crea
                Directory.CreateDirectory("C:\Program Files\Contable\SetupProyectoContable\Altas\" & rfcNombre & "")
            End If
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            PathArchivo = "C:\Program Files\Contable\SetupProyectoContable\Altas\" & rfcNombre & "\" & rfcNombre & "-ModifSalarios.txt" ' Se determina el nombre del archivo para la tranferencia concatenando la fecha actual
            ' asignar el valor de la cuenta

            If File.Exists(PathArchivo) Then

                File.Delete(PathArchivo)
                strStreamW = File.Create(PathArchivo)
                strStreamWriter = New StreamWriter(strStreamW, System.Text.Encoding.Default)

                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = Me.TablaReingreso.Rows.Count - 1
                Me.Cursor = Cursors.AppStarting
                For i As Integer = 0 To Me.TablaReingreso.Rows.Count - 1
                    If Me.TablaReingreso.Item(Selr.Index, i).Value = True Then


                        ID_matricula = IIf(IsNothing(Me.TablaReingreso.Item(Matr.Index, i).Value), "", Me.TablaReingreso.Item(Matr.Index, i).Value).ToString.PadRight(10, " ")
                        Ap_paterno = UCase(IIf(IsNothing(Me.TablaReingreso.Item(APR.Index, i).Value), "", Me.TablaReingreso.Item(APR.Index, i).Value).ToString.PadRight(27, " "))
                        Ap_materno = UCase(IIf(IsNothing(Me.TablaReingreso.Item(AMR.Index, i).Value), "", Me.TablaReingreso.Item(AMR.Index, i).Value).ToString.PadRight(27, " "))
                        Nombres = UCase(IIf(IsNothing(Me.TablaReingreso.Item(Nombr.Index, i).Value), "", Me.TablaReingreso.Item(Nombr.Index, i).Value).ToString.PadRight(27, " "))
                        Registro_Patronal = IIf(IsNothing(Me.TablaReingreso.Item(RPR.Index, i).Value), "", Me.TablaReingreso.Item(RPR.Index, i).Value).ToString.PadRight(10, " ")
                        Dig_Verificador_RP = IIf(IsNothing(Me.TablaReingreso.Item(DVRPR.Index, i).Value), "", Me.TablaReingreso.Item(DVRPR.Index, i).Value).ToString.PadRight(1, " ")
                        No_IMSS = IIf(IsNothing(Me.TablaReingreso.Item(NIR.Index, i).Value), "", Me.TablaReingreso.Item(NIR.Index, i).Value).ToString.PadRight(10, " ")
                        Dig_Verif_IMSS = IIf(IsNothing(Me.TablaReingreso.Item(DVIR.Index, i).Value), "", Me.TablaReingreso.Item(DVIR.Index, i).Value).ToString.PadRight(1, " ")
                        Dim a As Decimal = 0
                        Dim b As Integer = 0
                        a = Math.Round(IIf(IsNothing(Me.TablaReingreso.Item(SBR.Index, i).Value), 0, Me.TablaReingreso.Item(SBR.Index, i).Value), 2, MidpointRounding.ToEven)
                        b = Math.Round(IIf(IsNothing(Me.TablaReingreso.Item(SBR.Index, i).Value), 0, Me.TablaReingreso.Item(SBR.Index, i).Value), 0, MidpointRounding.ToEven)
                        'If (a - b) = 0.00 Then
                        '    Salario_Base = b.ToString.Replace(".", "").PadLeft(6, "0")
                        'Else
                        '    Salario_Base = a.ToString.Replace(".", "").PadLeft(6, "0")
                        'End If
                        Salario_Base = a.ToString.Replace(".", "").PadLeft(6, "0")

                        Tipo_Trabajador = IIf(IsNothing(Me.TablaReingreso.Item(TTR.Index, i).Value), "", Me.TablaReingreso.Item(TTR.Index, i).Value).ToString.PadRight(1, " ")
                        Tipo_Salario = IIf(IsNothing(Me.TablaReingreso.Item(TSR.Index, i).Value), "", Me.TablaReingreso.Item(TSR.Index, i).Value).ToString.PadRight(1, " ")
                        Sem_Jornada_Reducida = IIf(IsNothing(Me.TablaReingreso.Item(SJRR.Index, i).Value), "", Me.TablaReingreso.Item(SJRR.Index, i).Value).ToString.PadRight(1, " ")
                        Fecha_Movimiento = IIf(IsNothing(Me.TablaReingreso.Item(Fechar.Index, i).Value), "", Me.TablaReingreso.Item(Fechar.Index, i).Value).ToString.PadRight(8, " ").Replace("/", "")
                        Unidad_Medicina_Familiar = IIf(IsNothing(Me.TablaReingreso.Item(UMFR.Index, i).Value), "", Me.TablaReingreso.Item(UMFR.Index, i).Value).ToString.PadRight(3, " ")
                        Tipo_Movimiento = "07"
                        Guia = IIf(IsNothing(Me.TablaReingreso.Item(GR.Index, i).Value), "", Me.TablaReingreso.Item(GR.Index, i).Value).ToString.PadLeft(3, "0")
                        CURP = IIf(IsNothing(Me.TablaReingreso.Item(CR.Index, i).Value), "", Me.TablaReingreso.Item(CR.Index, i).Value).ToString.PadRight(18, " ")
                        Id_Empresa = Me.lstCliente.SelectItem
                        Id_Formato = "9"
                        Fecha_alta = IIf(IsNothing(Me.TablaReingreso.Item(Mat.Index, i).Value), "", Me.TablaReingreso.Item(Mat.Index, i).Value).ToString.PadRight(10, " ")
                        Fecha_Baja = IIf(IsNothing(Me.TablaReingreso.Item(Mat.Index, i).Value), "", Me.TablaReingreso.Item(Mat.Index, i).Value).ToString.PadRight(10, " ")

                        Dim cadena As String = ""
                        cadena = Registro_Patronal & Dig_Verificador_RP & No_IMSS & Dig_Verif_IMSS & Ap_paterno & Ap_materno & Nombres & Salario_Base & "0000000" & Tipo_Salario & Sem_Jornada_Reducida & Fecha_Movimiento & "     " & Tipo_Movimiento & Subd & Guia & ID_matricula & " " & CURP & Id_Formato
                        strStreamWriter.WriteLine(cadena)
                    End If
                    frm.Barra.Value = i
                Next

                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Me.Cursor = Cursors.Arrow
                strStreamWriter.Flush()
                strStreamWriter.Close()
            Else
                strStreamW = File.Create(PathArchivo)
                strStreamWriter = New StreamWriter(strStreamW, System.Text.Encoding.Default)

                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = Me.TablaReingreso.Rows.Count - 1
                Me.Cursor = Cursors.AppStarting
                For i As Integer = 0 To Me.TablaReingreso.Rows.Count - 1
                    If Me.TablaReingreso.Item(Selr.Index, i).Value = True Then


                        ID_matricula = IIf(IsNothing(Me.TablaReingreso.Item(Matr.Index, i).Value), "", Me.TablaReingreso.Item(Matr.Index, i).Value).ToString.PadRight(10, " ")
                        Ap_paterno = UCase(IIf(IsNothing(Me.TablaReingreso.Item(APR.Index, i).Value), "", Me.TablaReingreso.Item(APR.Index, i).Value).ToString.PadRight(27, " "))
                        Ap_materno = UCase(IIf(IsNothing(Me.TablaReingreso.Item(AMR.Index, i).Value), "", Me.TablaReingreso.Item(AMR.Index, i).Value).ToString.PadRight(27, " "))
                        Nombres = UCase(IIf(IsNothing(Me.TablaReingreso.Item(Nombr.Index, i).Value), "", Me.TablaReingreso.Item(Nombr.Index, i).Value).ToString.PadRight(27, " "))
                        Registro_Patronal = IIf(IsNothing(Me.TablaReingreso.Item(RPR.Index, i).Value), "", Me.TablaReingreso.Item(RPR.Index, i).Value).ToString.PadRight(10, " ")
                        Dig_Verificador_RP = IIf(IsNothing(Me.TablaReingreso.Item(DVRPR.Index, i).Value), "", Me.TablaReingreso.Item(DVRPR.Index, i).Value).ToString.PadRight(1, " ")
                        No_IMSS = IIf(IsNothing(Me.TablaReingreso.Item(NIR.Index, i).Value), "", Me.TablaReingreso.Item(NIR.Index, i).Value).ToString.PadRight(10, " ")
                        Dig_Verif_IMSS = IIf(IsNothing(Me.TablaReingreso.Item(DVIR.Index, i).Value), "", Me.TablaReingreso.Item(DVIR.Index, i).Value).ToString.PadRight(1, " ")

                        Dim a As Decimal = 0
                        Dim b As Integer = 0
                        a = Math.Round(IIf(IsNothing(Me.TablaReingreso.Item(SBR.Index, i).Value), 0, Me.TablaReingreso.Item(SBR.Index, i).Value), 2, MidpointRounding.ToEven)
                        b = Math.Round(IIf(IsNothing(Me.TablaReingreso.Item(SBR.Index, i).Value), 0, Me.TablaReingreso.Item(SBR.Index, i).Value), 0, MidpointRounding.ToEven)
                        'If (a - b) = 0.00 Then
                        '    Salario_Base = b.ToString.Replace(".", "").PadLeft(6, "0")
                        'Else
                        '    Salario_Base = a.ToString.Replace(".", "").PadLeft(6, "0")
                        'End If
                        Salario_Base = a.ToString.Replace(".", "").PadLeft(6, "0")

                        Tipo_Trabajador = IIf(IsNothing(Me.TablaReingreso.Item(TTR.Index, i).Value), "", Me.TablaReingreso.Item(TTR.Index, i).Value).ToString.PadRight(1, " ")
                        Tipo_Salario = IIf(IsNothing(Me.TablaReingreso.Item(TSR.Index, i).Value), "", Me.TablaReingreso.Item(TSR.Index, i).Value).ToString.PadRight(1, " ")
                        Sem_Jornada_Reducida = IIf(IsNothing(Me.TablaReingreso.Item(SJRR.Index, i).Value), "", Me.TablaReingreso.Item(SJRR.Index, i).Value).ToString.PadRight(1, " ")
                        Fecha_Movimiento = IIf(IsNothing(Me.TablaReingreso.Item(Fechar.Index, i).Value), "", Me.TablaReingreso.Item(Fechar.Index, i).Value).ToString.PadRight(8, " ").Replace("/", "")
                        Unidad_Medicina_Familiar = IIf(IsNothing(Me.TablaReingreso.Item(UMFR.Index, i).Value), "", Me.TablaReingreso.Item(UMFR.Index, i).Value).ToString.PadRight(3, " ")
                        Tipo_Movimiento = "07"
                        Guia = IIf(IsNothing(Me.TablaReingreso.Item(GR.Index, i).Value), "", Me.TablaReingreso.Item(GR.Index, i).Value).ToString.PadLeft(5, "0")
                        CURP = IIf(IsNothing(Me.TablaReingreso.Item(CR.Index, i).Value), "", Me.TablaReingreso.Item(CR.Index, i).Value).ToString.PadRight(18, " ")
                        Id_Empresa = Me.lstCliente.SelectItem
                        Id_Formato = "9"
                        Fecha_alta = IIf(IsNothing(Me.TablaReingreso.Item(Mat.Index, i).Value), "", Me.TablaReingreso.Item(Mat.Index, i).Value).ToString.PadRight(10, " ")
                        Fecha_Baja = IIf(IsNothing(Me.TablaReingreso.Item(Mat.Index, i).Value), "", Me.TablaReingreso.Item(Mat.Index, i).Value).ToString.PadRight(10, " ")
                        Dim cadena As String = ""
                        cadena = Registro_Patronal & Dig_Verificador_RP & No_IMSS & Dig_Verif_IMSS & Ap_paterno & Ap_materno & Nombres & Salario_Base & "0000000" & Tipo_Salario & Sem_Jornada_Reducida & Fecha_Movimiento & "     " & Tipo_Movimiento & Subd & Guia & ID_matricula & " " & CURP & Id_Formato
                        strStreamWriter.WriteLine(cadena)
                    End If
                    frm.Barra.Value = i
                Next


                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Me.Cursor = Cursors.Arrow
                strStreamWriter.Flush()
                strStreamWriter.Close()
            End If
        Catch ex As Exception
            MsgBox("Error al Guardar la informacion en el archivo. " & ex.ToString, MsgBoxStyle.Critical, Application.ProductName)
            strStreamWriter.Close() ' cerramos
        End Try
    End Sub
    Private Sub CmdAbrir_Click(sender As Object, e As EventArgs) Handles CmdAbrir.Click
        Eventos.Abrir_Capeta("C:\Program Files\Contable\SetupProyectoContable\Altas")
    End Sub
    Private Function Candados(ByVal T As DataGridView, ByVal Posicion As Integer)
        Dim Fecha As Date = Today
        Dim F As String = ""
        Dim Hacer As Boolean
        Dim Objeto As Dia_Correcto
        Try


            If T.Name = "TablaAltas" Then
                If T.Item(Fechaalta.Index, Posicion).Value.ToString <> "" Then
                    F = T.Item(Fechaalta.Index, Posicion).Value.ToString
                    Fecha = Convert.ToDateTime(IIf(Len(F) = 8, F.Substring(0, 2) & "/" & F.Substring(2, 2) & "/" & F.Substring(4, 4), F))
                    Objeto = Dias_Habiles(Fecha)
                    If Objeto.Hacer Then
                        Hacer = True
                    Else
                        T.Item(Fechaalta.Index, Posicion).Value = Objeto.Dia.ToString.Substring(0, 10).Replace("/", "")
                        T.Item(Fechaalta.Index, Posicion).Style.BackColor = Color.Yellow
                        Hacer = False
                    End If
                Else
                    Hacer = False
                End If
            ElseIf T.Name = "TablaBajas" Then
                If (T.Item(FechaB.Index, Posicion).Value.ToString <> "" Or T.Item(FechaB.Index, Posicion).Value.ToString <> Nothing) And T.Item(Clau.Index, Posicion).Value.ToString <> "" Then

                    F = T.Item(FechaB.Index, Posicion).Value.ToString.Replace("/", "")
                    Fecha = Convert.ToDateTime(IIf(Len(F) = 8, F.Substring(0, 2) & "/" & F.Substring(2, 2) & "/" & F.Substring(4, 4), F))
                    Objeto = Dias_Habiles(Fecha)
                    If Objeto.Hacer Then
                        Hacer = True
                    Else
                        T.Item(FechaB.Index, Posicion).Value = Objeto.Dia.ToString.Substring(0, 10).Replace("/", "")
                        T.Item(FechaB.Index, Posicion).Style.BackColor = Color.Yellow
                        Hacer = False
                    End If
                Else
                    Hacer = False
                End If
            ElseIf T.Name = "TablaReingreso" Then
                If T.Item(Fechar.Index, Posicion).Value.ToString <> "" Then


                    F = T.Item(Fechar.Index, Posicion).Value.ToString
                    Fecha = Convert.ToDateTime(IIf(Len(F) = 8, F.Substring(0, 2) & "/" & F.Substring(2, 2) & "/" & F.Substring(4, 4), F))
                    Objeto = Dias_Habiles(Fecha)
                    If Objeto.Hacer Then
                        Hacer = True
                    Else
                        T.Item(Fechar.Index, Posicion).Value = Objeto.Dia.ToString.Substring(0, 10).Replace("/", "")
                        T.Item(Fechar.Index, Posicion).Style.BackColor = Color.Yellow
                        Hacer = False
                    End If
                Else
                    Hacer = False
                End If
            End If
        Catch ex As Exception
            Hacer = False
        End Try
        Return Hacer
    End Function
    Public Class Dia_Correcto
        Public Property Dia As Date
        Public Property Hacer As Boolean

    End Class
    Private Function Dias_Habiles(ByVal Fecha As Date) As Dia_Correcto
        Dim Dia As Date
        Dim Dia_Sugerido As Date
        Dim Objeto As New Dia_Correcto
        Dim Dias As Long
        Dim Contador_de_dias As Integer

        If Today > Fecha Then
            Dias = DateAndTime.DateDiff(DateInterval.Day, Fecha, Today)
            Dia = Today
        Else
            Dia = Fecha
        End If

        Contador_de_dias = 0
        For I = 1 To Dias
            Dia = DateAndTime.DateAdd(DateInterval.Day, -1, Dia)
            If Format(Dia, "dddd") <> "sábado" And Format(Dia, "dddd") <> "domingo" Then
                Contador_de_dias = Contador_de_dias + 1
                If Contador_de_dias = 5 Then
                    Dia_Sugerido = Dia
                End If
            End If
        Next

        If Contador_de_dias > 5 Then
            Objeto.Dia = Dia_Sugerido
            Objeto.Hacer = False

        Else
            Objeto.Dia = Fecha
            Objeto.Hacer = True
        End If

        Return Objeto
    End Function
    Private Sub TablaAltas_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles TablaAltas.CellClick
        Try


            Dim columna As Integer = Me.TablaAltas.CurrentCell.ColumnIndex
            Dim Nombre As String
            Nombre = Me.TablaAltas.Columns.Item(Me.TablaAltas.CurrentCell.ColumnIndex).Name.ToString
            If Me.tabla.Rows.Count > 0 Then
                Me.tabla.Rows.Clear()
            End If

            Select Case Nombre
                Case "TipoSalario"
                    Me.tabla.Rows.Add("0", "Salario Fijo")
                    Me.tabla.Rows.Add("1", "Salario Variable")
                    Me.tabla.Rows.Add("2", "Salario Mixto")
                Case "TipoTrabajador"
                    Me.tabla.Rows.Add("1", "Trabajador Permanente")
                    Me.tabla.Rows.Add("2", "Trabajador Eventual Ciudad")
                    Me.tabla.Rows.Add("3", "Trabajador Eventual Construccion")
                    Me.tabla.Rows.Add("4", "Trabajador Eventual Campo")
                Case "SemJornadaReducida"
                    Me.tabla.Rows.Add("0", "Jornada Normal")
                    Me.tabla.Rows.Add("1", "Un Dia")
                    Me.tabla.Rows.Add("2", "Dos Dias")
                    Me.tabla.Rows.Add("3", "Tres Dias")
                    Me.tabla.Rows.Add("4", "Cuatro Dias")
                    Me.tabla.Rows.Add("5", "Cinco Dias")
                    Me.tabla.Rows.Add("6", "Jornada Reducida")
            End Select

        Catch ex As Exception

        End Try
    End Sub
    Private Sub TablaBajas_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles TablaBajas.CellClick
        Try


            Dim columna As Integer = Me.TablaBajas.CurrentCell.ColumnIndex
            Dim Nombre As String
            Nombre = Me.TablaBajas.Columns.Item(Me.TablaBajas.CurrentCell.ColumnIndex).Name.ToString
            If Me.tabla.Rows.Count > 0 Then
                Me.tabla.Rows.Clear()
            End If

            Select Case Nombre
                Case "TSB"
                    Me.tabla.Rows.Add("0", "Salario Fijo")
                    Me.tabla.Rows.Add("1", "Salario Variable")
                    Me.tabla.Rows.Add("2", "Salario Mixto")
                Case "TTB"
                    Me.tabla.Rows.Add("1", "Trabajador Permanente")
                    Me.tabla.Rows.Add("2", "Trabajador Eventual Ciudad")
                    Me.tabla.Rows.Add("3", "Trabajador Eventual Construccion")
                    Me.tabla.Rows.Add("4", "Trabajador Eventual Campo")
                Case "SJRB"
                    Me.tabla.Rows.Add("0", "Jornada Normal")
                    Me.tabla.Rows.Add("1", "Un Dia")
                    Me.tabla.Rows.Add("2", "Dos Dias")
                    Me.tabla.Rows.Add("3", "Tres Dias")
                    Me.tabla.Rows.Add("4", "Cuatro Dias")
                    Me.tabla.Rows.Add("5", "Cinco Dias")
                    Me.tabla.Rows.Add("6", "Jornada Reducida")
                Case "Clau"
                    Me.tabla.Rows.Add("1", "Termino de Contrato")
                    Me.tabla.Rows.Add("2", "Separacion Voluntaria")
                    Me.tabla.Rows.Add("3", "Abandono de Empleo")
                    Me.tabla.Rows.Add("4", "Defuncion")
                    Me.tabla.Rows.Add("5", "Clausura")
                    Me.tabla.Rows.Add("6", "Otras")
                    Me.tabla.Rows.Add("7", "Ausentismo")
                    Me.tabla.Rows.Add("8", "Rescision de Contrato")
                    Me.tabla.Rows.Add("9", "Jubilacion")
                    Me.tabla.Rows.Add("A", "Pension")
                    Me.LstTexto.Cargar(" SELECT  '1', 'Termino de Contrato'  UNION SELECT  '2' , 'Separacion Voluntaria' UNION SELECT '3' ,'Abandono de Empleo' 
                        UNION SELECT '4' , 'Defuncion' UNION SELECT '5' , 'Clausura' 
                        UNION SELECT '6' , 'Otras' 
                        UNION SELECT '7' , 'Ausentismo' 
                        UNION SELECT '8' , 'Rescision de Contrato' UNION SELECT '9' , 'Jubilacion'  UNION SELECT 'A' , 'Pension'")
                    Me.LstTexto.SelectText = ""
            End Select
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TablaReingreso_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles TablaReingreso.CellClick
        Try


            Dim columna As Integer = Me.TablaReingreso.CurrentCell.ColumnIndex
            Dim Nombre As String
            Nombre = Me.TablaReingreso.Columns.Item(Me.TablaReingreso.CurrentCell.ColumnIndex).Name.ToString
            If Me.tabla.Rows.Count > 0 Then
                Me.tabla.Rows.Clear()
            End If

            Select Case Nombre
                Case "TSR"
                    Me.tabla.Rows.Add("0", "Salario Fijo")
                    Me.tabla.Rows.Add("1", "Salario Variable")
                    Me.tabla.Rows.Add("2", "Salario Mixto")
                Case "TTR"
                    Me.tabla.Rows.Add("1", "Trabajador Permanente")
                    Me.tabla.Rows.Add("2", "Trabajador Eventual Ciudad")
                    Me.tabla.Rows.Add("3", "Trabajador Eventual Construccion")
                    Me.tabla.Rows.Add("4", "Trabajador Eventual Campo")
                Case "SJRR"
                    Me.tabla.Rows.Add("0", "Jornada Normal")
                    Me.tabla.Rows.Add("1", "Un Dia")
                    Me.tabla.Rows.Add("2", "Dos Dias")
                    Me.tabla.Rows.Add("3", "Tres Dias")
                    Me.tabla.Rows.Add("4", "Cuatro Dias")
                    Me.tabla.Rows.Add("5", "Cinco Dias")
                    Me.tabla.Rows.Add("6", "Jornada Reducida")
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LstTexto_Enters() Handles LstTexto.Enters
        If Me.TablasAltasBajasReingresos.SelectedIndex = 0 Then
            Try

                If Me.TablaAltas.Rows.Count > 0 Then
                    For Each Fila As DataGridViewRow In TablaAltas.Rows
                        If Fila.Cells(FechaMov.Index).Selected = True Then
                            Try
                                Fila.Cells(FechaMov.Index).Value = Trim(Me.LstTexto.SelectText.ToString.Replace("/", ""))
                            Catch ex As Exception

                            End Try
                        End If
                    Next
                End If
            Catch ex As Exception

            End Try
        ElseIf Me.TablasAltasBajasReingresos.SelectedIndex = 1 Then
            Try

                If Me.TablaBajas.Rows.Count > 0 Then
                    For Each Fila As DataGridViewRow In TablaBajas.Rows
                        If Fila.Cells(FechaB.Index).Selected = True Then
                            Try
                                Fila.Cells(FechaB.Index).Value = Trim(Me.LstTexto.SelectText.ToString.Replace("/", ""))
                            Catch ex As Exception

                            End Try
                        ElseIf Fila.Cells(Clau.Index).Selected = True Then
                            Try
                                Fila.Cells(Clau.Index).Value = Trim(Me.LstTexto.SelectItem)
                            Catch ex As Exception

                            End Try
                        End If
                    Next
                End If
            Catch ex As Exception

            End Try

        ElseIf Me.TablasAltasBajasReingresos.SelectedIndex = 2 Then
            Try

                If Me.TablaReingreso.Rows.Count > 0 Then
                    For Each Fila As DataGridViewRow In TablaReingreso.Rows
                        If Fila.Cells(Fechar.Index).Selected = True Then
                            Try
                                Fila.Cells(Fechar.Index).Value = Trim(Me.LstTexto.SelectText.ToString.Replace("/", ""))
                            Catch ex As Exception

                            End Try
                        End If
                    Next
                End If
            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Sub Baja_personal(ByVal ID As Integer, ByVal Fecha As String)
        Fecha = Convert.ToDateTime(IIf(Len(Fecha) = 8, Fecha.Substring(0, 2) & "/" & Fecha.Substring(2, 2) & "/" & Fecha.Substring(4, 4), Fecha))
        Dim sql As String = "UPDATE dbo.Personal_Clientes SET Fecha_Baja = " & Eventos.Sql_hoy(Fecha) & "
                            WHERE Id_Persona_Cliente = " & ID & " AND Id_Empresa = " & Me.lstCliente.SelectItem & " "
        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
End Class