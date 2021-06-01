<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VentadeActivos
    Inherits Telerik.WinControls.UI.RadForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VentadeActivos))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.ComboMes2 = New Telerik.WinControls.UI.RadDropDownList()
        Me.ComboMes = New Telerik.WinControls.UI.RadDropDownList()
        Me.LstAnio = New Telerik.WinControls.UI.RadDropDownList()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CmdCalcular = New Telerik.WinControls.UI.RadButton()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.CmdGuardar = New Telerik.WinControls.UI.RadButton()
        Me.CmdAgregar = New Telerik.WinControls.UI.RadButton()
        Me.CmdListar = New Telerik.WinControls.UI.RadButton()
        Me.RadPanel2 = New Telerik.WinControls.UI.RadPanel()
        Me.TablaVentasActivos = New System.Windows.Forms.DataGridView()
        Me.cta = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Folio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CtaDepActivo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ResulDep1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ResulDep2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ResulDep3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ResulDep4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ref = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Client = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NumFactura = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaFactura = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ImpGF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ImpExF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IvaF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechAdqF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MOIF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DepAcuF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ValorlibrosF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrecioVenta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UtilidadContable = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PerdidaContable = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.separador = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MoiPD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UltMes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FactorUltm = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaAdq = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InpcFecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FactorActual = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CostoFinal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrecioVentaF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UtilidadFisca = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PerdidaFiscal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sumas = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TablaResumen = New System.Windows.Forms.DataGridView()
        Me.DescripR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UtilidadC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PerdidaC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UtilidadF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PerdidaF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.ComboMes2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ComboMes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LstAnio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdCalcular, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdGuardar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdAgregar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdListar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel2.SuspendLayout()
        CType(Me.TablaVentasActivos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TablaResumen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Ayuda
        '
        Me.Ayuda.IsBalloon = True
        Me.Ayuda.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.ComboMes2)
        Me.RadPanel1.Controls.Add(Me.ComboMes)
        Me.RadPanel1.Controls.Add(Me.LstAnio)
        Me.RadPanel1.Controls.Add(Me.Label4)
        Me.RadPanel1.Controls.Add(Me.Label2)
        Me.RadPanel1.Controls.Add(Me.Label1)
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label3)
        Me.RadPanel1.Controls.Add(Me.CmdCalcular)
        Me.RadPanel1.Controls.Add(Me.cmdCerrar)
        Me.RadPanel1.Controls.Add(Me.CmdGuardar)
        Me.RadPanel1.Controls.Add(Me.CmdAgregar)
        Me.RadPanel1.Controls.Add(Me.CmdListar)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1074, 100)
        Me.RadPanel1.TabIndex = 0
        '
        'ComboMes2
        '
        Me.ComboMes2.Location = New System.Drawing.Point(933, 33)
        Me.ComboMes2.Name = "ComboMes2"
        Me.ComboMes2.Size = New System.Drawing.Size(125, 36)
        Me.ComboMes2.TabIndex = 668
        Me.ComboMes2.Text = " "
        Me.ComboMes2.ThemeName = "Material"
        '
        'ComboMes
        '
        Me.ComboMes.Location = New System.Drawing.Point(798, 33)
        Me.ComboMes.Name = "ComboMes"
        Me.ComboMes.Size = New System.Drawing.Size(125, 36)
        Me.ComboMes.TabIndex = 667
        Me.ComboMes.Text = " "
        Me.ComboMes.ThemeName = "Material"
        '
        'LstAnio
        '
        Me.LstAnio.Location = New System.Drawing.Point(663, 33)
        Me.LstAnio.Name = "LstAnio"
        Me.LstAnio.Size = New System.Drawing.Size(125, 36)
        Me.LstAnio.TabIndex = 666
        Me.LstAnio.Text = " "
        Me.LstAnio.ThemeName = "Material"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(794, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 18)
        Me.Label4.TabIndex = 665
        Me.Label4.Text = "Mes Inicial:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(930, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 18)
        Me.Label2.TabIndex = 664
        Me.Label2.Text = "Mes Final:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(660, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 18)
        Me.Label1.TabIndex = 663
        Me.Label1.Text = "Año:"
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(278, 33)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(370, 36)
        Me.lstCliente.TabIndex = 658
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(274, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 18)
        Me.Label3.TabIndex = 657
        Me.Label3.Text = "Empresa:"
        '
        'CmdCalcular
        '
        Me.CmdCalcular.Image = Global.ATMFiscal.My.Resources.Resources.Calculadora
        Me.CmdCalcular.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdCalcular.Location = New System.Drawing.Point(165, 2)
        Me.CmdCalcular.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdCalcular.Name = "CmdCalcular"
        Me.CmdCalcular.Size = New System.Drawing.Size(50, 54)
        Me.CmdCalcular.TabIndex = 656
        Me.CmdCalcular.TabStop = False
        Me.CmdCalcular.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdCalcular.ThemeName = "Aqua"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(2, 2)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.cmdCerrar.TabIndex = 649
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'CmdGuardar
        '
        Me.CmdGuardar.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdGuardar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdGuardar.Location = New System.Drawing.Point(219, 2)
        Me.CmdGuardar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdGuardar.Name = "CmdGuardar"
        Me.CmdGuardar.Size = New System.Drawing.Size(50, 54)
        Me.CmdGuardar.TabIndex = 655
        Me.CmdGuardar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdGuardar.ThemeName = "Aqua"
        '
        'CmdAgregar
        '
        Me.CmdAgregar.Image = Global.ATMFiscal.My.Resources.Resources.añadir_fila
        Me.CmdAgregar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdAgregar.Location = New System.Drawing.Point(111, 2)
        Me.CmdAgregar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdAgregar.Name = "CmdAgregar"
        Me.CmdAgregar.Size = New System.Drawing.Size(50, 54)
        Me.CmdAgregar.TabIndex = 654
        Me.CmdAgregar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdAgregar.ThemeName = "Aqua"
        '
        'CmdListar
        '
        Me.CmdListar.Image = Global.ATMFiscal.My.Resources.Resources.Reporte
        Me.CmdListar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdListar.Location = New System.Drawing.Point(57, 2)
        Me.CmdListar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdListar.Name = "CmdListar"
        Me.CmdListar.Size = New System.Drawing.Size(50, 54)
        Me.CmdListar.TabIndex = 653
        Me.CmdListar.TabStop = False
        Me.CmdListar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdListar.ThemeName = "Aqua"
        '
        'RadPanel2
        '
        Me.RadPanel2.Controls.Add(Me.TablaVentasActivos)
        Me.RadPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel2.Location = New System.Drawing.Point(0, 100)
        Me.RadPanel2.Name = "RadPanel2"
        Me.RadPanel2.Size = New System.Drawing.Size(1074, 272)
        Me.RadPanel2.TabIndex = 1
        '
        'TablaVentasActivos
        '
        Me.TablaVentasActivos.AllowUserToAddRows = False
        Me.TablaVentasActivos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TablaVentasActivos.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.TablaVentasActivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaVentasActivos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cta, Me.Folio, Me.CtaDepActivo, Me.ResulDep1, Me.ResulDep2, Me.ResulDep3, Me.ResulDep4, Me.Ref, Me.Client, Me.NumFactura, Me.FechaFactura, Me.ImpGF, Me.ImpExF, Me.IvaF, Me.TotalF, Me.FechAdqF, Me.MOIF, Me.DepAcuF, Me.ValorlibrosF, Me.PrecioVenta, Me.UtilidadContable, Me.PerdidaContable, Me.separador, Me.MoiPD, Me.UltMes, Me.FactorUltm, Me.FechaAdq, Me.InpcFecha, Me.FactorActual, Me.CostoFinal, Me.PrecioVentaF, Me.UtilidadFisca, Me.PerdidaFiscal, Me.Tipo, Me.Sumas})
        Me.TablaVentasActivos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaVentasActivos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.TablaVentasActivos.Location = New System.Drawing.Point(0, 0)
        Me.TablaVentasActivos.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaVentasActivos.Name = "TablaVentasActivos"
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TablaVentasActivos.RowsDefaultCellStyle = DataGridViewCellStyle13
        Me.TablaVentasActivos.RowTemplate.Height = 24
        Me.TablaVentasActivos.Size = New System.Drawing.Size(1074, 272)
        Me.TablaVentasActivos.TabIndex = 6
        '
        'cta
        '
        Me.cta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cta.DefaultCellStyle = DataGridViewCellStyle2
        Me.cta.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.cta.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cta.HeaderText = "Cuenta"
        Me.cta.Name = "cta"
        Me.cta.Width = 5
        '
        'Folio
        '
        Me.Folio.HeaderText = "Folio_Fiscal"
        Me.Folio.Name = "Folio"
        '
        'CtaDepActivo
        '
        Me.CtaDepActivo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
        Me.CtaDepActivo.HeaderText = "Activo_Cta_de_Depreciacion"
        Me.CtaDepActivo.Name = "CtaDepActivo"
        Me.CtaDepActivo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CtaDepActivo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CtaDepActivo.Width = 5
        '
        'ResulDep1
        '
        Me.ResulDep1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ResulDep1.HeaderText = "Resultado_Dep_1"
        Me.ResulDep1.Name = "ResulDep1"
        Me.ResulDep1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ResulDep1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ResulDep1.Width = 120
        '
        'ResulDep2
        '
        Me.ResulDep2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ResulDep2.HeaderText = "Resultados_Dep_2"
        Me.ResulDep2.Name = "ResulDep2"
        Me.ResulDep2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ResulDep2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ResulDep2.Width = 126
        '
        'ResulDep3
        '
        Me.ResulDep3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ResulDep3.HeaderText = "Resultado_Dep_3"
        Me.ResulDep3.Name = "ResulDep3"
        Me.ResulDep3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ResulDep3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ResulDep3.Width = 120
        '
        'ResulDep4
        '
        Me.ResulDep4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ResulDep4.HeaderText = "Resultado_Dep_4"
        Me.ResulDep4.Name = "ResulDep4"
        Me.ResulDep4.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ResulDep4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ResulDep4.Width = 120
        '
        'Ref
        '
        Me.Ref.HeaderText = "Referencia"
        Me.Ref.Name = "Ref"
        '
        'Client
        '
        Me.Client.HeaderText = "Cliente"
        Me.Client.Name = "Client"
        '
        'NumFactura
        '
        Me.NumFactura.HeaderText = "Numero de Factura"
        Me.NumFactura.Name = "NumFactura"
        '
        'FechaFactura
        '
        Me.FechaFactura.HeaderText = "Fecha_Factura"
        Me.FechaFactura.Name = "FechaFactura"
        '
        'ImpGF
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = "0"
        Me.ImpGF.DefaultCellStyle = DataGridViewCellStyle3
        Me.ImpGF.HeaderText = "Importe Grabado Facura"
        Me.ImpGF.Name = "ImpGF"
        '
        'ImpExF
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = "0"
        Me.ImpExF.DefaultCellStyle = DataGridViewCellStyle4
        Me.ImpExF.HeaderText = "Importe Exento Factura"
        Me.ImpExF.Name = "ImpExF"
        '
        'IvaF
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = "0"
        Me.IvaF.DefaultCellStyle = DataGridViewCellStyle5
        Me.IvaF.HeaderText = "IVA Factura"
        Me.IvaF.Name = "IvaF"
        '
        'TotalF
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = "0"
        Me.TotalF.DefaultCellStyle = DataGridViewCellStyle6
        Me.TotalF.HeaderText = "Total Factura"
        Me.TotalF.Name = "TotalF"
        '
        'FechAdqF
        '
        Me.FechAdqF.HeaderText = "Fecha Adquisicion"
        Me.FechAdqF.Name = "FechAdqF"
        '
        'MOIF
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N2"
        DataGridViewCellStyle7.NullValue = "0"
        Me.MOIF.DefaultCellStyle = DataGridViewCellStyle7
        Me.MOIF.HeaderText = "MOI"
        Me.MOIF.Name = "MOIF"
        '
        'DepAcuF
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N2"
        DataGridViewCellStyle8.NullValue = "0"
        Me.DepAcuF.DefaultCellStyle = DataGridViewCellStyle8
        Me.DepAcuF.HeaderText = "Depreciacion Acumulada"
        Me.DepAcuF.Name = "DepAcuF"
        '
        'ValorlibrosF
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N2"
        DataGridViewCellStyle9.NullValue = "0"
        Me.ValorlibrosF.DefaultCellStyle = DataGridViewCellStyle9
        Me.ValorlibrosF.HeaderText = "Valor en Libros"
        Me.ValorlibrosF.Name = "ValorlibrosF"
        '
        'PrecioVenta
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "N2"
        DataGridViewCellStyle10.NullValue = "0"
        Me.PrecioVenta.DefaultCellStyle = DataGridViewCellStyle10
        Me.PrecioVenta.HeaderText = "Precio de Venta"
        Me.PrecioVenta.Name = "PrecioVenta"
        '
        'UtilidadContable
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "N2"
        DataGridViewCellStyle11.NullValue = "0"
        Me.UtilidadContable.DefaultCellStyle = DataGridViewCellStyle11
        Me.UtilidadContable.HeaderText = "Utilidad Contable"
        Me.UtilidadContable.Name = "UtilidadContable"
        '
        'PerdidaContable
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle12.Format = "N2"
        DataGridViewCellStyle12.NullValue = "0"
        Me.PerdidaContable.DefaultCellStyle = DataGridViewCellStyle12
        Me.PerdidaContable.HeaderText = "Perdida Contable"
        Me.PerdidaContable.Name = "PerdidaContable"
        '
        'separador
        '
        Me.separador.HeaderText = "Separador"
        Me.separador.Name = "separador"
        '
        'MoiPD
        '
        Me.MoiPD.HeaderText = "MOI Pendiente"
        Me.MoiPD.Name = "MoiPD"
        '
        'UltMes
        '
        Me.UltMes.HeaderText = "Ultimo Mes 1/2"
        Me.UltMes.Name = "UltMes"
        '
        'FactorUltm
        '
        Me.FactorUltm.HeaderText = "Factor Ultimo Mes"
        Me.FactorUltm.Name = "FactorUltm"
        '
        'FechaAdq
        '
        Me.FechaAdq.HeaderText = "Fecha Adquisicion"
        Me.FechaAdq.Name = "FechaAdq"
        '
        'InpcFecha
        '
        Me.InpcFecha.HeaderText = "INPC Fecha Adquisicion"
        Me.InpcFecha.Name = "InpcFecha"
        '
        'FactorActual
        '
        Me.FactorActual.HeaderText = "Factor Actualizacion"
        Me.FactorActual.Name = "FactorActual"
        '
        'CostoFinal
        '
        Me.CostoFinal.HeaderText = "Costo Final"
        Me.CostoFinal.Name = "CostoFinal"
        '
        'PrecioVentaF
        '
        Me.PrecioVentaF.HeaderText = "Precio de Venta"
        Me.PrecioVentaF.Name = "PrecioVentaF"
        '
        'UtilidadFisca
        '
        Me.UtilidadFisca.HeaderText = "Utilidad Fiscal"
        Me.UtilidadFisca.Name = "UtilidadFisca"
        '
        'PerdidaFiscal
        '
        Me.PerdidaFiscal.HeaderText = "Perdida Fiscal"
        Me.PerdidaFiscal.Name = "PerdidaFiscal"
        '
        'Tipo
        '
        Me.Tipo.HeaderText = "TIPO"
        Me.Tipo.Name = "Tipo"
        '
        'Sumas
        '
        Me.Sumas.HeaderText = "Sumas"
        Me.Sumas.Name = "Sumas"
        '
        'TablaResumen
        '
        Me.TablaResumen.AllowUserToAddRows = False
        Me.TablaResumen.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TablaResumen.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle14
        Me.TablaResumen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaResumen.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DescripR, Me.UtilidadC, Me.PerdidaC, Me.UtilidadF, Me.PerdidaF})
        Me.TablaResumen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaResumen.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.TablaResumen.Location = New System.Drawing.Point(0, 372)
        Me.TablaResumen.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaResumen.Name = "TablaResumen"
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TablaResumen.RowsDefaultCellStyle = DataGridViewCellStyle16
        Me.TablaResumen.RowTemplate.Height = 24
        Me.TablaResumen.Size = New System.Drawing.Size(1074, 175)
        Me.TablaResumen.TabIndex = 7
        '
        'DescripR
        '
        Me.DescripR.HeaderText = "Descripcion"
        Me.DescripR.Name = "DescripR"
        '
        'UtilidadC
        '
        Me.UtilidadC.HeaderText = "Utilidad_Contable"
        Me.UtilidadC.Name = "UtilidadC"
        '
        'PerdidaC
        '
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle15.Format = "N2"
        DataGridViewCellStyle15.NullValue = "0"
        Me.PerdidaC.DefaultCellStyle = DataGridViewCellStyle15
        Me.PerdidaC.HeaderText = "Perdida_Contable"
        Me.PerdidaC.Name = "PerdidaC"
        '
        'UtilidadF
        '
        Me.UtilidadF.HeaderText = "Utilidad Fiscal"
        Me.UtilidadF.Name = "UtilidadF"
        '
        'PerdidaF
        '
        Me.PerdidaF.HeaderText = "Perdida Fiscal"
        Me.PerdidaF.Name = "PerdidaF"
        '
        'VentadeActivos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1074, 547)
        Me.Controls.Add(Me.TablaResumen)
        Me.Controls.Add(Me.RadPanel2)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "VentadeActivos"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Venta de Activos"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.ComboMes2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ComboMes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LstAnio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdCalcular, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdGuardar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdAgregar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdListar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel2.ResumeLayout(False)
        CType(Me.TablaVentasActivos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TablaResumen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents Ayuda As ToolTip
	Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
	Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdCalcular As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdGuardar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdAgregar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdListar As Telerik.WinControls.UI.RadButton
	Friend WithEvents lstCliente As Listas
	Friend WithEvents Label3 As Label
	Friend WithEvents ComboMes2 As Telerik.WinControls.UI.RadDropDownList
	Friend WithEvents ComboMes As Telerik.WinControls.UI.RadDropDownList
	Friend WithEvents LstAnio As Telerik.WinControls.UI.RadDropDownList
	Friend WithEvents Label4 As Label
	Friend WithEvents Label2 As Label
	Friend WithEvents Label1 As Label
	Friend WithEvents RadPanel2 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents TablaVentasActivos As DataGridView
	Friend WithEvents cta As DataGridViewComboBoxColumn
	Friend WithEvents Folio As DataGridViewTextBoxColumn
	Friend WithEvents CtaDepActivo As DataGridViewTextBoxColumn
	Friend WithEvents ResulDep1 As DataGridViewTextBoxColumn
	Friend WithEvents ResulDep2 As DataGridViewTextBoxColumn
	Friend WithEvents ResulDep3 As DataGridViewTextBoxColumn
	Friend WithEvents ResulDep4 As DataGridViewTextBoxColumn
	Friend WithEvents Ref As DataGridViewTextBoxColumn
	Friend WithEvents Client As DataGridViewTextBoxColumn
	Friend WithEvents NumFactura As DataGridViewTextBoxColumn
	Friend WithEvents FechaFactura As DataGridViewTextBoxColumn
	Friend WithEvents ImpGF As DataGridViewTextBoxColumn
	Friend WithEvents ImpExF As DataGridViewTextBoxColumn
	Friend WithEvents IvaF As DataGridViewTextBoxColumn
	Friend WithEvents TotalF As DataGridViewTextBoxColumn
	Friend WithEvents FechAdqF As DataGridViewTextBoxColumn
	Friend WithEvents MOIF As DataGridViewTextBoxColumn
	Friend WithEvents DepAcuF As DataGridViewTextBoxColumn
	Friend WithEvents ValorlibrosF As DataGridViewTextBoxColumn
	Friend WithEvents PrecioVenta As DataGridViewTextBoxColumn
	Friend WithEvents UtilidadContable As DataGridViewTextBoxColumn
	Friend WithEvents PerdidaContable As DataGridViewTextBoxColumn
	Friend WithEvents separador As DataGridViewTextBoxColumn
	Friend WithEvents MoiPD As DataGridViewTextBoxColumn
	Friend WithEvents UltMes As DataGridViewTextBoxColumn
	Friend WithEvents FactorUltm As DataGridViewTextBoxColumn
	Friend WithEvents FechaAdq As DataGridViewTextBoxColumn
	Friend WithEvents InpcFecha As DataGridViewTextBoxColumn
	Friend WithEvents FactorActual As DataGridViewTextBoxColumn
	Friend WithEvents CostoFinal As DataGridViewTextBoxColumn
	Friend WithEvents PrecioVentaF As DataGridViewTextBoxColumn
	Friend WithEvents UtilidadFisca As DataGridViewTextBoxColumn
	Friend WithEvents PerdidaFiscal As DataGridViewTextBoxColumn
	Friend WithEvents Tipo As DataGridViewTextBoxColumn
	Friend WithEvents Sumas As DataGridViewTextBoxColumn
	Friend WithEvents TablaResumen As DataGridView
	Friend WithEvents DescripR As DataGridViewTextBoxColumn
	Friend WithEvents UtilidadC As DataGridViewTextBoxColumn
	Friend WithEvents PerdidaC As DataGridViewTextBoxColumn
	Friend WithEvents UtilidadF As DataGridViewTextBoxColumn
	Friend WithEvents PerdidaF As DataGridViewTextBoxColumn
End Class

