pipeline {

    agent any

    parameters {
        string(name:"SOLUTION_NAME", defaultValue:"WebApi.sln", description: "Solution Name")
        text(name:"TEST_PROJ_PATH", defaultValue:"", description: "Test Project Path To .csproj file")
        booleanParam(name: 'BUILD', defaultValue: false, description: 'Check To Build')
        booleanParam(name: 'TEST', defaultValue: false, description: 'Check To Test')
        booleanParam(name: 'PUBLISH', defaultValue: false, description: 'Check To Publish')
        booleanParam(name: 'DEPLOY', defaultValue: false, description: 'Check To Deploy')
    }

    stages {
        stage('build') {
            when {
                expression { params.BUILD || params.PUBLISH || params.TEST}
            }
            steps {
                bat '''dotnet build %SOLUTION_NAME%'''
            }
        }

        stage('publish') {
             when {
                expression { return params.PUBLISH }
            }
            steps {
                bat '''dotnet publish %SOLUTION_NAME% -p:Configuration=release -v:q -o ../artifacts'''
            }
        }
    }
    // post { 
    //     always { 
    //         sh 'zip -r artifacts.zip WebApplication2/Publish/ '
    //         archiveArtifacts artifacts: 'artifacts.zip'
    //     }
    // }
}