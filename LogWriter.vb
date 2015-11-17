Imports System.IO

''' <summary>
''' CONTROLE DE REGISTRO DIGITAL
''' </summary>
''' <remarks></remarks>
Public Class LogWriter

    Private LogAddr As String = My.Settings.LogAddress
    Public UsrCFG As New UsrCFG

    ''' <summary>
    ''' ESCREVE ARQUIVO DE LOG
    ''' </summary>
    ''' <param name="DATAHORA">STRING</param>
    ''' <param name="MSG">STRING</param>
    ''' <param name="FUNCAO">STRING</param>
    ''' <returns>NOTHING</returns>
    ''' <remarks>ENJOY</remarks>
    Function WhriteLog(ByVal DATAHORA As String, ByVal MSG As String, ByVal FUNCAO As String)

        Dim ADDRESS As String = LogAddr
        Dim NOMELOG As String = "_" & Environment.MachineName & "_" & Format(Now, "yyyyMMdd") & ".txt"

        Dim log As StreamWriter
        Dim texto As String = ""
        Dim fluxoTexto As IO.StreamReader
        Dim linhaTexto As String

        If IO.File.Exists(ADDRESS & NOMELOG) Then
            fluxoTexto = New IO.StreamReader(ADDRESS & NOMELOG)
            linhaTexto = fluxoTexto.ReadLine

            While linhaTexto <> Nothing
                texto = texto & linhaTexto & vbNewLine
                linhaTexto = fluxoTexto.ReadLine
            End While
            fluxoTexto.Close()
        End If
        texto = UsrCFG.NomeDoUsuario & "-" & DATAHORA & " - " & FUNCAO & ": " & MSG & "; " & "MySql:" & vbNewLine & texto & vbNewLine & "//---- --"
        log = New StreamWriter(ADDRESS & NOMELOG)
        log.Write(texto)
        log.Close()

        Return Nothing

    End Function
End Class
