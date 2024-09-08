### Application Architecture Analysis
The following information is provided to give an example of the architecture you could be working with at LAF. It needs some attention in order to improve reliability, scalability, and developer experience.

### Agents Compare: Our Registration Funnels
![Diagram](architecture.drawio.svg)
#### Repository
- Backend: An ASP.NET Core application that provides APIs and an MVC application that serves the skeleton HTML
- Frontend: a folder that contains a react application
  - The react application is broken up into a number of variants, and each variant has a number of versions. 
  Each variant is build with a webpack config. Each version has its own top level entry file which references both shared and variant specific components and services.

Development on the funnels can include adding a new version, adding or updating a question across variants, updating services with different data requests/responses, and more.

#### Example variants (and versions)
- LAF: Desktop browser experience (t1, t2, t3)
- LAFMobile: Mobile browser experience (m1, m2, m3)
- AC: Desktop browser experience with alternate branding/styling (r1, r2, r3)

#### Build, Deployment, and Hosting
Our build server builds a single artifact containing the ASP.NET Core (Backend) application and all variants of the React application. Each variant then has a deployment action that deploys the artifact with the config required for the variant.

The variants are hosted in IIS (a site per variant, this includes the ASP.NET application running for each variant).

Each variant is accesible at a distinct url with the version identifier also in the url. eg: lafregistration.laf.com/registration/t1 and lafmobile.laf.com/registration/m3

IIS is running on windows instances behind a load balancer and auto scaling group. Cloudfront adds a caching layer between the load balancer and the user.