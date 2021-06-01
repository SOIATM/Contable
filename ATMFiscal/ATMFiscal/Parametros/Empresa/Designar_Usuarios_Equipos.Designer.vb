<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Designar_Usuarios_Equipos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Designar_Usuarios_Equipos))
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.TablaEquipos_Usuarios = New ATMFiscal.Tabla_Filtro()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lstUsuario = New ATMFiscal.Listas()
        Me.lstEquipos = New ATMFiscal.Listas()
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
        Me.RadPanel1.Controls.Add(Me.TablaEquipos_Usuarios)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1432, 463)
        Me.RadPanel1.TabIndex = 0
        '
        'TablaEquipos_Usuarios
        '
        Me.TablaEquipos_Usuarios.BackColor = System.Drawing.Color.LightSteelBlue
        Me.TablaEquipos_Usuarios.CmdActualizar_enabled = True
        Me.TablaEquipos_Usuarios.Cmdcerrar_enabled = True
        Me.TablaEquipos_Usuarios.CmdEliminar_enabled = True
        Me.TablaEquipos_Usuarios.CmdExportaExcel_enabled = True
        Me.TablaEquipos_Usuarios.Cmdguardar_enabled = True
        Me.TablaEquipos_Usuarios.CmdNuevo_enabled = True
        Me.TablaEquipos_Usuarios.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaEquipos_Usuarios.Location = New System.Drawing.Point(0, 0)
        Me.TablaEquipos_Usuarios.Name = "TablaEquipos_Usuarios"
        Me.TablaEquipos_Usuarios.Size = New System.Drawing.Size(1432, 463)
        Me.TablaEquipos_Usuarios.SqlSelect = "select"
        Me.TablaEquipos_Usuarios.TabIndex = 0
        Me.TablaEquipos_Usuarios.Tag = "P_Master"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadGroupBox1.Controls.Add(Me.Label2)
        Me.RadGroupBox1.Controls.Add(Me.Label3)
        Me.RadGroupBox1.Controls.Add(Me.lstUsuario)
        Me.RadGroupBox1.Controls.Add(Me.lstEquipos)
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Control Datos Principales"
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 478)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(667, 125)
        Me.RadGroupBox1.TabIndex = 1
        Me.RadGroupBox1.Text = "Control Datos Principales"
        Me.RadGroupBox1.ThemeName = "MaterialBlueGrey"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(338, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 18)
        Me.Label2.TabIndex = 104
        Me.Label2.Text = "Usuario:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(338, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 18)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Usuario:"
        Me.Label3.Visible = False
        '
        'lstUsuario
        '
        Me.lstUsuario.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstUsuario.Location = New System.Drawing.Point(342, 63)
        Me.lstUsuario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstUsuario.Name = "lstUsuario"
        Me.lstUsuario.SelectItem = ""
        Me.lstUsuario.SelectText = ""
        Me.lstUsuario.Size = New System.Drawing.Size(312, 36)
        Me.lstUsuario.TabIndex = 103
        '
        'lstEquipos
        '
        Me.lstEquipos.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstEquipos.Location = New System.Drawing.Point(9, 63)
        Me.lstEquipos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstEquipos.Name = "lstEquipos"
        Me.lstEquipos.SelectItem = ""
        Me.lstEquipos.SelectText = ""
        Me.lstEquipos.Size = New System.Drawing.Size(312, 36)
        Me.lstEquipos.TabIndex = 102
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 18)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Departamentos:"
        '
        'Designar_Usuarios_Equipos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.CadetBlue
        Me.ClientSize = New System.Drawing.Size(1432, 639)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Controls.Add(Me.RadPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Designar_Usuarios_Equipos"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "P_Master"
        Me.Text = "Designar Usuarios a Departamentos Contables"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents TablaEquipos_Usuarios As Tabla_Filtro
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lstUsuario As Listas
    Friend WithEvents lstEquipos As Listas
    Friend WithEvents Label2 As Label
End Class

