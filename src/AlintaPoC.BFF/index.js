const { ApolloServer, ApolloError } = require('apollo-server')
const PersonAPI = require('./datasources/people')

// The GraphQL schema
const typeDefs = require('./schema')

// A map of functions which return data for the schema.
const resolvers = require('./resolvers')

const dataSources = () => ({
  personAPI: new PersonAPI()
})

process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0'

const server = new ApolloServer({
  typeDefs,
  resolvers,
  dataSources,
  debug: false,
  formatError: err => {
    if (err.extensions.code == 'INTERNAL_SERVER_ERROR') {
      return new ApolloError('Some error', 'ERROR')
    }
    return err
  }
})

server.listen(process.env.port || 4001).then(({ url }) => {
  console.log(`🚀 Server ready at ${url}`)
})
