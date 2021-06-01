<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Polizas_Modelo_Recibidas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Polizas_Modelo_Recibidas))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TablaFactura = New System.Windows.Forms.DataGridView()
        Me.SelFactura = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.RFCFacturas = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NomFactura = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FantesFacturas = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FDespuesFacturas = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LetraFacturas = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.EfecFacturas = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.TransfFactura = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.BOFactura = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.BDFactura = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChequeFacura = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.BOCHFactura = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.TpolFactura = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ProvFacturas = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.AnticipoFactura = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.IdFac = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.lblfiltroFacturas = New System.Windows.Forms.Label()
        Me.LstFacturas = New Telerik.WinControls.UI.RadTextBox()
        Me.CmdGuardarF = New Telerik.WinControls.UI.RadButton()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.CmdSalirF = New Telerik.WinControls.UI.RadButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CmdEliminarF = New Telerik.WinControls.UI.RadButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CmdLimpiarF = New Telerik.WinControls.UI.RadButton()
        Me.CmdBuscarFact = New Telerik.WinControls.UI.RadButton()
        Me.CmdNuevoF = New Telerik.WinControls.UI.RadButton()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TablaNotasC = New System.Windows.Forms.DataGridView()
        Me.SelNotas = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.RFCNotas = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NombreNotas = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FantesNotas = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaDNotas = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LetrasNotas = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.EfeNotas = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.TransfNotas = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ChequeNotas = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.TPOLNotas = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.PANotas = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.PPNotas = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.IDnOTA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RadPanel2 = New Telerik.WinControls.UI.RadPanel()
        Me.lblNotasFiltro = New System.Windows.Forms.Label()
        Me.LstNotasCredito = New Telerik.WinControls.UI.RadTextBox()
        Me.CmdGuardarN = New Telerik.WinControls.UI.RadButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CmdLimpiarN = New Telerik.WinControls.UI.RadButton()
        Me.CmdEliminarN = New Telerik.WinControls.UI.RadButton()
        Me.CmdNuevoN = New Telerik.WinControls.UI.RadButton()
        Me.CmdBuscarN = New Telerik.WinControls.UI.RadButton()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TablaComplemento = New System.Windows.Forms.DataGridView()
        Me.RFCComplementos = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NombreComplementos = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FantesComplementos = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaDComplementos = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LetraComplementos = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.EfeComplementos = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.TransComplementos = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.BOComplementos = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.BDComplementos = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ChComplementos = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BOCHComplementos = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.TpolComplementos = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.AnticipoComplementos = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.RadPanel3 = New Telerik.WinControls.UI.RadPanel()
        Me.RadTextBox2 = New Telerik.WinControls.UI.RadTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CmdGuardarC = New Telerik.WinControls.UI.RadButton()
        Me.CmdLimpiarC = New Telerik.WinControls.UI.RadButton()
        Me.CmdBuscarC = New Telerik.WinControls.UI.RadButton()
        Me.TxtFiltroComplementos = New Telerik.WinControls.UI.RadButton()
        Me.CmdNuevoC = New Telerik.WinControls.UI.RadButton()
        Me.Imagenes = New System.Windows.Forms.ImageList(Me.components)
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CambiarBancoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.TablaFactura, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.LstFacturas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdGuardarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdEliminarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdLimpiarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdBuscarFact, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdNuevoF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.TablaNotasC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel2.SuspendLayout()
        CType(Me.LstNotasCredito, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdGuardarN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdLimpiarN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdEliminarN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdNuevoN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdBuscarN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.TablaComplemento, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel3.SuspendLayout()
        CType(Me.RadTextBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdGuardarC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdLimpiarC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdBuscarC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFiltroComplementos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdNuevoC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.ImageList = Me.Imagenes
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1384, 585)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TablaFactura)
        Me.TabPage1.Controls.Add(Me.RadPanel1)
        Me.TabPage1.ImageIndex = 0
        Me.TabPage1.Location = New System.Drawing.Point(4, 39)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1376, 542)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Facturas"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TablaFactura
        '
        Me.TablaFactura.AllowUserToAddRows = False
        Me.TablaFactura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaFactura.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SelFactura, Me.RFCFacturas, Me.NomFactura, Me.FantesFacturas, Me.FDespuesFacturas, Me.LetraFacturas, Me.EfecFacturas, Me.TransfFactura, Me.BOFactura, Me.BDFactura, Me.ChequeFacura, Me.BOCHFactura, Me.TpolFactura, Me.ProvFacturas, Me.AnticipoFactura, Me.IdFac})
        Me.TablaFactura.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaFactura.Location = New System.Drawing.Point(3, 92)
        Me.TablaFactura.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaFactura.Name = "TablaFactura"
        Me.TablaFactura.RowTemplate.Height = 24
        Me.TablaFactura.Size = New System.Drawing.Size(1370, 447)
        Me.TablaFactura.TabIndex = 3
        '
        'SelFactura
        '
        Me.SelFactura.HeaderText = "Selecciona"
        Me.SelFactura.Name = "SelFactura"
        Me.SelFactura.ReadOnly = True
        '
        'RFCFacturas
        '
        Me.RFCFacturas.HeaderText = "RFC"
        Me.RFCFacturas.Name = "RFCFacturas"
        '
        'NomFactura
        '
        Me.NomFactura.HeaderText = "Nombre"
        Me.NomFactura.Name = "NomFactura"
        '
        'FantesFacturas
        '
        Me.FantesFacturas.HeaderText = "Dia_Antes_de"
        Me.FantesFacturas.Name = "FantesFacturas"
        '
        'FDespuesFacturas
        '
        Me.FDespuesFacturas.HeaderText = "Dia_Despues_de"
        Me.FDespuesFacturas.Name = "FDespuesFacturas"
        '
        'LetraFacturas
        '
        Me.LetraFacturas.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.LetraFacturas.HeaderText = "Letra_Contabilizar"
        Me.LetraFacturas.Name = "LetraFacturas"
        Me.LetraFacturas.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.LetraFacturas.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'EfecFacturas
        '
        Me.EfecFacturas.HeaderText = "Efectivo"
        Me.EfecFacturas.Name = "EfecFacturas"
        Me.EfecFacturas.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.EfecFacturas.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'TransfFactura
        '
        Me.TransfFactura.HeaderText = "Transferencia"
        Me.TransfFactura.Name = "TransfFactura"
        Me.TransfFactura.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TransfFactura.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'BOFactura
        '
        Me.BOFactura.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.BOFactura.HeaderText = "Banco_Origen"
        Me.BOFactura.Name = "BOFactura"
        Me.BOFactura.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BOFactura.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'BDFactura
        '
        Me.BDFactura.HeaderText = "Banco_Destino"
        Me.BDFactura.Name = "BDFactura"
        Me.BDFactura.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'ChequeFacura
        '
        Me.ChequeFacura.HeaderText = "Cheque"
        Me.ChequeFacura.Name = "ChequeFacura"
        Me.ChequeFacura.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ChequeFacura.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'BOCHFactura
        '
        Me.BOCHFactura.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.BOCHFactura.HeaderText = "Banco_Origen_Cheque"
        Me.BOCHFactura.Name = "BOCHFactura"
        Me.BOCHFactura.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BOCHFactura.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'TpolFactura
        '
        Me.TpolFactura.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.TpolFactura.HeaderText = "Tipo_Poliza"
        Me.TpolFactura.Name = "TpolFactura"
        Me.TpolFactura.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TpolFactura.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'ProvFacturas
        '
        Me.ProvFacturas.HeaderText = "Provision"
        Me.ProvFacturas.Name = "ProvFacturas"
        Me.ProvFacturas.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ProvFacturas.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'AnticipoFactura
        '
        Me.AnticipoFactura.HeaderText = "Anticipo"
        Me.AnticipoFactura.Name = "AnticipoFactura"
        Me.AnticipoFactura.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AnticipoFactura.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'IdFac
        '
        Me.IdFac.HeaderText = "Id"
        Me.IdFac.Name = "IdFac"
        Me.IdFac.Visible = False
        '
        'RadPanel1
        '
        Me.RadPanel1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadPanel1.Controls.Add(Me.lblfiltroFacturas)
        Me.RadPanel1.Controls.Add(Me.LstFacturas)
        Me.RadPanel1.Controls.Add(Me.CmdGuardarF)
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.CmdSalirF)
        Me.RadPanel1.Controls.Add(Me.Label1)
        Me.RadPanel1.Controls.Add(Me.CmdEliminarF)
        Me.RadPanel1.Controls.Add(Me.Label2)
        Me.RadPanel1.Controls.Add(Me.CmdLimpiarF)
        Me.RadPanel1.Controls.Add(Me.CmdBuscarFact)
        Me.RadPanel1.Controls.Add(Me.CmdNuevoF)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(3, 3)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1370, 89)
        Me.RadPanel1.TabIndex = 0
        '
        'lblfiltroFacturas
        '
        Me.lblfiltroFacturas.AutoSize = True
        Me.lblfiltroFacturas.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblfiltroFacturas.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfiltroFacturas.Location = New System.Drawing.Point(1169, 36)
        Me.lblfiltroFacturas.Name = "lblfiltroFacturas"
        Me.lblfiltroFacturas.Size = New System.Drawing.Size(0, 18)
        Me.lblfiltroFacturas.TabIndex = 681
        '
        'LstFacturas
        '
        Me.LstFacturas.AutoSize = False
        Me.LstFacturas.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstFacturas.Location = New System.Drawing.Point(800, 30)
        Me.LstFacturas.Name = "LstFacturas"
        Me.LstFacturas.Size = New System.Drawing.Size(317, 36)
        Me.LstFacturas.TabIndex = 521
        Me.LstFacturas.ThemeName = "Material"
        '
        'CmdGuardarF
        '
        Me.CmdGuardarF.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdGuardarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdGuardarF.Location = New System.Drawing.Point(216, 14)
        Me.CmdGuardarF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdGuardarF.Name = "CmdGuardarF"
        Me.CmdGuardarF.Size = New System.Drawing.Size(50, 54)
        Me.CmdGuardarF.TabIndex = 520
        Me.CmdGuardarF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdGuardarF.ThemeName = "Aqua"
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(340, 30)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(423, 36)
        Me.lstCliente.TabIndex = 520
        '
        'CmdSalirF
        '
        Me.CmdSalirF.Image = CType(resources.GetObject("CmdSalirF.Image"), System.Drawing.Image)
        Me.CmdSalirF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdSalirF.Location = New System.Drawing.Point(4, 14)
        Me.CmdSalirF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdSalirF.Name = "CmdSalirF"
        Me.CmdSalirF.Size = New System.Drawing.Size(50, 54)
        Me.CmdSalirF.TabIndex = 515
        Me.CmdSalirF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdSalirF.ThemeName = "Aqua"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(797, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 18)
        Me.Label1.TabIndex = 518
        Me.Label1.Text = "Filtrar:"
        '
        'CmdEliminarF
        '
        Me.CmdEliminarF.Image = Global.ATMFiscal.My.Resources.Resources.Eliminar
        Me.CmdEliminarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdEliminarF.Location = New System.Drawing.Point(269, 14)
        Me.CmdEliminarF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdEliminarF.Name = "CmdEliminarF"
        Me.CmdEliminarF.Size = New System.Drawing.Size(50, 54)
        Me.CmdEliminarF.TabIndex = 517
        Me.CmdEliminarF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdEliminarF.ThemeName = "Aqua"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(336, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 18)
        Me.Label2.TabIndex = 519
        Me.Label2.Text = "Empresa:"
        '
        'CmdLimpiarF
        '
        Me.CmdLimpiarF.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdLimpiarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdLimpiarF.Location = New System.Drawing.Point(57, 14)
        Me.CmdLimpiarF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdLimpiarF.Name = "CmdLimpiarF"
        Me.CmdLimpiarF.Size = New System.Drawing.Size(50, 54)
        Me.CmdLimpiarF.TabIndex = 519
        Me.CmdLimpiarF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdLimpiarF.ThemeName = "Aqua"
        '
        'CmdBuscarFact
        '
        Me.CmdBuscarFact.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdBuscarFact.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdBuscarFact.Location = New System.Drawing.Point(110, 14)
        Me.CmdBuscarFact.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdBuscarFact.Name = "CmdBuscarFact"
        Me.CmdBuscarFact.Size = New System.Drawing.Size(50, 54)
        Me.CmdBuscarFact.TabIndex = 516
        Me.CmdBuscarFact.TabStop = False
        Me.CmdBuscarFact.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdBuscarFact.ThemeName = "Aqua"
        '
        'CmdNuevoF
        '
        Me.CmdNuevoF.Image = Global.ATMFiscal.My.Resources.Resources.Nuevo
        Me.CmdNuevoF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdNuevoF.Location = New System.Drawing.Point(163, 14)
        Me.CmdNuevoF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdNuevoF.Name = "CmdNuevoF"
        Me.CmdNuevoF.Size = New System.Drawing.Size(50, 54)
        Me.CmdNuevoF.TabIndex = 518
        Me.CmdNuevoF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdNuevoF.ThemeName = "Aqua"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TablaNotasC)
        Me.TabPage2.Controls.Add(Me.RadPanel2)
        Me.TabPage2.ImageIndex = 0
        Me.TabPage2.Location = New System.Drawing.Point(4, 39)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1376, 542)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Notas de Crédito"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TablaNotasC
        '
        Me.TablaNotasC.AllowUserToAddRows = False
        Me.TablaNotasC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaNotasC.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SelNotas, Me.RFCNotas, Me.NombreNotas, Me.FantesNotas, Me.FechaDNotas, Me.LetrasNotas, Me.EfeNotas, Me.TransfNotas, Me.ChequeNotas, Me.TPOLNotas, Me.PANotas, Me.PPNotas, Me.IDnOTA})
        Me.TablaNotasC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaNotasC.Location = New System.Drawing.Point(3, 98)
        Me.TablaNotasC.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaNotasC.Name = "TablaNotasC"
        Me.TablaNotasC.RowTemplate.Height = 24
        Me.TablaNotasC.Size = New System.Drawing.Size(1370, 441)
        Me.TablaNotasC.TabIndex = 4
        '
        'SelNotas
        '
        Me.SelNotas.HeaderText = "Hacer"
        Me.SelNotas.Name = "SelNotas"
        Me.SelNotas.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SelNotas.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'RFCNotas
        '
        Me.RFCNotas.HeaderText = "RFC"
        Me.RFCNotas.Name = "RFCNotas"
        '
        'NombreNotas
        '
        Me.NombreNotas.HeaderText = "Nombre"
        Me.NombreNotas.Name = "NombreNotas"
        '
        'FantesNotas
        '
        Me.FantesNotas.HeaderText = "Fecha_Antes_de"
        Me.FantesNotas.Name = "FantesNotas"
        '
        'FechaDNotas
        '
        Me.FechaDNotas.HeaderText = "Fecha_Despues_de"
        Me.FechaDNotas.Name = "FechaDNotas"
        '
        'LetrasNotas
        '
        Me.LetrasNotas.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.LetrasNotas.HeaderText = "Letra_Contabilizar"
        Me.LetrasNotas.Name = "LetrasNotas"
        Me.LetrasNotas.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.LetrasNotas.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'EfeNotas
        '
        Me.EfeNotas.HeaderText = "Efectivo"
        Me.EfeNotas.Name = "EfeNotas"
        Me.EfeNotas.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.EfeNotas.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'TransfNotas
        '
        Me.TransfNotas.HeaderText = "Transferencia"
        Me.TransfNotas.Name = "TransfNotas"
        Me.TransfNotas.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TransfNotas.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'ChequeNotas
        '
        Me.ChequeNotas.HeaderText = "Cheque"
        Me.ChequeNotas.Name = "ChequeNotas"
        Me.ChequeNotas.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ChequeNotas.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'TPOLNotas
        '
        Me.TPOLNotas.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.TPOLNotas.HeaderText = "Tipo_Poliza"
        Me.TPOLNotas.Name = "TPOLNotas"
        Me.TPOLNotas.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TPOLNotas.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'PANotas
        '
        Me.PANotas.HeaderText = "Provision_Acred"
        Me.PANotas.Name = "PANotas"
        Me.PANotas.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PANotas.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'PPNotas
        '
        Me.PPNotas.HeaderText = "Provision_Provee"
        Me.PPNotas.Name = "PPNotas"
        Me.PPNotas.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PPNotas.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'IDnOTA
        '
        Me.IDnOTA.HeaderText = "id"
        Me.IDnOTA.Name = "IDnOTA"
        '
        'RadPanel2
        '
        Me.RadPanel2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadPanel2.Controls.Add(Me.lblNotasFiltro)
        Me.RadPanel2.Controls.Add(Me.LstNotasCredito)
        Me.RadPanel2.Controls.Add(Me.CmdGuardarN)
        Me.RadPanel2.Controls.Add(Me.Label3)
        Me.RadPanel2.Controls.Add(Me.CmdLimpiarN)
        Me.RadPanel2.Controls.Add(Me.CmdEliminarN)
        Me.RadPanel2.Controls.Add(Me.CmdNuevoN)
        Me.RadPanel2.Controls.Add(Me.CmdBuscarN)
        Me.RadPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel2.Location = New System.Drawing.Point(3, 3)
        Me.RadPanel2.Name = "RadPanel2"
        Me.RadPanel2.Size = New System.Drawing.Size(1370, 95)
        Me.RadPanel2.TabIndex = 0
        '
        'lblNotasFiltro
        '
        Me.lblNotasFiltro.AutoSize = True
        Me.lblNotasFiltro.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblNotasFiltro.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotasFiltro.Location = New System.Drawing.Point(862, 46)
        Me.lblNotasFiltro.Name = "lblNotasFiltro"
        Me.lblNotasFiltro.Size = New System.Drawing.Size(0, 18)
        Me.lblNotasFiltro.TabIndex = 666
        '
        'LstNotasCredito
        '
        Me.LstNotasCredito.AutoSize = False
        Me.LstNotasCredito.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstNotasCredito.Location = New System.Drawing.Point(340, 38)
        Me.LstNotasCredito.Name = "LstNotasCredito"
        Me.LstNotasCredito.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.LstNotasCredito.Size = New System.Drawing.Size(434, 36)
        Me.LstNotasCredito.TabIndex = 522
        Me.LstNotasCredito.ThemeName = "Material"
        '
        'CmdGuardarN
        '
        Me.CmdGuardarN.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdGuardarN.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdGuardarN.Location = New System.Drawing.Point(163, 2)
        Me.CmdGuardarN.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdGuardarN.Name = "CmdGuardarN"
        Me.CmdGuardarN.Size = New System.Drawing.Size(50, 54)
        Me.CmdGuardarN.TabIndex = 525
        Me.CmdGuardarN.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdGuardarN.ThemeName = "Aqua"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(337, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 18)
        Me.Label3.TabIndex = 518
        Me.Label3.Text = "Filtrar:"
        '
        'CmdLimpiarN
        '
        Me.CmdLimpiarN.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdLimpiarN.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdLimpiarN.Location = New System.Drawing.Point(4, 2)
        Me.CmdLimpiarN.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdLimpiarN.Name = "CmdLimpiarN"
        Me.CmdLimpiarN.Size = New System.Drawing.Size(50, 54)
        Me.CmdLimpiarN.TabIndex = 524
        Me.CmdLimpiarN.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdLimpiarN.ThemeName = "Aqua"
        '
        'CmdEliminarN
        '
        Me.CmdEliminarN.Image = Global.ATMFiscal.My.Resources.Resources.Eliminar
        Me.CmdEliminarN.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdEliminarN.Location = New System.Drawing.Point(216, 2)
        Me.CmdEliminarN.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdEliminarN.Name = "CmdEliminarN"
        Me.CmdEliminarN.Size = New System.Drawing.Size(50, 54)
        Me.CmdEliminarN.TabIndex = 522
        Me.CmdEliminarN.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdEliminarN.ThemeName = "Aqua"
        '
        'CmdNuevoN
        '
        Me.CmdNuevoN.Image = Global.ATMFiscal.My.Resources.Resources.Nuevo
        Me.CmdNuevoN.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdNuevoN.Location = New System.Drawing.Point(110, 2)
        Me.CmdNuevoN.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdNuevoN.Name = "CmdNuevoN"
        Me.CmdNuevoN.Size = New System.Drawing.Size(50, 54)
        Me.CmdNuevoN.TabIndex = 523
        Me.CmdNuevoN.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdNuevoN.ThemeName = "Aqua"
        '
        'CmdBuscarN
        '
        Me.CmdBuscarN.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdBuscarN.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdBuscarN.Location = New System.Drawing.Point(57, 2)
        Me.CmdBuscarN.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdBuscarN.Name = "CmdBuscarN"
        Me.CmdBuscarN.Size = New System.Drawing.Size(50, 54)
        Me.CmdBuscarN.TabIndex = 521
        Me.CmdBuscarN.TabStop = False
        Me.CmdBuscarN.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdBuscarN.ThemeName = "Aqua"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.TablaComplemento)
        Me.TabPage3.Controls.Add(Me.RadPanel3)
        Me.TabPage3.ImageIndex = 0
        Me.TabPage3.Location = New System.Drawing.Point(4, 39)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(1376, 542)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Complementos de Pago"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TablaComplemento
        '
        Me.TablaComplemento.AllowUserToAddRows = False
        Me.TablaComplemento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaComplemento.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RFCComplementos, Me.NombreComplementos, Me.FantesComplementos, Me.FechaDComplementos, Me.LetraComplementos, Me.EfeComplementos, Me.TransComplementos, Me.BOComplementos, Me.BDComplementos, Me.ChComplementos, Me.BOCHComplementos, Me.TpolComplementos, Me.AnticipoComplementos})
        Me.TablaComplemento.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaComplemento.Location = New System.Drawing.Point(0, 82)
        Me.TablaComplemento.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaComplemento.Name = "TablaComplemento"
        Me.TablaComplemento.RowTemplate.Height = 24
        Me.TablaComplemento.Size = New System.Drawing.Size(1376, 460)
        Me.TablaComplemento.TabIndex = 4
        '
        'RFCComplementos
        '
        Me.RFCComplementos.HeaderText = "RFC"
        Me.RFCComplementos.Name = "RFCComplementos"
        '
        'NombreComplementos
        '
        Me.NombreComplementos.HeaderText = "Nombre"
        Me.NombreComplementos.Name = "NombreComplementos"
        '
        'FantesComplementos
        '
        Me.FantesComplementos.HeaderText = "Fecha_Antes_de"
        Me.FantesComplementos.Name = "FantesComplementos"
        '
        'FechaDComplementos
        '
        Me.FechaDComplementos.HeaderText = "Fecha_Despues_de"
        Me.FechaDComplementos.Name = "FechaDComplementos"
        '
        'LetraComplementos
        '
        Me.LetraComplementos.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.LetraComplementos.HeaderText = "Letra_Contabilizar"
        Me.LetraComplementos.Name = "LetraComplementos"
        Me.LetraComplementos.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.LetraComplementos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'EfeComplementos
        '
        Me.EfeComplementos.HeaderText = "Efectivo"
        Me.EfeComplementos.Name = "EfeComplementos"
        Me.EfeComplementos.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.EfeComplementos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'TransComplementos
        '
        Me.TransComplementos.HeaderText = "Transferencia"
        Me.TransComplementos.Name = "TransComplementos"
        Me.TransComplementos.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TransComplementos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'BOComplementos
        '
        Me.BOComplementos.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.BOComplementos.HeaderText = "Banco_Origen"
        Me.BOComplementos.Name = "BOComplementos"
        Me.BOComplementos.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BOComplementos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'BDComplementos
        '
        Me.BDComplementos.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.BDComplementos.HeaderText = "Banco_Destino"
        Me.BDComplementos.Name = "BDComplementos"
        Me.BDComplementos.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BDComplementos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'ChComplementos
        '
        Me.ChComplementos.HeaderText = "Cheque"
        Me.ChComplementos.Name = "ChComplementos"
        '
        'BOCHComplementos
        '
        Me.BOCHComplementos.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.BOCHComplementos.HeaderText = "Banco_Origen_Cheque"
        Me.BOCHComplementos.Name = "BOCHComplementos"
        Me.BOCHComplementos.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BOCHComplementos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'TpolComplementos
        '
        Me.TpolComplementos.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.TpolComplementos.HeaderText = "Tipo_Poliza"
        Me.TpolComplementos.Name = "TpolComplementos"
        Me.TpolComplementos.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TpolComplementos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'AnticipoComplementos
        '
        Me.AnticipoComplementos.HeaderText = "Anticipo"
        Me.AnticipoComplementos.Name = "AnticipoComplementos"
        Me.AnticipoComplementos.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AnticipoComplementos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'RadPanel3
        '
        Me.RadPanel3.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadPanel3.Controls.Add(Me.RadTextBox2)
        Me.RadPanel3.Controls.Add(Me.Label4)
        Me.RadPanel3.Controls.Add(Me.CmdGuardarC)
        Me.RadPanel3.Controls.Add(Me.CmdLimpiarC)
        Me.RadPanel3.Controls.Add(Me.CmdBuscarC)
        Me.RadPanel3.Controls.Add(Me.TxtFiltroComplementos)
        Me.RadPanel3.Controls.Add(Me.CmdNuevoC)
        Me.RadPanel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel3.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel3.Name = "RadPanel3"
        Me.RadPanel3.Size = New System.Drawing.Size(1376, 82)
        Me.RadPanel3.TabIndex = 0
        '
        'RadTextBox2
        '
        Me.RadTextBox2.AutoSize = False
        Me.RadTextBox2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadTextBox2.Location = New System.Drawing.Point(333, 24)
        Me.RadTextBox2.Name = "RadTextBox2"
        Me.RadTextBox2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.RadTextBox2.Size = New System.Drawing.Size(434, 36)
        Me.RadTextBox2.TabIndex = 524
        Me.RadTextBox2.ThemeName = "Material"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(330, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 18)
        Me.Label4.TabIndex = 523
        Me.Label4.Text = "Filtrar:"
        '
        'CmdGuardarC
        '
        Me.CmdGuardarC.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdGuardarC.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdGuardarC.Location = New System.Drawing.Point(166, 2)
        Me.CmdGuardarC.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdGuardarC.Name = "CmdGuardarC"
        Me.CmdGuardarC.Size = New System.Drawing.Size(50, 54)
        Me.CmdGuardarC.TabIndex = 530
        Me.CmdGuardarC.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdGuardarC.ThemeName = "Aqua"
        '
        'CmdLimpiarC
        '
        Me.CmdLimpiarC.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdLimpiarC.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdLimpiarC.Location = New System.Drawing.Point(7, 2)
        Me.CmdLimpiarC.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdLimpiarC.Name = "CmdLimpiarC"
        Me.CmdLimpiarC.Size = New System.Drawing.Size(50, 54)
        Me.CmdLimpiarC.TabIndex = 529
        Me.CmdLimpiarC.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdLimpiarC.ThemeName = "Aqua"
        '
        'CmdBuscarC
        '
        Me.CmdBuscarC.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdBuscarC.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdBuscarC.Location = New System.Drawing.Point(60, 2)
        Me.CmdBuscarC.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdBuscarC.Name = "CmdBuscarC"
        Me.CmdBuscarC.Size = New System.Drawing.Size(50, 54)
        Me.CmdBuscarC.TabIndex = 526
        Me.CmdBuscarC.TabStop = False
        Me.CmdBuscarC.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdBuscarC.ThemeName = "Aqua"
        '
        'TxtFiltroComplementos
        '
        Me.TxtFiltroComplementos.Image = Global.ATMFiscal.My.Resources.Resources.Eliminar
        Me.TxtFiltroComplementos.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.TxtFiltroComplementos.Location = New System.Drawing.Point(219, 2)
        Me.TxtFiltroComplementos.Margin = New System.Windows.Forms.Padding(2)
        Me.TxtFiltroComplementos.Name = "TxtFiltroComplementos"
        Me.TxtFiltroComplementos.Size = New System.Drawing.Size(50, 54)
        Me.TxtFiltroComplementos.TabIndex = 527
        Me.TxtFiltroComplementos.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.TxtFiltroComplementos.ThemeName = "Aqua"
        '
        'CmdNuevoC
        '
        Me.CmdNuevoC.Image = Global.ATMFiscal.My.Resources.Resources.Nuevo
        Me.CmdNuevoC.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdNuevoC.Location = New System.Drawing.Point(113, 2)
        Me.CmdNuevoC.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdNuevoC.Name = "CmdNuevoC"
        Me.CmdNuevoC.Size = New System.Drawing.Size(50, 54)
        Me.CmdNuevoC.TabIndex = 528
        Me.CmdNuevoC.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdNuevoC.ThemeName = "Aqua"
        '
        'Imagenes
        '
        Me.Imagenes.ImageStream = CType(resources.GetObject("Imagenes.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Imagenes.TransparentColor = System.Drawing.Color.Transparent
        Me.Imagenes.Images.SetKeyName(0, "XmlImportar.ico")
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CambiarBancoToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(180, 28)
        '
        'CambiarBancoToolStripMenuItem
        '
        Me.CambiarBancoToolStripMenuItem.Name = "CambiarBancoToolStripMenuItem"
        Me.CambiarBancoToolStripMenuItem.Size = New System.Drawing.Size(179, 24)
        Me.CambiarBancoToolStripMenuItem.Text = "Cambiar Banco"
        '
        'Polizas_Modelo_Recibidas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.CadetBlue
        Me.ClientSize = New System.Drawing.Size(1384, 585)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Polizas_Modelo_Recibidas"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pre-Cargas Recibidas"
        Me.ThemeName = "MaterialBlueGrey"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.TablaFactura, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.LstFacturas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdGuardarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdEliminarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdLimpiarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdBuscarFact, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdNuevoF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.TablaNotasC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel2.ResumeLayout(False)
        Me.RadPanel2.PerformLayout()
        CType(Me.LstNotasCredito, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdGuardarN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdLimpiarN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdEliminarN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdNuevoN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdBuscarN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.TablaComplemento, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel3.ResumeLayout(False)
        Me.RadPanel3.PerformLayout()
        CType(Me.RadTextBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdGuardarC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdLimpiarC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdBuscarC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFiltroComplementos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdNuevoC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents TabControl1 As TabControl
	Friend WithEvents TabPage1 As TabPage
	Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents TabPage2 As TabPage
	Friend WithEvents CmdGuardarF As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdSalirF As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdEliminarF As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdLimpiarF As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdBuscarFact As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdNuevoF As Telerik.WinControls.UI.RadButton
	Friend WithEvents TablaFactura As DataGridView
	Friend WithEvents SelFactura As DataGridViewCheckBoxColumn
	Friend WithEvents RFCFacturas As DataGridViewTextBoxColumn
	Friend WithEvents NomFactura As DataGridViewTextBoxColumn
	Friend WithEvents FantesFacturas As DataGridViewTextBoxColumn
	Friend WithEvents FDespuesFacturas As DataGridViewTextBoxColumn
	Friend WithEvents LetraFacturas As DataGridViewComboBoxColumn
	Friend WithEvents EfecFacturas As DataGridViewCheckBoxColumn
	Friend WithEvents TransfFactura As DataGridViewCheckBoxColumn
	Friend WithEvents BOFactura As DataGridViewComboBoxColumn
	Friend WithEvents BDFactura As DataGridViewTextBoxColumn
	Friend WithEvents ChequeFacura As DataGridViewCheckBoxColumn
	Friend WithEvents BOCHFactura As DataGridViewComboBoxColumn
	Friend WithEvents TpolFactura As DataGridViewComboBoxColumn
	Friend WithEvents ProvFacturas As DataGridViewCheckBoxColumn
	Friend WithEvents AnticipoFactura As DataGridViewCheckBoxColumn
	Friend WithEvents IdFac As DataGridViewTextBoxColumn
	Friend WithEvents LstFacturas As Telerik.WinControls.UI.RadTextBox
	Friend WithEvents lstCliente As Listas
	Friend WithEvents Label1 As Label
	Friend WithEvents Label2 As Label
	Friend WithEvents RadPanel2 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents CmdGuardarN As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdLimpiarN As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdEliminarN As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdNuevoN As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdBuscarN As Telerik.WinControls.UI.RadButton
	Friend WithEvents TablaNotasC As DataGridView
	Friend WithEvents SelNotas As DataGridViewCheckBoxColumn
	Friend WithEvents RFCNotas As DataGridViewTextBoxColumn
	Friend WithEvents NombreNotas As DataGridViewTextBoxColumn
	Friend WithEvents FantesNotas As DataGridViewTextBoxColumn
	Friend WithEvents FechaDNotas As DataGridViewTextBoxColumn
	Friend WithEvents LetrasNotas As DataGridViewComboBoxColumn
	Friend WithEvents EfeNotas As DataGridViewCheckBoxColumn
	Friend WithEvents TransfNotas As DataGridViewCheckBoxColumn
	Friend WithEvents ChequeNotas As DataGridViewCheckBoxColumn
	Friend WithEvents TPOLNotas As DataGridViewComboBoxColumn
	Friend WithEvents PANotas As DataGridViewCheckBoxColumn
	Friend WithEvents PPNotas As DataGridViewCheckBoxColumn
	Friend WithEvents IDnOTA As DataGridViewTextBoxColumn
	Friend WithEvents LstNotasCredito As Telerik.WinControls.UI.RadTextBox
	Friend WithEvents Label3 As Label
	Friend WithEvents TabPage3 As TabPage
	Friend WithEvents TablaComplemento As DataGridView
	Friend WithEvents RFCComplementos As DataGridViewTextBoxColumn
	Friend WithEvents NombreComplementos As DataGridViewTextBoxColumn
	Friend WithEvents FantesComplementos As DataGridViewTextBoxColumn
	Friend WithEvents FechaDComplementos As DataGridViewTextBoxColumn
	Friend WithEvents LetraComplementos As DataGridViewComboBoxColumn
	Friend WithEvents EfeComplementos As DataGridViewCheckBoxColumn
	Friend WithEvents TransComplementos As DataGridViewCheckBoxColumn
	Friend WithEvents BOComplementos As DataGridViewComboBoxColumn
	Friend WithEvents BDComplementos As DataGridViewComboBoxColumn
	Friend WithEvents ChComplementos As DataGridViewTextBoxColumn
	Friend WithEvents BOCHComplementos As DataGridViewComboBoxColumn
	Friend WithEvents TpolComplementos As DataGridViewComboBoxColumn
	Friend WithEvents AnticipoComplementos As DataGridViewCheckBoxColumn
	Friend WithEvents RadPanel3 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents RadTextBox2 As Telerik.WinControls.UI.RadTextBox
	Friend WithEvents Label4 As Label
	Friend WithEvents CmdGuardarC As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdLimpiarC As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdBuscarC As Telerik.WinControls.UI.RadButton
	Friend WithEvents TxtFiltroComplementos As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdNuevoC As Telerik.WinControls.UI.RadButton
	Friend WithEvents Imagenes As ImageList
    Friend WithEvents lblfiltroFacturas As Label
    Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents CambiarBancoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents lblNotasFiltro As Label
End Class

