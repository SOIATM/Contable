<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Ingresos_Series
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Ingresos_Series))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.BarraF = New Telerik.WinControls.UI.RadProgressBar()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.CmdEliminarF = New Telerik.WinControls.UI.RadButton()
        Me.CmdGuardarF = New Telerik.WinControls.UI.RadButton()
        Me.CmdNuevoF = New Telerik.WinControls.UI.RadButton()
        Me.CmdBuscarFact = New Telerik.WinControls.UI.RadButton()
        Me.CmdLimpiarF = New Telerik.WinControls.UI.RadButton()
        Me.CmdSalirF = New Telerik.WinControls.UI.RadButton()
        Me.RadPanel2 = New Telerik.WinControls.UI.RadPanel()
        Me.TablaSeries = New System.Windows.Forms.DataGridView()
        Me.SelFactura = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Serie = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Abono = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.CtaIngG = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.CtaIngEx = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.CtaIngC = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.IVATras = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ISRRet = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.IVARet = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.CtaIngPCG = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.CtaIngPCE = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.CtaIngPCC = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.IVAPTras = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ISRRPA = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.IVARetPA = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Clientes = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.AnticipoClientes = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.IvaClientes = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.IvaCancelClientes = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DevSVentasG = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DevSVentasC = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DevSVentasEx = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.IvaSDev = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.BarraF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdEliminarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdGuardarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdNuevoF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdBuscarFact, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdLimpiarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel2.SuspendLayout()
        CType(Me.TablaSeries, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadPanel1.Controls.Add(Me.BarraF)
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label12)
        Me.RadPanel1.Controls.Add(Me.CmdEliminarF)
        Me.RadPanel1.Controls.Add(Me.CmdGuardarF)
        Me.RadPanel1.Controls.Add(Me.CmdNuevoF)
        Me.RadPanel1.Controls.Add(Me.CmdBuscarFact)
        Me.RadPanel1.Controls.Add(Me.CmdLimpiarF)
        Me.RadPanel1.Controls.Add(Me.CmdSalirF)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1401, 144)
        Me.RadPanel1.TabIndex = 0
        '
        'BarraF
        '
        Me.BarraF.Location = New System.Drawing.Point(8, 94)
        Me.BarraF.Name = "BarraF"
        Me.BarraF.Size = New System.Drawing.Size(442, 45)
        Me.BarraF.TabIndex = 689
        Me.BarraF.ThemeName = "Material"
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(479, 39)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(614, 49)
        Me.lstCliente.TabIndex = 688
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(475, 15)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 18)
        Me.Label12.TabIndex = 687
        Me.Label12.Text = "Empresa:"
        '
        'CmdEliminarF
        '
        Me.CmdEliminarF.Image = Global.ATMFiscal.My.Resources.Resources.Eliminar
        Me.CmdEliminarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdEliminarF.Location = New System.Drawing.Point(383, 21)
        Me.CmdEliminarF.Name = "CmdEliminarF"
        Me.CmdEliminarF.Size = New System.Drawing.Size(67, 67)
        Me.CmdEliminarF.TabIndex = 6
        Me.CmdEliminarF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdEliminarF.ThemeName = "Aqua"
        '
        'CmdGuardarF
        '
        Me.CmdGuardarF.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdGuardarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdGuardarF.Location = New System.Drawing.Point(308, 21)
        Me.CmdGuardarF.Name = "CmdGuardarF"
        Me.CmdGuardarF.Size = New System.Drawing.Size(67, 67)
        Me.CmdGuardarF.TabIndex = 5
        Me.CmdGuardarF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdGuardarF.ThemeName = "Aqua"
        '
        'CmdNuevoF
        '
        Me.CmdNuevoF.Image = Global.ATMFiscal.My.Resources.Resources.Nuevo
        Me.CmdNuevoF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdNuevoF.Location = New System.Drawing.Point(233, 21)
        Me.CmdNuevoF.Name = "CmdNuevoF"
        Me.CmdNuevoF.Size = New System.Drawing.Size(67, 67)
        Me.CmdNuevoF.TabIndex = 4
        Me.CmdNuevoF.TabStop = False
        Me.CmdNuevoF.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdNuevoF.ThemeName = "Aqua"
        '
        'CmdBuscarFact
        '
        Me.CmdBuscarFact.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdBuscarFact.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdBuscarFact.Location = New System.Drawing.Point(158, 21)
        Me.CmdBuscarFact.Name = "CmdBuscarFact"
        Me.CmdBuscarFact.Size = New System.Drawing.Size(67, 67)
        Me.CmdBuscarFact.TabIndex = 3
        Me.CmdBuscarFact.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdBuscarFact.ThemeName = "Aqua"
        '
        'CmdLimpiarF
        '
        Me.CmdLimpiarF.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdLimpiarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdLimpiarF.Location = New System.Drawing.Point(83, 21)
        Me.CmdLimpiarF.Name = "CmdLimpiarF"
        Me.CmdLimpiarF.Size = New System.Drawing.Size(67, 67)
        Me.CmdLimpiarF.TabIndex = 2
        Me.CmdLimpiarF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdLimpiarF.ThemeName = "Aqua"
        '
        'CmdSalirF
        '
        Me.CmdSalirF.Image = CType(resources.GetObject("CmdSalirF.Image"), System.Drawing.Image)
        Me.CmdSalirF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdSalirF.Location = New System.Drawing.Point(8, 21)
        Me.CmdSalirF.Name = "CmdSalirF"
        Me.CmdSalirF.Size = New System.Drawing.Size(67, 67)
        Me.CmdSalirF.TabIndex = 1
        Me.CmdSalirF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdSalirF.ThemeName = "Aqua"
        '
        'RadPanel2
        '
        Me.RadPanel2.Controls.Add(Me.TablaSeries)
        Me.RadPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel2.Location = New System.Drawing.Point(0, 144)
        Me.RadPanel2.Name = "RadPanel2"
        Me.RadPanel2.Size = New System.Drawing.Size(1401, 379)
        Me.RadPanel2.TabIndex = 1
        '
        'TablaSeries
        '
        Me.TablaSeries.AllowUserToAddRows = False
        Me.TablaSeries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaSeries.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SelFactura, Me.Serie, Me.Abono, Me.CtaIngG, Me.CtaIngEx, Me.CtaIngC, Me.IVATras, Me.ISRRet, Me.IVARet, Me.CtaIngPCG, Me.CtaIngPCE, Me.CtaIngPCC, Me.IVAPTras, Me.ISRRPA, Me.IVARetPA, Me.Clientes, Me.AnticipoClientes, Me.IvaClientes, Me.IvaCancelClientes, Me.DevSVentasG, Me.DevSVentasC, Me.DevSVentasEx, Me.IvaSDev, Me.ID})
        Me.TablaSeries.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaSeries.Location = New System.Drawing.Point(0, 0)
        Me.TablaSeries.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TablaSeries.Name = "TablaSeries"
        Me.TablaSeries.RowTemplate.Height = 24
        Me.TablaSeries.Size = New System.Drawing.Size(1401, 379)
        Me.TablaSeries.TabIndex = 4
        '
        'SelFactura
        '
        Me.SelFactura.HeaderText = "Selecciona"
        Me.SelFactura.Name = "SelFactura"
        '
        'Serie
        '
        Me.Serie.HeaderText = "Serie"
        Me.Serie.Name = "Serie"
        '
        'Abono
        '
        Me.Abono.HeaderText = "Abono"
        Me.Abono.Name = "Abono"
        '
        'CtaIngG
        '
        Me.CtaIngG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.CtaIngG.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.CtaIngG.HeaderText = "Cta_Ing_Grava"
        Me.CtaIngG.Name = "CtaIngG"
        Me.CtaIngG.Width = 104
        '
        'CtaIngEx
        '
        Me.CtaIngEx.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.CtaIngEx.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.CtaIngEx.HeaderText = "Cta_Ing_Exentos"
        Me.CtaIngEx.Name = "CtaIngEx"
        Me.CtaIngEx.Width = 115
        '
        'CtaIngC
        '
        Me.CtaIngC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.CtaIngC.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.CtaIngC.HeaderText = "Cta_Ing_Cero"
        Me.CtaIngC.Name = "CtaIngC"
        Me.CtaIngC.Width = 97
        '
        'IVATras
        '
        Me.IVATras.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.IVATras.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.IVATras.HeaderText = "IVA Trasladado"
        Me.IVATras.Name = "IVATras"
        Me.IVATras.Width = 95
        '
        'ISRRet
        '
        Me.ISRRet.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.ISRRet.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.ISRRet.HeaderText = "ISR Retenido"
        Me.ISRRet.Name = "ISRRet"
        Me.ISRRet.Width = 83
        '
        'IVARet
        '
        Me.IVARet.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.IVARet.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.IVARet.HeaderText = "IVA Retenido"
        Me.IVARet.Name = "IVARet"
        Me.IVARet.Width = 85
        '
        'CtaIngPCG
        '
        Me.CtaIngPCG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.CtaIngPCG.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.CtaIngPCG.HeaderText = "Cta Ing Por Cobrar Gravados"
        Me.CtaIngPCG.Name = "CtaIngPCG"
        Me.CtaIngPCG.Width = 121
        '
        'CtaIngPCE
        '
        Me.CtaIngPCE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.CtaIngPCE.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.CtaIngPCE.HeaderText = "Cta Ing Por Cobrar_Exentos"
        Me.CtaIngPCE.Name = "CtaIngPCE"
        Me.CtaIngPCE.Width = 165
        '
        'CtaIngPCC
        '
        Me.CtaIngPCC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.CtaIngPCC.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.CtaIngPCC.HeaderText = "Cta Ing Por Cobrar Cero"
        Me.CtaIngPCC.Name = "CtaIngPCC"
        Me.CtaIngPCC.Width = 121
        '
        'IVAPTras
        '
        Me.IVAPTras.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.IVAPTras.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.IVAPTras.HeaderText = "IVA_Por_Trasladar"
        Me.IVAPTras.Name = "IVAPTras"
        Me.IVAPTras.Width = 122
        '
        'ISRRPA
        '
        Me.ISRRPA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.ISRRPA.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.ISRRPA.HeaderText = "ISR_Retenido_Por_Acreditar"
        Me.ISRRPA.Name = "ISRRPA"
        Me.ISRRPA.Width = 181
        '
        'IVARetPA
        '
        Me.IVARetPA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.IVARetPA.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.IVARetPA.HeaderText = "IVA_Ret_Por_Acreditar"
        Me.IVARetPA.Name = "IVARetPA"
        Me.IVARetPA.Width = 149
        '
        'Clientes
        '
        Me.Clientes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Clientes.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.Clientes.HeaderText = "Clientes"
        Me.Clientes.Name = "Clientes"
        Me.Clientes.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Clientes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Clientes.Width = 86
        '
        'AnticipoClientes
        '
        Me.AnticipoClientes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.AnticipoClientes.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.AnticipoClientes.HeaderText = "Anticipo de Clientes"
        Me.AnticipoClientes.Name = "AnticipoClientes"
        Me.AnticipoClientes.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AnticipoClientes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.AnticipoClientes.Width = 145
        '
        'IvaClientes
        '
        Me.IvaClientes.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.IvaClientes.HeaderText = "IVA de Anticipos Clientes"
        Me.IvaClientes.Name = "IvaClientes"
        Me.IvaClientes.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IvaClientes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.IvaClientes.Width = 300
        '
        'IvaCancelClientes
        '
        Me.IvaCancelClientes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.IvaCancelClientes.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.IvaCancelClientes.HeaderText = "IVA Cancel Anticipos Clientes"
        Me.IvaCancelClientes.Name = "IvaCancelClientes"
        Me.IvaCancelClientes.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IvaCancelClientes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.IvaCancelClientes.Width = 153
        '
        'DevSVentasG
        '
        Me.DevSVentasG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DevSVentasG.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.DevSVentasG.HeaderText = "DevSobreVentasG"
        Me.DevSVentasG.Name = "DevSVentasG"
        Me.DevSVentasG.Width = 125
        '
        'DevSVentasC
        '
        Me.DevSVentasC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DevSVentasC.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.DevSVentasC.HeaderText = "DevSobreVentasC"
        Me.DevSVentasC.Name = "DevSVentasC"
        Me.DevSVentasC.Width = 124
        '
        'DevSVentasEx
        '
        Me.DevSVentasEx.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DevSVentasEx.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.DevSVentasEx.HeaderText = "DevSobreVentasEx"
        Me.DevSVentasEx.Name = "DevSVentasEx"
        Me.DevSVentasEx.Width = 128
        '
        'IvaSDev
        '
        Me.IvaSDev.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.IvaSDev.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.IvaSDev.HeaderText = "IVA Sobre Dev"
        Me.IvaSDev.Name = "IvaSDev"
        Me.IvaSDev.Width = 93
        '
        'ID
        '
        Me.ID.HeaderText = "id"
        Me.ID.Name = "ID"
        Me.ID.Visible = False
        '
        'Ingresos_Series
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1401, 523)
        Me.Controls.Add(Me.RadPanel2)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Ingresos_Series"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "P_Master"
        Me.Text = "Ingresos Series"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.BarraF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdEliminarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdGuardarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdNuevoF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdBuscarFact, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdLimpiarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel2.ResumeLayout(False)
        CType(Me.TablaSeries, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents CmdSalirF As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdLimpiarF As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdBuscarFact As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdEliminarF As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdGuardarF As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdNuevoF As Telerik.WinControls.UI.RadButton
    Friend WithEvents Label12 As Label
    Friend WithEvents lstCliente As Listas
    Friend WithEvents RadPanel2 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents TablaSeries As DataGridView
    Friend WithEvents SelFactura As DataGridViewCheckBoxColumn
    Friend WithEvents Serie As DataGridViewTextBoxColumn
    Friend WithEvents Abono As DataGridViewCheckBoxColumn
    Friend WithEvents CtaIngG As DataGridViewComboBoxColumn
    Friend WithEvents CtaIngEx As DataGridViewComboBoxColumn
    Friend WithEvents CtaIngC As DataGridViewComboBoxColumn
    Friend WithEvents IVATras As DataGridViewComboBoxColumn
    Friend WithEvents ISRRet As DataGridViewComboBoxColumn
    Friend WithEvents IVARet As DataGridViewComboBoxColumn
    Friend WithEvents CtaIngPCG As DataGridViewComboBoxColumn
    Friend WithEvents CtaIngPCE As DataGridViewComboBoxColumn
    Friend WithEvents CtaIngPCC As DataGridViewComboBoxColumn
    Friend WithEvents IVAPTras As DataGridViewComboBoxColumn
    Friend WithEvents ISRRPA As DataGridViewComboBoxColumn
    Friend WithEvents IVARetPA As DataGridViewComboBoxColumn
    Friend WithEvents Clientes As DataGridViewComboBoxColumn
    Friend WithEvents AnticipoClientes As DataGridViewComboBoxColumn
    Friend WithEvents IvaClientes As DataGridViewComboBoxColumn
    Friend WithEvents IvaCancelClientes As DataGridViewComboBoxColumn
    Friend WithEvents DevSVentasG As DataGridViewComboBoxColumn
    Friend WithEvents DevSVentasC As DataGridViewComboBoxColumn
    Friend WithEvents DevSVentasEx As DataGridViewComboBoxColumn
    Friend WithEvents IvaSDev As DataGridViewComboBoxColumn
    Friend WithEvents ID As DataGridViewTextBoxColumn
    Friend WithEvents BarraF As Telerik.WinControls.UI.RadProgressBar
End Class

