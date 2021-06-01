<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Clasificaciondecuentas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Clasificaciondecuentas))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.LstEmpresa = New ATMFiscal.Listas()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.CmdEliminarF = New Telerik.WinControls.UI.RadButton()
        Me.CmdGuardarF = New Telerik.WinControls.UI.RadButton()
        Me.CmdNuevoF = New Telerik.WinControls.UI.RadButton()
        Me.CmdBuscarFact = New Telerik.WinControls.UI.RadButton()
        Me.CmdSalirF = New Telerik.WinControls.UI.RadButton()
        Me.Estad = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Bal = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Cla = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Desc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TablaImportar = New System.Windows.Forms.DataGridView()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.CmdEliminarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdGuardarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdNuevoF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdBuscarFact, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadPanel1.Controls.Add(Me.LstEmpresa)
        Me.RadPanel1.Controls.Add(Me.Label12)
        Me.RadPanel1.Controls.Add(Me.CmdEliminarF)
        Me.RadPanel1.Controls.Add(Me.CmdGuardarF)
        Me.RadPanel1.Controls.Add(Me.CmdNuevoF)
        Me.RadPanel1.Controls.Add(Me.CmdBuscarFact)
        Me.RadPanel1.Controls.Add(Me.CmdSalirF)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(874, 114)
        Me.RadPanel1.TabIndex = 1
        Me.RadPanel1.Tag = "P_Master"
        '
        'LstEmpresa
        '
        Me.LstEmpresa.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstEmpresa.Location = New System.Drawing.Point(404, 43)
        Me.LstEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstEmpresa.Name = "LstEmpresa"
        Me.LstEmpresa.SelectItem = ""
        Me.LstEmpresa.SelectText = ""
        Me.LstEmpresa.Size = New System.Drawing.Size(387, 36)
        Me.LstEmpresa.TabIndex = 688
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(400, 19)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 18)
        Me.Label12.TabIndex = 687
        Me.Label12.Text = "Empresa:"
        '
        'CmdEliminarF
        '
        Me.CmdEliminarF.Image = Global.ATMFiscal.My.Resources.Resources.Eliminar
        Me.CmdEliminarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdEliminarF.Location = New System.Drawing.Point(227, 21)
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
        Me.CmdGuardarF.Location = New System.Drawing.Point(300, 21)
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
        Me.CmdNuevoF.Location = New System.Drawing.Point(154, 21)
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
        Me.CmdBuscarFact.Location = New System.Drawing.Point(81, 21)
        Me.CmdBuscarFact.Name = "CmdBuscarFact"
        Me.CmdBuscarFact.Size = New System.Drawing.Size(67, 67)
        Me.CmdBuscarFact.TabIndex = 3
        Me.CmdBuscarFact.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdBuscarFact.ThemeName = "Aqua"
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
        'Estad
        '
        Me.Estad.HeaderText = "Estados de Resultados"
        Me.Estad.Name = "Estad"
        Me.Estad.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Estad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Bal
        '
        Me.Bal.HeaderText = "Balanza"
        Me.Bal.Name = "Bal"
        Me.Bal.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Bal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Cla
        '
        Me.Cla.HeaderText = "Clave"
        Me.Cla.Name = "Cla"
        '
        'Desc
        '
        Me.Desc.HeaderText = "Descripción"
        Me.Desc.Name = "Desc"
        '
        'ID
        '
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.Visible = False
        '
        'TablaImportar
        '
        Me.TablaImportar.AllowUserToAddRows = False
        Me.TablaImportar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaImportar.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.Desc, Me.Cla, Me.Bal, Me.Estad})
        Me.TablaImportar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaImportar.Location = New System.Drawing.Point(0, 114)
        Me.TablaImportar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TablaImportar.Name = "TablaImportar"
        Me.TablaImportar.RowTemplate.Height = 24
        Me.TablaImportar.Size = New System.Drawing.Size(874, 309)
        Me.TablaImportar.TabIndex = 5
        '
        'Clasificaciondecuentas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(874, 423)
        Me.ControlBox = False
        Me.Controls.Add(Me.TablaImportar)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Clasificaciondecuentas"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Clasificacion de Cuentas Balance y Resultados"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.CmdEliminarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdGuardarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdNuevoF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdBuscarFact, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents LstEmpresa As Listas
	Friend WithEvents Label12 As Label
	Friend WithEvents CmdEliminarF As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdGuardarF As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdNuevoF As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdBuscarFact As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdSalirF As Telerik.WinControls.UI.RadButton
	Friend WithEvents Estad As DataGridViewCheckBoxColumn
	Friend WithEvents Bal As DataGridViewCheckBoxColumn
	Friend WithEvents Cla As DataGridViewTextBoxColumn
	Friend WithEvents Desc As DataGridViewTextBoxColumn
	Friend WithEvents ID As DataGridViewTextBoxColumn
	Friend WithEvents TablaImportar As DataGridView
    Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
End Class

