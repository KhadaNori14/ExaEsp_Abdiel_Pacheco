Public Class FrmVisitantes
    Private Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
        TxtNombres.Text = ""
        TxtApellidos.Text = ""
        TxtEmail.Text = ""
        TxtTelefono.Text = ""
        CmbCiudad.SelectedIndex = -1
        LblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy")
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Dim dao As New daoVisitantes()
        Dim visitante As New VisitantesEntidad()
        visitante.Nombres = TxtNombres.Text
        visitante.Apellidos = TxtApellidos.Text
        visitante.Email = TxtEmail.Text
        visitante.Telefono = TxtTelefono.Text
        visitante.Fechavisita = DateTime.ParseExact(LblFecha.Text, "dd/MM/yyyy", Nothing)
        visitante.Ciudad = CmbCiudad.SelectedItem.ToString()
        If dao.AgregarRegistro(visitante) Then
            MessageBox.Show("Registro guardado correctamente.")
        Else
            MessageBox.Show("Error al guardar el registro.")
        End If
    End Sub
    Private Sub BtnEditar_Click(sender As Object, e As EventArgs) Handles BtnEditar.Click
        Dim dao As New daoVisitantes()
        Dim id As Integer = ObtenerIdSeleccionado()
        If id > 0 Then
            Dim visitante As VisitantesEntidad = dao.BuscarRegistro(id)
            TxtNombres.Text = visitante.Nombres
            TxtApellidos.Text = visitante.Apellidos
            TxtEmail.Text = visitante.Email
            TxtTelefono.Text = visitante.Telefono
            CmbCiudad.SelectedItem = visitante.Ciudad
            LblFecha.Text = visitante.Fechavisita.ToString("dd/MM/yyyy")
        Else
            MessageBox.Show("Debe seleccionar un registro para editar.")
        End If
    End Sub

    Private Function ObtenerIdSeleccionado(Optional ByVal index As Integer = 0) As Integer
        If DgvVisitantes.SelectedRows.Count > 0 Then
            Return Integer.Parse(DgvVisitantes.SelectedRows(index).Cells("Id").Value.ToString())
        Else
            Return 0
        End If
    End Function

    Private Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        Dim dao As New daoVisitantes()
        Dim id As Integer = ObtenerIdSeleccionado()
        If id > 0 Then
            If dao.EliminarRegistro(id) Then
                MessageBox.Show("Registro eliminado correctamente.")
            Else
                MessageBox.Show("Error al eliminar el registro.")
            End If
        Else
            MessageBox.Show("Debe seleccionar un registro para eliminar.")
        End If
    End Sub

    Private Function ObtenerIdSeleccionado() As Integer
        If DgvVisitantes.SelectedRows.Count > 0 Then
            Return Integer.Parse(DgvVisitantes.SelectedRows(0).Cells("Id").Value.ToString())
        Else
            Return 0
        End If
    End Function

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvVisitantes.CellContentClick

    End Sub
End Class