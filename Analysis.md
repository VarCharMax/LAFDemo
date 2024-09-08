
## Methods exposed by the controller:
#### get
#### match
#### add
#### agents

## Variables
#### agents
- A static list that just functions as a data store.
- Because it's a singleton, there is a client-side lock to protect it from concurrent add operations.
- This should be re-implemented as a stand-alone data store. The singleton implementation can be kept if time contraints prevent me from creating a proper database, but a real datbase will obviate the singleton behaviour and the need for a static variable.
#### requestCount
- A static variable that tracks the number of times the match controller method has been called. Static variables are essentially global variables, and should never be used arbitrarily in code, as they are difficult to track and confusing. They are in any case a hack, and there are better ways of implementing the functionality they support.
- If we ned this functionality, it should be re-implemented a a service.

## Classes:
#### Agent
- Has ID value. Need to clarify where this id value comes from - is it provided by the business, or an autoincremented database key?
- Has a series of properties that an integer rating value can be applied to.
- These properties should have a limit on the values that can be assigned, e.g. 1 - ?. It is not clear what the scale of the values is supposed to be.
#### MatchRequest
- A parameter to MatchAgent. Allows multiple parameters to be bundled. 

## Business logic:
#### Get
- Just returns a value to ensure that the controller is reachable.
#### GetScore
- Just translates the string AttributePreference property of MatchRequest into the associated integer value.
- See if it's possible to avoid hard-coding this operation, so that the conversion is self-maintaining if more agent preference properties are added later.
#### MatchAgent (match)
- Takes a MatchRequest parameter that allows the client to specify the size of the agent's business and the minimum value of a specified attribute. Both properties must have a value.
- Has a hard-coded condition that the specified attribute must have a value of at least 5. This should be turned into an application setting - either an application config parameter or a setting that can be set by an administrator.
- Returns the matched agent as JSON.
- Has embedded logic - if 0 or a multiple of 100 requests have been performed, the size of the list is doubled. I'm not sure what this logic is for, but it seems to have an obvious problem because the Id field will get duplicated by this operation. If we want to keep this or some variation of it for some reason, at least the static requestCount variable should be re-implemented as a service.
- The implementation is tricked up to emulate async behaviour. Obviously this will be removed.
#### AddAgent (add)
- Adds a new agent to the data store. Will return an error result if an agent is added with an existing id. This is a problematic situation, since if the id is a database key, the value will not be known to the end user. Presumably the id will have to be something unique, such as an agent licence number. The existing Id field should be changed to something meaningful, and the Id field used for database purposes.
- The lock wil be automatically replaced by a transaction if a datastore with an ORM is implemented.
- Some way of obfuscating the key field should also be implemented, so that database keys are not exposed to the end user.
#### GetAllAgents (agents)
- Just returns a list all agents in the data store.
- All database code should be replaced using dependency injection.
- The connection string should be stored as an application config parameter.
