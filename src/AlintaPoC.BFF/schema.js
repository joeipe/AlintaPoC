const { gql } = require('apollo-server');

module.exports = gql`
  type Query {
    getAllPeople: [Person],
    getPersonById(id: ID!): Person,
    getPeopleFilter(
      id: ID,
      firstName: String,
      lastName: String,
      doB: String
    ): [Person],
  }
  type Mutation {
    addPerson(person: PersonInput): Boolean,
    updatePerson(person: PersonInput): Boolean,
    deletePerson(id: ID!): Boolean
  }
  type Person {
    id: ID!,
    firstName: String!,
    lastName: String!,
    doB: String!,
  }
  input PersonInput {
    id: ID,
    firstName: String,
    lastName: String,
    doB: String,
  }
`;