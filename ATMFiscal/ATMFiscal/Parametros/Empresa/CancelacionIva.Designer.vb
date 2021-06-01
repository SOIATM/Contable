<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CancelacionIva
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CancelacionIva))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Tabla_detalleCancelaciones = New ATMFiscal.tabla_detalle()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label20.Location = New System.Drawing.Point(435, 9)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(72, 18)
        Me.Label20.TabIndex = 91
        Me.Label20.Text = "Empresa:"
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(438, 35)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(450, 36)
        Me.lstCliente.TabIndex = 92
        '
        'Tabla_detalleCancelaciones
        '
        Me.Tabla_detalleCancelaciones.Cmdcerrar_enabled = True
        Me.Tabla_detalleCancelaciones.CmdEditar_Enabled = True
        Me.Tabla_detalleCancelaciones.CmdEliminar_enabled = True
        Me.Tabla_detalleCancelaciones.CmdNuevo_Enabled = True
        Me.Tabla_detalleCancelaciones.CmdRefrescar_enabled = True
        Me.Tabla_detalleCancelaciones.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla_detalleCancelaciones.Location = New System.Drawing.Point(0, 0)
        Me.Tabla_detalleCancelaciones.Name = "Tabla_detalleCancelaciones"
        Me.Tabla_detalleCancelaciones.Size = New System.Drawing.Size(905, 518)
        Me.Tabla_detalleCancelaciones.SqlSelect = ""
        Me.Tabla_detalleCancelaciones.TabIndex = 0
        Me.Tabla_detalleCancelaciones.Tag = "P_Master"
        '
        'CancelacionIva
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(905, 518)
        Me.ControlBox = False
        Me.Controls.Add(Me.lstCliente)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Tabla_detalleCancelaciones)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CancelacionIva"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cuentas para Cancelaciones de IVA"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents Tabla_detalleCancelaciones As tabla_detalle
    Friend WithEvents lstCliente As Listas
    Friend WithEvents Label20 As Label
End Class

