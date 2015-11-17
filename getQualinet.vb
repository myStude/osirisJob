Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports MySql.Data.MySqlClient
Imports System.Threading

''' <summary>
''' busca dados do qualinet
''' </summary>
''' <remarks></remarks>
Public Class getQualinet
    Inherits MySql_Repository

    Private sqlQuery As New Query

    Sub AT5()

        Dim RESULT As String = Nothing

        Using qConn As New MySqlConnection(qualinetConnection("paq_last"))
            qConn.Open()
            Dim qComm As New MySqlCommand(sqlQuery.at5(Format(Now, "yyyy-MM-dd"), "100"), qConn)
            Using ReadRows As MySqlDataReader = qComm.ExecuteReader()
                While ReadRows.Read
                    RESULT += ReadRows.Item("COD_OS")
                End While
            End Using
        End Using


    End Sub


End Class
