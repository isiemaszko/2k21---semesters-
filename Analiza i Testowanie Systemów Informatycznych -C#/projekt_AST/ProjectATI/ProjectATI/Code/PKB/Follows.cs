using ProjectATI.Code.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.PKB
{
    class Follows
    {
        private List<Node> followsList;
        private List<Node> followsReverseList;

        public Follows()
        {
            this.followsList = new List<Node>();
            this.followsReverseList = new List<Node>();
        }

        public Node SetFollows(Node follower, Node followee)
        {
            if (followsList.Contains(follower) && followsReverseList.Contains(followee))
                return null;

            if (!followsList.Contains(follower) && !followsReverseList.Contains(followee))
            {
                followsList.Add(follower);
                followsReverseList.Add(followee);
            }
            return follower;
        }

        public Node GetFollows(Node follower)
        {
            if (followsList.Contains(follower))
            {
                return followsReverseList[followsList.IndexOf(follower)];
            }
            return null;
        }

        public Node GetFollowedBy(Node followee)
        {
            if (followsReverseList.Contains(followee))
            {
                return followsList[followsReverseList.IndexOf(followee)];
            }
            return null;
        }

        public List<Node> GetFollowsStar(Node follower)
        {
            List<Node> returnList = new List<Node>();
            Node followee = new Node();

            if (follower != null)
            {
                followee = GetFollows(follower);
                if (followee != null)
                    returnList.Add(followee);
            }
            else
                return null;
            
            if (followee != null)
            {
                returnList.AddRange(GetFollowsStar(followee));
            }
            return returnList;
        }

        public List<Node> GetFollowedByStar(Node followee)
        {
            List<Node> returnList = new List<Node>();
            Node follower = new Node();

            if (followee != null)
            {
                follower = GetFollowedBy(followee);
                if (follower != null)
                    returnList.Add(follower);
            }
            else
                return null;

            if (follower != null)
            {
                returnList.AddRange(GetFollowedByStar(follower));
            }
            return returnList;
        }
    }
}