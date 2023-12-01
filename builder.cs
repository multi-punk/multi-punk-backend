using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiApi.Database.Tables;

namespace MultiApi;

public class Builder
{
    private Party party = new Party();
    public Builder WithOwner(string ownerId)
    {
        party.OwnerId = ownerId;
        return this;
    }
    public Builder WithId(string id)
    {
        party.Id = id;
        return this;
    }
    public Builder AddParticipants (string participantId)
    {
        party.Participants.Add(participantId);
        return this;
    }
    public Party Build()
    {
        return party;
    }
}
