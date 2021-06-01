<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Control_Proyectos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Control_Proyectos))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.TablaControlEquipos = New ATMFiscal.Tabla_Filtro()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lstClientes = New ATMFiscal.Listas()
        Me.lstEquipo = New ATMFiscal.Listas()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.TablaControlEquipos)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1384, 365)
        Me.RadPanel1.TabIndex = 0
        '
        'TablaControlEquipos
        '
        Me.TablaControlEquipos.BackColor = System.Drawing.Color.LightSteelBlue
        Me.TablaControlEquipos.CmdActualizar_enabled = True
        Me.TablaControlEquipos.Cmdcerrar_enabled = True
        Me.TablaControlEquipos.CmdEliminar_enabled = True
        Me.TablaControlEquipos.CmdExportaExcel_enabled = True
        Me.TablaControlEquipos.Cmdguardar_enabled = True
        Me.TablaControlEquipos.CmdNuevo_enabled = True
        Me.TablaControlEquipos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaControlEquipos.Location = New System.Drawing.Point(0, 0)
        Me.TablaControlEquipos.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaControlEquipos.Name = "TablaControlEquipos"
        Me.TablaControlEquipos.Size = New System.Drawing.Size(1384, 365)
        Me.TablaControlEquipos.SqlSelect = "select"
        Me.TablaControlEquipos.TabIndex = 0
        Me.TablaControlEquipos.Tag = "P_Master"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadGroupBox1.Controls.Add(Me.Label2)
        Me.RadGroupBox1.Controls.Add(Me.lstClientes)
        Me.RadGroupBox1.Controls.Add(Me.lstEquipo)
        Me.RadGroupBox1.Controls.Add(Me.Label3)
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Control Datos Principales"
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 388)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(381, 207)
        Me.RadGroupBox1.TabIndex = 1
        Me.RadGroupBox1.Text = "Control Datos Principales"
        Me.RadGroupBox1.ThemeName = "MaterialBlueGrey"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(110, 18)
        Me.Label2.TabIndex = 104
        Me.Label2.Text = "Departamento:"
        '
        'lstClientes
        '
        Me.lstClientes.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstClientes.Location = New System.Drawing.Point(20, 147)
        Me.lstClientes.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstClientes.Name = "lstClientes"
        Me.lstClientes.SelectItem = ""
        Me.lstClientes.SelectText = ""
        Me.lstClientes.Size = New System.Drawing.Size(340, 36)
        Me.lstClientes.TabIndex = 103
        '
        'lstEquipo
        '
        Me.lstEquipo.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstEquipo.Location = New System.Drawing.Point(20, 61)
        Me.lstEquipo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstEquipo.Name = "lstEquipo"
        Me.lstEquipo.SelectItem = ""
        Me.lstEquipo.SelectText = ""
        Me.lstEquipo.Size = New System.Drawing.Size(340, 36)
        Me.lstEquipo.TabIndex = 102
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 113)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 18)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Empresa:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 18)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Departamento:"
        '
        'Control_Proyectos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.CadetBlue
        Me.ClientSize = New System.Drawing.Size(1384, 618)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Controls.Add(Me.RadPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Control_Proyectos"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "P_Master"
        Me.Text = "Asignación de Empresas a Departamentos"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents TablaControlEquipos As Tabla_Filtro
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lstClientes As Listas
    Friend WithEvents lstEquipo As Listas
    Friend WithEvents Label2 As Label
End Class

