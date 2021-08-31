-- DOCUMENTATION: https://ic3w0lf22.gitbook.io/roblox-account-manager/

local Account = {} Account.__index = Account

local WebserverSettings = {
    Port = '7963',
    Password = ''
}

local function GET(Method, Account, ...)
    local Arguments = {...}
    local Url, Body = 'http://localhost:' .. WebserverSettings.Port .. '/' .. Method .. '?Account=' .. Account

    for Index, Parameter in pairs(Arguments) do
        Url = Url .. '&' .. Parameter
    end

    if WebserverSettings.Password and #WebserverSettings.Password >= 6 then
        Url = Url .. '&Password=' .. WebserverSettings.Password
    end
    
    return syn.request {
        Method = 'GET',
        Url = Url
    }
end

local function POST(Method, Account, Body, ...)
    local Arguments = {...}
    local Url, Body = 'http://localhost:' .. WebserverSettings.Port .. '/' .. Method .. '?Account=' .. Account

    for Index, Parameter in pairs(Arguments) do
        Url = '&' .. Url .. Parameter
    end

    if WebserverSettings.Password and #WebserverSettings.Password >= 6 then
        Url = Url .. '&Password=' .. WebserverSettings.Password
    end
    
    return syn.request {
        Method = 'POST',
        Url = Url,
        Body = Body
    }
end

function Account.new(Username)
    local self = {} setmetatable(self, Account)

    local IsValid = GET('Test', Username)

    if IsValid.StatusCode ~= 200 or IsValid.Body == 'Invalid Account' then return false end

    self.Username = Username

    return self
end

function Account:GetCSRFToken() return GET('GetCSRFToken', self.Username) end

function Account:BlockUser(Argument)
    if typeof(Argument) == 'string' then
        return GET('BlockUser', self.Username, 'UserId=' .. Argument)
    elseif typeof(Argument) == 'Instance' and Argument:IsA'Player' then
        return self:BlockUser(tostring(Argument.UserId))
    elseif typeof(Argument) == 'number' then
        return self:BlockUser(tostring(Argument))
    end
end
function Account:UnblockUser(Argument)
    if typeof(Argument) == 'string' then
        return GET('UnblockUser', self.Username, 'UserId=' .. Argument)
    elseif typeof(Argument) == 'Instance' and Argument:IsA'Player' then
        return self:BlockUser(tostring(Argument.UserId))
    elseif typeof(Argument) == 'number' then
        return self:BlockUser(tostring(Argument))
    end
end
function Account:GetBlockedList() return GET('GetBlockedList', self.Username) end
function Account:UnblockEveryone() return GET('UnblockEveryone', self.Username) end

function Account:GetAlias() return GET('GetAlias', self.Username) end
function Account:GetDescription() return GET('GetDescription', self.Username) end
function Account:SetAlias(Alias) return POST('SetAlias', self.Username, Alias) end
function Account:SetDescription(Description) return POST('SetDescription', self.Username, Description) end
function Account:AppendDescription(Description) return POST('AppendDescription', self.Username, Description) end

function Account:GetField(Field) return GET('GetField', self.Username, 'Field=' .. Field).Body end
function Account:SetField(Field, Value) return GET('SetField', self.Username, 'Field=' .. Field, 'Value=' .. tostring(Value)).Body end
function Account:RemoveField(Field) return GET('RemoveField', self.Username, 'Field=' .. Field) end

function Account:GetCookie() return GET('GetCookie', self.Username) end
function Account:LaunchAccount(PlaceId, JobId, FollowUser, JoinVip) -- if you want to follow someone, PlaceId must be their user id
    return GET('LaunchAccount', self.Username, 'PlaceId=' .. PlaceId, JobId and ('JobId=' .. JobId), FollowUser and 'FollowUser=true', JoinVip and 'JoinVIP=true')
end

return Account