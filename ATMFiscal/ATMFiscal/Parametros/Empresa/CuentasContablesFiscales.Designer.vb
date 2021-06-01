<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CuentasContablesFiscales
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CuentasContablesFiscales))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CmdGuardarF = New Telerik.WinControls.UI.RadButton()
        Me.CmdNuevoF = New Telerik.WinControls.UI.RadButton()
        Me.CmdSalirF = New Telerik.WinControls.UI.RadButton()
        Me.CmdBuscarFact = New Telerik.WinControls.UI.RadButton()
        Me.CmdEliminarF = New Telerik.WinControls.UI.RadButton()
        Me.CmdPlant = New Telerik.WinControls.UI.RadButton()
        Me.TablaImportar = New System.Windows.Forms.DataGridView()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Descripcion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fiscal = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.CmdGuardarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdNuevoF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdBuscarFact, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdEliminarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdPlant, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label5)
        Me.RadPanel1.Controls.Add(Me.CmdGuardarF)
        Me.RadPanel1.Controls.Add(Me.CmdNuevoF)
        Me.RadPanel1.Controls.Add(Me.CmdSalirF)
        Me.RadPanel1.Controls.Add(Me.CmdBuscarFact)
        Me.RadPanel1.Controls.Add(Me.CmdEliminarF)
        Me.RadPanel1.Controls.Add(Me.CmdPlant)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(959, 76)
        Me.RadPanel1.TabIndex = 2
        Me.RadPanel1.ThemeName = "Material"
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(351, 26)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(370, 36)
        Me.lstCliente.TabIndex = 581
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(348, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 18)
        Me.Label5.TabIndex = 580
        Me.Label5.Text = "Empresa:"
        '
        'CmdGuardarF
        '
        Me.CmdGuardarF.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdGuardarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdGuardarF.Location = New System.Drawing.Point(219, 8)
        Me.CmdGuardarF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdGuardarF.Name = "CmdGuardarF"
        Me.CmdGuardarF.Size = New System.Drawing.Size(50, 54)
        Me.CmdGuardarF.TabIndex = 580
        Me.CmdGuardarF.TabStop = False
        Me.CmdGuardarF.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdGuardarF.ThemeName = "Aqua"
        '
        'CmdNuevoF
        '
        Me.CmdNuevoF.Image = Global.ATMFiscal.My.Resources.Resources.Nuevo
        Me.CmdNuevoF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdNuevoF.Location = New System.Drawing.Point(111, 8)
        Me.CmdNuevoF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdNuevoF.Name = "CmdNuevoF"
        Me.CmdNuevoF.Size = New System.Drawing.Size(50, 54)
        Me.CmdNuevoF.TabIndex = 581
        Me.CmdNuevoF.TabStop = False
        Me.CmdNuevoF.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdNuevoF.ThemeName = "Aqua"
        '
        'CmdSalirF
        '
        Me.CmdSalirF.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.CmdSalirF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdSalirF.Location = New System.Drawing.Point(3, 8)
        Me.CmdSalirF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdSalirF.Name = "CmdSalirF"
        Me.CmdSalirF.Size = New System.Drawing.Size(50, 54)
        Me.CmdSalirF.TabIndex = 587
        Me.CmdSalirF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdSalirF.ThemeName = "Aqua"
        '
        'CmdBuscarFact
        '
        Me.CmdBuscarFact.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdBuscarFact.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdBuscarFact.Location = New System.Drawing.Point(57, 8)
        Me.CmdBuscarFact.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdBuscarFact.Name = "CmdBuscarFact"
        Me.CmdBuscarFact.Size = New System.Drawing.Size(50, 54)
        Me.CmdBuscarFact.TabIndex = 582
        Me.CmdBuscarFact.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdBuscarFact.ThemeName = "Aqua"
        '
        'CmdEliminarF
        '
        Me.CmdEliminarF.Image = Global.ATMFiscal.My.Resources.Resources.Eliminar
        Me.CmdEliminarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdEliminarF.Location = New System.Drawing.Point(165, 8)
        Me.CmdEliminarF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdEliminarF.Name = "CmdEliminarF"
        Me.CmdEliminarF.Size = New System.Drawing.Size(50, 54)
        Me.CmdEliminarF.TabIndex = 586
        Me.CmdEliminarF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdEliminarF.ThemeName = "Aqua"
        '
        'CmdPlant
        '
        Me.CmdPlant.Image = Global.ATMFiscal.My.Resources.Resources.añadir_fila
        Me.CmdPlant.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdPlant.Location = New System.Drawing.Point(273, 8)
        Me.CmdPlant.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdPlant.Name = "CmdPlant"
        Me.CmdPlant.Size = New System.Drawing.Size(50, 54)
        Me.CmdPlant.TabIndex = 583
        Me.CmdPlant.TabStop = False
        Me.CmdPlant.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdPlant.ThemeName = "Aqua"
        '
        'TablaImportar
        '
        Me.TablaImportar.AllowUserToAddRows = False
        Me.TablaImportar.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.TablaImportar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaImportar.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.Descripcion, Me.Fiscal, Me.Tipo})
        Me.TablaImportar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaImportar.Location = New System.Drawing.Point(0, 76)
        Me.TablaImportar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TablaImportar.Name = "TablaImportar"
        Me.TablaImportar.RowTemplate.Height = 24
        Me.TablaImportar.Size = New System.Drawing.Size(959, 301)
        Me.TablaImportar.TabIndex = 3
        '
        'ID
        '
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.Visible = False
        '
        'Descripcion
        '
        Me.Descripcion.HeaderText = "Descripcion"
        Me.Descripcion.Name = "Descripcion"
        '
        'Fiscal
        '
        Me.Fiscal.HeaderText = "Fiscal"
        Me.Fiscal.Name = "Fiscal"
        Me.Fiscal.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Fiscal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Tipo
        '
        Me.Tipo.HeaderText = "Tipo"
        Me.Tipo.Name = "Tipo"
        '
        'CuentasContablesFiscales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(959, 377)
        Me.Controls.Add(Me.TablaImportar)
        Me.Controls.Add(Me.RadPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CuentasContablesFiscales"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cuentas Contables Fiscales"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.CmdGuardarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdNuevoF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdBuscarFact, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdEliminarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdPlant, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents lstCliente As Listas
    Friend WithEvents Label5 As Label
    Friend WithEvents CmdGuardarF As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdNuevoF As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdSalirF As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdBuscarFact As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdEliminarF As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdPlant As Telerik.WinControls.UI.RadButton
    Friend WithEvents TablaImportar As DataGridView
    Friend WithEvents ID As DataGridViewTextBoxColumn
    Friend WithEvents Descripcion As DataGridViewTextBoxColumn
    Friend WithEvents Fiscal As DataGridViewCheckBoxColumn
    Friend WithEvents Tipo As DataGridViewTextBoxColumn
End Class

