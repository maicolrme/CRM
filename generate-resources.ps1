$ErrorActionPreference = 'Stop'

$keys = [ordered]@{
'Common.Save'        = @('Save','Guardar','Salva')
'Common.Cancel'      = @('Cancel','Cancelar','Annulla')
'Common.Delete'      = @('Delete','Eliminar','Elimina')
'Common.Edit'        = @('Edit','Editar','Modifica')
'Common.Create'      = @('Create','Crear','Crea')
'Common.Search'      = @('Search','Buscar','Cerca')
'Common.Back'        = @('Back','Volver','Indietro')
'Common.Actions'     = @('Actions','Acciones','Azioni')
'Common.Status'      = @('Status','Estado','Stato')
'Common.Email'       = @('Email','Email','Email')
'Common.Phone'       = @('Phone','Teléfono','Telefono')
'Common.Notes'       = @('Notes','Notas','Note')
'Common.Name'        = @('Name','Nombre','Nome')
'Common.Details'     = @('Details','Detalles','Dettagli')
'Common.ConfirmDelete' = @('Are you sure you want to delete this record?','¿Seguro que desea eliminar este registro?','Sei sicuro di voler eliminare questa registrazione?')
'Common.NoResults'   = @('No results found','No se encontraron resultados','Nessun risultato trovato')
'Common.All'         = @('All','Todos','Tutti')
'Common.Language'    = @('Language','Idioma','Lingua')
'Common.Optional'    = @('Optional','Opcional','Facoltativo')
'Common.Close'       = @('Close','Cerrar','Chiudi')
'Common.Yes'         = @('Yes','Sì','Sì')
'Common.No'          = @('No','No','No')
'Common.Date'        = @('Date','Fecha','Data')
'Common.Description' = @('Description','Descripción','Descrizione')
'Common.User'        = @('User','Usuario','Utente')
'Common.Company'     = @('Company','Empresa','Azienda')

'Navigation.Dashboard'   = @('Dashboard','Panel','Dashboard')
'Navigation.Customers'   = @('Customers','Clientes','Clienti')
'Navigation.Companies'   = @('Companies','Empresas','Aziende')
'Navigation.Contacts'    = @('Contacts','Contactos','Contatti')
'Navigation.Activities'  = @('Activities','Actividades','Attività')
'Navigation.Tasks'       = @('Tasks','Tareas','Compiti')
'Navigation.Admin'       = @('Admin','Administración','Amministrazione')
'Navigation.Users'       = @('Users','Usuarios','Utenti')
'Navigation.Roles'       = @('Roles','Roles','Ruoli')
'Navigation.Home'        = @('Home','Inicio','Home')
'Navigation.Menu'        = @('Menu','Menú','Menu')

'Auth.LoginTitle'      = @('Sign in to your account','Inicia sesión en tu cuenta','Accedi al tuo account')
'Auth.RegisterTitle'   = @('Create your account','Crea tu cuenta','Crea il tuo account')
'Auth.Email'           = @('Email address','Correo electrónico','Indirizzo email')
'Auth.Password'        = @('Password','Contraseña','Password')
'Auth.ConfirmPassword' = @('Confirm password','Confirmar contraseña','Conferma password')
'Auth.FirstName'       = @('First name','Nombre','Nome')
'Auth.LastName'        = @('Last name','Apellido','Cognome')
'Auth.Login'           = @('Sign in','Iniciar sesión','Accedi')
'Auth.Register'        = @('Sign up','Registrarse','Registrati')
'Auth.NoAccount'       = @('Don''t have an account?','¿No tienes cuenta?','Non hai un account?')
'Auth.HaveAccount'     = @('Already have an account?','¿Ya tienes cuenta?','Hai già un account?')
'Auth.InvalidCredentials' = @('Invalid email or password.','Correo o contraseña incorrectos.','Email o password non validi.')
'Auth.Logout'        = @('Sign out','Cerrar sesión','Esci')
'Auth.EmailExists'     = @('That email is already registered.','Ese correo ya está registrado.','Quella email è già registrata.')
'Auth.Registered'      = @('Account created. You can sign in now.','Cuenta creada. Ya puedes iniciar sesión.','Account creato. Ora puoi accedere.')
'Auth.LanguagePrompt'  = @('Language','Idioma','Lingua')

'Home.Tagline'    = @('A simple, modern CRM to manage customers, companies, contacts, activities and tasks.','Un CRM simple y moderno para gestionar clientes, empresas, contactos, actividades y tareas.','Un CRM semplice e moderno per gestire clienti, aziende, contatti, attività e compiti.')
'Home.Features'   = @('Everything you need, nothing you don''t.','Todo lo que necesitas, nada que no.','Tutto quello che ti serve, niente di superfluo.')
'Home.Feature1Title' = @('Customer management','Gestión de clientes','Gestione clienti')
'Home.Feature1Text'  = @('Track leads, prospects and customers with statuses and notes.','Sigue leads, prospectos y clientes con estados y notas.','Traccia lead, prospect e clienti con stati e note.')
'Home.Feature2Title' = @('Activities & tasks','Actividades y tareas','Attività e compiti')
'Home.Feature2Text'  = @('Log calls, meetings and emails. Keep your follow-ups on time.','Registra llamadas, reuniones y correos. No pierdas seguimientos.','Registra chiamate, riunioni e email. Non perdere i follow-up.')
'Home.Feature3Title' = @('Multilanguage','Multilenguaje','Multilingua')
'Home.Feature3Text'  = @('English, Spanish and Italian out of the box.','Inglés, español e italiano desde el principio.','Inglese, spagnolo e italiano già disponibili.')

'Dashboard.Greeting'        = @('Good morning','Buenos días','Buongiorno')
'Dashboard.Customers'       = @('Customers','Clientes','Clienti')
'Dashboard.OpenTasks'       = @('Open tasks','Tareas abiertas','Compiti aperti')
'Dashboard.Activities'      = @('Activities','Actividades','Attività totali')
'Dashboard.RecentCustomers' = @('Recent customers','Clientes recientes','Clienti recenti')
'Dashboard.UpcomingTasks'   = @('Upcoming tasks','Próximas tareas','Prossimi compiti')
'Dashboard.RecentActivities'= @('Recent activities','Actividades recientes','Attività recenti')
'Dashboard.ViewAll'         = @('View all','Ver todo','Vedi tutto')
'Dashboard.NoTasks'         = @('No upcoming tasks.','No hay tareas pendientes.','Nessun compito in arrivo.')
'Dashboard.NoActivities'    = @('No activities yet.','Aún no hay actividades.','Nessuna attività ancora.')

'Customer.Title'   = @('Customers','Clientes','Clienti')
'Customer.Create'  = @('New customer','Nuevo cliente','Nuovo cliente')
'Customer.Edit'    = @('Edit customer','Editar cliente','Modifica cliente')
'Customer.Details' = @('Customer','Cliente','Cliente')
'Customer.Search'  = @('Search by name, email, company or phone...','Buscar por nombre, correo, empresa o teléfono...','Cerca per nome, email, azienda o telefono...')
'Customer.Filter'  = @('Filter by status','Filtrar por estado','Filtra per stato')
'Customer.Notes'   = @('Notes','Notas','Note')
'Customer.Activities' = @('Activities','Actividades','Attività')
'Customer.Tasks'   = @('Tasks','Tareas','Attività')
'Customer.Created' = @('Customer created.','Cliente creado.','Cliente creato.')
'Customer.Updated' = @('Customer updated.','Cliente actualizado.','Cliente aggiornato.')
'Customer.Deleted' = @('Customer deleted.','Cliente eliminado.','Cliente eliminato.')
'Customer.Empty'   = @('No customers found.','No se encontraron clienti.','Nessun cliente trovato.')
'Customer.Status.Lead'     = @('Lead','Lead','Lead')
'Customer.Status.Prospect' = @('Prospect','Prospecto','Prospect')
'Customer.Status.Customer' = @('Customer','Cliente','Cliente')
'Customer.Status.Inactive' = @('Inactive','Inactivo','Inattivo')

'Company.Title'   = @('Companies','Empresas','Aziende')
'Company.Create'  = @('New company','Nueva empresa','Nuova azienda')
'Company.Edit'    = @('Edit company','Editar empresa','Modifica azienda')
'Company.Details' = @('Company','Empresa','Azienda')
'Company.Search'  = @('Search companies...','Buscar empresas...','Cerca aziende...')
'Company.Website' = @('Website','Sitio web','Sito web')
'Company.Address' = @('Address','Dirección','Indirizzo')
'Company.Contacts'= @('Contacts','Contactos','Contatti')
'Company.RelatedCustomers' = @('Customers','Clientes','Clienti')
'Company.Created' = @('Company created.','Empresa creada.','Azienda creata.')
'Company.Updated' = @('Company updated.','Empresa actualizada.','Azienda aggiornata.')
'Company.Deleted' = @('Company deleted.','Empresa eliminada.','Azienda eliminata.')
'Company.Empty'   = @('No companies found.','No se encontraron empresas.','Nessuna azienda trovata.')

'Contact.Title'   = @('Contacts','Contactos','Contatti')
'Contact.Create'  = @('New contact','Nuevo contacto','Nuovo contatto')
'Contact.Edit'    = @('Edit contact','Editar contacto','Modifica contatto')
'Contact.Position'= @('Position','Cargo','Posizione')
'Contact.Search'  = @('Search contacts...','Buscar contactos...','Cerca contatti...')
'Contact.Created' = @('Contact created.','Contacto creado.','Contatto creato.')
'Contact.Updated' = @('Contact updated.','Contacto actualizado.','Contatto aggiornato.')
'Contact.Deleted' = @('Contact deleted.','Contacto eliminado.','Contatto eliminato.')
'Contact.Empty'   = @('No contacts found.','No se encontraron contactos.','Nessun contatto trovato.')

'Activity.Title'  = @('Activities','Actividades','Attività')
'Activity.Create' = @('New activity','Nueva attività','Nuova attività')
'Activity.Type'   = @('Type','Tipo','Tipo')
'Activity.Type.Call'    = @('Call','Llamada','Chiamata')
'Activity.Type.Meeting' = @('Meeting','Reunión','Riunione')
'Activity.Type.Email'   = @('Email','Correo','Email')
'Activity.Type.Note'    = @('Note','Nota','Nota')
'Activity.Type.Other'   = @('Other','Otro','Altro')
'Activity.DueDate'      = @('Date','Fecha','Data')
'Activity.LoggedBy'     = @('Logged by','Registrado por','Registrato da')
'Activity.Created'      = @('Activity created.','Actividad creada.','Attività creata.')
'Activity.Deleted'      = @('Activity deleted.','Actividad eliminada.','Attività eliminata.')
'Activity.Empty'        = @('No activities found.','No se encontraron actividades.','Nessuna attività trovata.')
'Activity.FilterCustomer' = @('Customer','Cliente','Cliente')

'Task.Title'       = @('Tasks','Tareas','Compiti')
'Task.Create'      = @('New task','Nueva tarea','Nuovo compito')
'Task.Edit'        = @('Edit task','Editar tarea','Modifica compito')
'Task.Complete'    = @('Complete','Completar','Completata')
'Task.MarkCompleted' = @('Mark as completed','Marcar como completada','Segna come completata')
'Task.Status.Pending'    = @('Pending','Pendiente','In sospeso')
'Task.Status.InProgress' = @('In Progress','En curso','In corso')
'Task.Status.Completed'  = @('Completed','Completada','Completata')
'Task.DueDate'      = @('Due date','Fecha límite','Scadenza')
'Task.Created'     = @('Task created.','Tarea creada.','Compito creato.')
'Task.Updated'     = @('Task updated.','Tarea actualizada.','Compito aggiornato.')
'Task.Deleted'     = @('Task deleted.','Tarea eliminada.','Compito eliminato.')
'Task.Empty'       = @('No tasks found.','No se encontraron tareas.','Nessun compito trovato.')
'Task.Overdue'     = @('Overdue','Vencida','In ritardo')

'Profile.Title'         = @('Profile','Perfil','Profilo')
'Profile.PersonalInfo'  = @('Personal information','Información personal','Informazioni personali')
'Profile.Language'      = @('Preferred language','Idioma preferito','Lingua preferita')
'Profile.ChangePassword'= @('Change password','Cambiar contraseña','Cambia password')
'Profile.CurrentPassword'   = @('Current password','Contraseña actual','Password attuale')
'Profile.NewPassword'       = @('New password','Nueva contraseña','Nuova password')
'Profile.ConfirmPassword'   = @('Confirm password','Confirmar contraseña','Conferma password')
'Profile.Saved'         = @('Profile updated.','Perfil actualizado.','Profilo aggiornato.')
'Profile.PasswordChanged' = @('Password changed.','Contraseña cambiata.','Password aggiornata.')
'Profile.WrongPassword' = @('Current password is incorrect.','La contraseña actual es incorrecta.','La password attuale non è corretta.')

'Admin.Title'      = @('Administration','Administración','Amministrazione')
'Admin.Users'      = @('Users','Utenti','Utenti')
'Admin.Roles'      = @('Roles','Roles','Ruoli')
'Admin.EditUser'   = @('Edit user','Editar utente','Modifica utente')
'Admin.RoleAdmin'  = @('Administrator','Administrador','Amministratore')
'Admin.RoleUser'   = @('Regular user','Usuario normal','Utente standard')
'Admin.MakeAdmin'  = @('Administrator access','Acceso de administrador','Accesso amministratore')
'Admin.IsActive'   = @('Active account','Cuenta activa','Account attivo')
'Admin.Toggle'     = @('Activate / deactivate','Activar / desactivar','Attiva / disattiva')
'Admin.Created'    = @('User updated.','Usuario actualizado.','Utente aggiornato.')
'Admin.Deleted'    = @('User deleted.','Usuario eliminado.','Utente eliminato.')
'Admin.Empty'      = @('No users found.','No se encontraron usuarios.','Nessun utente trovato.')
'Admin.SystemRoles'= @('System roles','Roles del sistema','Ruoli di sistema')
'Admin.RoleAdminDesc'  = @('Full access to the administration panel and all CRM data.','Acceso completo al panel de administración y a todos los datos del CRM.','Accesso completo al pannello di amministrazione e a tutti i dati del CRM.')
'Admin.RoleUserDesc'   = @('Standard access to customers, activities and tasks.','Acceso estándar a clientes, actividades y tareas.','Accesso standard a clienti, attività e compiti.')
'Common.Tasks'         = @('Tasks','Tareas','Compiti')

'Error.Title'      = @('Something went wrong','Algo salió mal','Qualcosa è andato storto')
'Error.NotFound'   = @('The page you requested was not found.','La página solicitada no se encontró.','La pagina richiesta non è stata trovata.')

'Language.En' = @('English','English','English')
'Language.Es' = @('Español','Español','Español')
'Language.It' = @('Italiano','Italiano','Italiano')
}

$header = @'
<?xml version="1.0" encoding="utf-8"?>
<root>
  <xsd:schema id="root" xmlns="" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
    <xsd:element name="root" msdata:IsDataSet="true">
      <xsd:complexType>
        <xsd:choice maxOccurs="unbounded">
          <xsd:element name="metadata">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name="value" type="xsd:string" minOccurs="0" />
              </xsd:sequence>
              <xsd:attribute name="name" use="required" type="xsd:string" />
              <xsd:attribute name="type" type="xsd:string" />
              <xsd:attribute name="mimetype" type="xsd:string" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name="assembly">
            <xsd:complexType>
              <xsd:attribute name="alias" type="xsd:string" />
              <xsd:attribute name="name" type="xsd:string" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name="data">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name="value" type="xsd:string" minOccurs="0" msdata:Ordinal="1" />
                <xsd:element name="comment" type="xsd:string" minOccurs="0" msdata:Ordinal="2" />
              </xsd:sequence>
              <xsd:attribute name="name" type="xsd:string" use="required" msdata:Ordinal="1" />
              <xsd:attribute name="type" type="xsd:string" msdata:Ordinal="3" />
              <xsd:attribute name="mimetype" type="xsd:string" msdata:Ordinal="4" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name="resheader">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name="value" type="xsd:string" minOccurs="0" msdata:Ordinal="1" />
              </xsd:sequence>
              <xsd:attribute name="name" type="xsd:string" use="required" />
            </xsd:complexType>
          </xsd:element>
        </xsd:choice>
      </xsd:complexType>
    </xsd:element>
  </xsd:schema>
  <resheader name="resmimetype"><value>text/microsoft-resx</value></resheader>
  <resheader name="version"><value>2.0</value></resheader>
  <resheader name="reader"><value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value></resheader>
  <resheader name="writer"><value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value></resheader>
'@

$languages = @('en', 'es', 'it')

foreach ($i in 0..($languages.Count - 1)) {
    $lang = $languages[$i]
    $sb = New-Object System.Text.StringBuilder
    [void]$sb.Append($header)
    foreach ($key in $keys.Keys) {
        $value = $keys[$key][$i].Replace('&', '&amp;').Replace('<', '&lt;').Replace('>', '&gt;')
        [void]$sb.Append("`n  <data name=`"$key`" xml:space=`"preserve`">`n    <value>$value</value>`n  </data>")
    }
    [void]$sb.Append("`n</root>`n")
    $suffix = if ($lang -eq 'en') { '' } else { ".$lang" }
    $path = Join-Path $PSScriptRoot "Resources\SharedResource$suffix.resx"
    [System.IO.File]::WriteAllText($path, $sb.ToString(), [System.Text.UTF8Encoding]::new($false))
    Write-Host "Wrote $path ($($keys.Count) keys)"
}
