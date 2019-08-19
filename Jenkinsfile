pipeline {

    agent any

    parameters {
        string(name:"SOLUTION_NAME", defaultValue:"WebApi.sln", description: "Solution Name")
        string(name:"SOLUTION_DLL", defaultValue:"WebApi.dll", description: "Solution Name")

        string(name:"DOCKER_IMAGE_NAME", defaultValue:"aspcore_webapplication", description: "Docker Image Name")

        text(name:"TEST_PROJ_PATH", defaultValue:"", description: "Test Project Path To .csproj file")
        string(name:"PORT_NO", defaultValue:"8989", description: "Bind Port Number")
        booleanParam(name: 'BUILD', defaultValue: false, description: 'Check To Build')
        booleanParam(name: 'TEST', defaultValue: false, description: 'Check To Test')
        booleanParam(name: 'PUBLISH', defaultValue: false, description: 'Check To Publish')
        booleanParam(name: 'DEPLOY', defaultValue: false, description: 'Check To Deploy')
        booleanParam(name: 'SONAR_ANALYSIS', defaultValue: false, description: 'Check To Sonar Analysis')
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
                expression { return params.PUBLISH || params.DEPLOY }
            }
            steps {
                bat '''dotnet publish %SOLUTION_NAME% -p:Configuration=release -v:q -o ../artifacts'''
            }
        }


         stage('Docker build') {
            
             steps{
                bat 'docker build -t %DOCKER_IMAGE_NAME% .'
             }
        }

        stage('UpdateDockerRepo') {
            when {
                  expression {return params.DEPLOY}
            }
            steps {
                bat ''' docker tag %DOCKER_IMAGE_NAME%:latest subtleparesh/%DOCKER_IMAGE_NAME% 
                        docker push subtleparesh/%DOCKER_IMAGE_NAME%:latest'''
            }
        }
         stage('run docker image'){
            steps{
                echo 'run the image'
                bat 'docker run -p %PORT_NO%:55031 -e SOLUTION_DLL=%SOLUTION_DLL%  %DOCKER_IMAGE_NAME%'
            }
        }

        
    }
  
}