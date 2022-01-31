module.exports = {
  Query: {
    getAllPeople: (parent, args, {dataSources}, info) => { 
      return dataSources.personAPI.getPeople();
    },
    getPersonById: (parent, {id}, {dataSources}, info) => {
      return dataSources.personAPI.getPersonById(id);
    },
    getPeopleFilter: (parent, args, {dataSources}, info) => {
      return dataSources.personAPI.getPeopleFilter(args);
    }
  },
  Mutation: {
    addPerson: (parent, {person}, {dataSources}, info) => {
      dataSources.personAPI.addPerson(person);
      return true;
    },
    updatePerson: (parent, {person}, {dataSources}, info) => {
      dataSources.personAPI.updatePerson(person);
      return true;
    },
    deletePerson: (parent, {id}, {dataSources}, info) => {
      dataSources.personAPI.deletePerson(id);
      return true;
    }
  }
};