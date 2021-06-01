<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Control_Depto_trabajo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Control_Depto_trabajo))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.TablaEquipos = New ATMFiscal.Tabla_Filtro()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstEquipo = New ATMFiscal.Listas()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.TablaEquipos)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1384, 384)
        Me.RadPanel1.TabIndex = 0
        '
        'TablaEquipos
        '
        Me.TablaEquipos.BackColor = System.Drawing.Color.LightSteelBlue
        Me.TablaEquipos.CmdActualizar_enabled = True
        Me.TablaEquipos.Cmdcerrar_enabled = True
        Me.TablaEquipos.CmdEliminar_enabled = True
        Me.TablaEquipos.CmdExportaExcel_enabled = True
        Me.TablaEquipos.Cmdguardar_enabled = True
        Me.TablaEquipos.CmdNuevo_enabled = True
        Me.TablaEquipos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaEquipos.Location = New System.Drawing.Point(0, 0)
        Me.TablaEquipos.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaEquipos.Name = "TablaEquipos"
        Me.TablaEquipos.Size = New System.Drawing.Size(1384, 384)
        Me.TablaEquipos.SqlSelect = "select"
        Me.TablaEquipos.TabIndex = 0
        Me.TablaEquipos.Tag = "P_Master"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.Controls.Add(Me.lstEquipo)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Control Datos"
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 401)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(360, 130)
        Me.RadGroupBox1.TabIndex = 1
        Me.RadGroupBox1.Text = "Control Datos"
        Me.RadGroupBox1.ThemeName = "MaterialBlueGrey"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 18)
        Me.Label1.TabIndex = 102
        Me.Label1.Text = "Departamentos:"
        '
        'lstEquipo
        '
        Me.lstEquipo.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstEquipo.Location = New System.Drawing.Point(18, 60)
        Me.lstEquipo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstEquipo.Name = "lstEquipo"
        Me.lstEquipo.SelectItem = ""
        Me.lstEquipo.SelectText = ""
        Me.lstEquipo.Size = New System.Drawing.Size(312, 36)
        Me.lstEquipo.TabIndex = 101
        '
        'Control_Depto_trabajo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.CadetBlue
        Me.ClientSize = New System.Drawing.Size(1384, 571)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Controls.Add(Me.RadPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Control_Depto_trabajo"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "P_Master"
        Me.Text = "Control Departamentos Contables"
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
    Friend WithEvents TablaEquipos As Tabla_Filtro
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lstEquipo As Listas
End Class

